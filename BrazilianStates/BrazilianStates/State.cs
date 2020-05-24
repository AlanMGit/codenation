using System;
using System.Collections.Generic;
using System.Text;

namespace BrazilianStates
{
    public class State
    {
        public State()
        {
        }

        public State(string name, string acronym)
        {
            this.Name = name;
            this.Acronym = acronym;
        }

        public State(string name, string acronym, double extensive)
        {
            this.Name = name;
            this.Acronym = acronym;
            this.Extensive = extensive;
        }

        public string Name { get; set; }

        public string Acronym { get; set; }

        public double Extensive { get; set; }

    }
}
