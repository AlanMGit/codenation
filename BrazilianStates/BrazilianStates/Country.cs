using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrazilianStates
{
    public class Country
    {
        public State[] Top10StatesByArea()
        {
            return GetStates().OrderByDescending(x => x.Extensive)
                              .Take(10)
                              .ToArray();
        }

        public List<State> GetStates()
        {
            return new List<State>
            {
                new State("Acre", "AC", 164123.040),
                new State("Alagoas", "AL", 27778.506),
                new State("Amapá", "AP", 142828.521),
                new State("Amazonas", "AM", 1559159.148),
                new State("Bahia", "BA", 564733.177),
                new State("Ceará", "CE", 148920.472),
                new State("Distrito Federal", "DF", 5779.999),
                new State("Espírito Santo", "ES", 46095.583),
                new State("Goiás", "GO", 340111.783),
                new State("Maranhão", "MA", 331937.450),
                new State("Mato Grosso", "MT", 903366.192),
                new State("Mato Grosso do Sul", "MS", 357145.532),
                new State("Minas Gerais", "MG", 586522.122),
                new State("Pará", "PA", 1247954.666),
                new State("Paraíba", "PB", 56585.000),
                new State("Paraná", "PR", 199307.922),
                new State("Pernambuco", "PE", 98311.616),
                new State("Piauí", "PI", 251577.738),
                new State("Rio de Janeiro", "RJ", 43780.172),
                new State("Rio Grande do Norte", "RN", 52811.047),
                new State("Rio Grande do Sul", "RS", 281730.223),
                new State("Rondônia", "RO", 237590.547),
                new State("Roraima", "RR", 224300.506),
                new State("Santa Catarina", "SC", 95736.165),
                new State("São Paulo", "SP", 248222.362),
                new State("Sergipe", "SE", 21915.116),
                new State("Tocantins", "TO", 277720.520)
            };
        }
    }
}
