using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentionTest.Service.DTOs.Capital;

namespace VentionTest.Service.Interfaces
{
    public interface ICapitalService
    {
        public Task<CapitalForResultDto> AddAsync(CapitalForCreationDto dto);
        public Task<IEnumerable<CapitalForResultDto>> RetrieveAllAsync();
        public Task<CapitalForResultDto> RetrieveByIdAsync(long id);
        public Task<CapitalForResultDto> ModifyAsync(long id, CapitalForUpdateDto dto);
        public Task<bool> RemoveAsync(long id);
    }
}
