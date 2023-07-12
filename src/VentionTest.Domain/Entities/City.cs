using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentionTest.Domain.Commons;

namespace VentionTest.Domain.Entities
{
    public class City : Auditable
    {
        public string Name { get; set; }
        public long CountryId { get; set; }
        public Country Country { get; set; }
    }
}
