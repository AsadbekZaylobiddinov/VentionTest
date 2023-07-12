using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentionTest.Domain.Commons;

namespace VentionTest.Domain.Entities
{
    public class Country : Auditable
    {
        public string Name { get; set; }
        public Capital Capital { get; set; } 
        public List<City> Cities { get; set; }
        public List<Language> Languages { get; set; }
        public List<LanguageCountry> LanguageCountries { get; set; }
    }
}
