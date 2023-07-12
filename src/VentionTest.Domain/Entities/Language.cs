using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentionTest.Domain.Commons;

namespace VentionTest.Domain.Entities
{
    public class Language : Auditable
    {
        public string Name { get; set; }
        public List<Country> Countries { get; set; }
        public List<LanguageCountry> LanguageCountries { get; set; }
    }
}
