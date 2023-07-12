using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentionTest.Service.DTOs.Capital;
using VentionTest.Service.DTOs.City;

namespace VentionTest.Service.Interfaces
{
    public interface ICityService
    {
        public Task<CityForResultDto> AddAsync(CityForCreationDto dto);
        public Task<IEnumerable<CityForResultDto>> RetrieveAllAsync(long countryId);
        public Task<CityForResultDto> RetrieveByIdAsync(long id);
        public Task<CityForResultDto> ModifyAsync(long id, CityForUpdateDto dto);
        public Task<bool> RemoveAsync(long id);
    }
}
