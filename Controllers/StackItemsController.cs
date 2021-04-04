using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackRestApi.Models;

namespace StackRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StackItemsController : ControllerBase
    {
        private readonly IReversedStackManager _stackManager;

        public StackItemsController(IReversedStackManager stackManager)
        {
            _stackManager = stackManager;
        }

        // GET: api/StackItems/id
        // id - action name, could be pop / peek
        [HttpGet("{Id}")]
        public async Task<ActionResult<string>> GetPopOrPeek(string id)
        {
            if(id == "pop" || id == "peek")
            {
                StackItem stackItem = id == "pop" ?  await _stackManager.Pop() : await _stackManager.Peek();
                if (stackItem != null && stackItem.Data != null)
                {
                    return stackItem.Data;
                }
            }
            return NotFound();
        }

        // POST: api/StackItems/id
        // id - action name, could be revert / push
        [HttpPost("{id}")]
        public async Task<ActionResult<StackItem>> PostPushOrRevertStack(string Id,StackItem stackItem)
        {
            if(Id == "revert")
            {
                if(await _stackManager.RevertStack())
                {
                    return Ok();
                }
            }
            else if(Id == "push")
            {
               if( await _stackManager.Push(stackItem) != null)
                {
                    return Ok();
                }
            }
            return NoContent();
        }
    }
}
