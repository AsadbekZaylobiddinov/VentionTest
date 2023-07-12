using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentionTest.Domain.Commons;

namespace VentionTest.Domain.Entities
{
    public class LanguageCountry : Auditable
    {
        public long LanguageId { get; set; }
        public Language Language { get; set; }
        public long CountryId { get; set; }
        public Country Country { get; set; }
    }
}
