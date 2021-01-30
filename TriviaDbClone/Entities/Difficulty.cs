using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TriviaDbClone.Entities
{
    public class Difficulty
    {
        public Guid id { get; set; }
        public string difficulty_level { get; set; }
    }
}
