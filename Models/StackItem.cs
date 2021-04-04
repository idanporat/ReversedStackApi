using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StackRestApi.Models
{
    public class StackItem
    {
        [Key]
        public string Id { get; set; }
        public string Data { get; set; }
        public string BeforeItemId { get; set; }
        public string AfterItemId { get; set; }

        //Top and last could be on the same property, but this is more clear
        public bool IsTop { get; set; }
        public bool IsLast { get; set; }
    }
}
