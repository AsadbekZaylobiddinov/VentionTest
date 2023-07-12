using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentionTest.Service.DTOs.LanguageCountry;

namespace VentionTest.Service.Interfaces
{
    public interface ILanguageCountryService
    {
        public Task<LanguageCountryForResultDto> AddAsync(LanguageCountryForCreationDto dto);
        public Task<IEnumerable<LanguageCountryForResultDto>> RetrieveAllAsync();
        public Task<LanguageCountryForResultDto> RetrieveByIdAsync(long id);
        public Task<LanguageCountryForResultDto> ModifyAsync(long id, LanguageCountryForUpdateDto dto);
        public Task<bool> RemoveAsync(long id);
    }
}
