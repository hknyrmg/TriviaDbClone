using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TriviaDbClone.Entities
{
    public class Questions
    {
        public Guid id { get; set; }
        public Guid category_id { get; set; }
        public Guid type_id { get; set; }
        public Guid difficulty_id { get; set; }
        public string question { get; set; }

    }
}
