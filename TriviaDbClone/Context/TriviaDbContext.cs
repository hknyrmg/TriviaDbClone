using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriviaDbClone.Entities;

namespace TriviaDbClone.Context
{
    public class TriviaDbContext: DbContext
    {
        public TriviaDbContext(DbContextOptions options)  : base(options) 
        {
                
        }
        DbSet<Question_Types> Question_Types { get; set; }
        DbSet<Categories> Categories { get; set; }
        DbSet<Difficulty> Difficulty { get; set; }
        DbSet<incorrect_answer> incorrect_answer { get; set; }
        DbSet<Correct_Answer> Correct_Answer { get; set; }

        DbSet<Questions> Questions { get; set; }

    }
}
