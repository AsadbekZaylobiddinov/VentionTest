using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentionTest.Service.DTOs.Country;
using VentionTest.Service.DTOs.Language;

namespace VentionTest.Service.Interfaces
{
    public interface ILanguageService
    {
        public Task<LanguageForResultDto> AddAsync(LanguageForCreationDto dto);
        public Task<IEnumerable<LanguageForResultDto>> RetrieveAllAsync();
        public Task<LanguageForResultDto> RetrieveByIdAsync(long id);
        public Task<LanguageForResultDto> ModifyAsync(long id, LanguageForUpdateDto dto);
        public Task<bool> RemoveAsync(long id);
    }
}
