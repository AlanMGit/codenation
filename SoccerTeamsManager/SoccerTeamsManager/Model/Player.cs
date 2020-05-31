using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerTeamsManager.Model
{
    public class Player : ModelBase
    {
        public long TeamId { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public int SkillLevel { get; set; }
        public decimal Salary { get; set; }
        public bool Captain { get; set; } = false;
    }
}
