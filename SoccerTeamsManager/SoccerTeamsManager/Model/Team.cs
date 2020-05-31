using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerTeamsManager.Model
{
    public class Team : ModelBase
    {
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public string MainShirtColor { get; set; }
        public string SecondaryShirtColor { get; set; }
    }
}
