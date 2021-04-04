using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackRestApi
{
    /// <summary>
    /// The design of the reversed stack is like a two ways linkedlist,
    /// which for each item has a 2 pointers (before and after).
    /// with this kind of design the revert action should only switch the definition between the top and last items
    /// </summary>
    public class ReversedStackManager : IReversedStackManager
    {
        private readonly StackContext _context;

        public ReversedStackManager(StackContext context)
        {
            _context = context;
        }
        //get the top item
        public async Task<StackItem> Peek()
        {
            var stackItem = await _context.StackItems.FirstOrDefaultAsync(item => item.IsTop == true);
            if (stackItem == null)
            {
                return null;
            }
            return stackItem;
        }
        //get and remove the top item and set the top item to the poinetered item of the removed one
        public async Task<StackItem> Pop()
        {
            var stackItem = await _context.StackItems.FirstOrDefaultAsync(item => item.IsTop == true);
            if (stackItem == null)
            {
                return null;
            }
            string newTopId = null;
            StackItem newTopItem = null;
            if (stackItem.BeforeItemId != null)
            {
                newTopId = stackItem.BeforeItemId;
                newTopItem = await _context.StackItems.FirstOrDefaultAsync(item => item.Id == newTopId);
                newTopItem.AfterItemId = null;
            }
            else if (stackItem.AfterItemId != null)
            {
                newTopId = stackItem.AfterItemId;
                newTopItem = await _context.StackItems.FirstOrDefaultAsync(item => item.Id == newTopId);
                newTopItem.BeforeItemId = null;
            }
            if (newTopItem != null)
            {
                newTopItem.IsTop = true;
            }

            _context.StackItems.Remove(stackItem);
            await _context.SaveChangesAsync();
            return stackItem;
        }
        //Pushing a new item, setting the new item to be the top 
        public async Task<StackItem> Push(StackItem stackItem)
        {
            stackItem.IsTop = true;
            stackItem.Id = Guid.NewGuid().ToString();

            StackItem topItem = await _context.StackItems.FirstOrDefaultAsync(item => item.IsTop);

            if (topItem != null)
            {
                stackItem.BeforeItemId = topItem.Id;
                stackItem.IsLast = false;

                topItem.IsTop = false;
                topItem.AfterItemId = stackItem.Id;
            }
            else
            {
                stackItem.IsLast = true;
            }
            _context.StackItems.Add(stackItem);
            await _context.SaveChangesAsync();
            return stackItem;
        }
        //Reverting the stack, by defining the old top to be the last and the old last to be the top
        public async Task<bool> RevertStack()
        {
            if(await _context.StackItems.CountAsync() < 2)
            {
                return false;
            }
            var top = await _context.StackItems.FirstOrDefaultAsync(item => item.IsTop);
            var last = await _context.StackItems.FirstOrDefaultAsync(item => item.IsLast);

            top.IsTop = false;
            top.IsLast = true;

            last.IsTop = true;
            last.IsLast = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }

    public interface IReversedStackManager
    {
        Task<StackItem> Push(StackItem stackItem);
        Task<StackItem> Pop();
        Task<StackItem> Peek();
        Task<bool> RevertStack();
    }
}
