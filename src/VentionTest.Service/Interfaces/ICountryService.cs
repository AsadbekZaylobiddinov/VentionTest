using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentionTest.Service.DTOs.City;
using VentionTest.Service.DTOs.Country;

namespace VentionTest.Service.Interfaces
{
    public interface ICountryService
    {
        public Task<CountryForResultDto> AddAsync(CountryForCreationDto dto);
        public Task<IEnumerable<CountryForResultDto>> RetrieveAllAsync();
        public Task<CountryForResultDto> RetrieveByIdAsync(long id);
        public Task<CountryForResultDto> ModifyAsync(long id, CountryForUpdateDto dto);
        public Task<bool> RemoveAsync(long id);
    }
}
