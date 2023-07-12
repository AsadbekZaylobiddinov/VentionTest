using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentionTest.DAL.IRepositories;
using VentionTest.Domain.Entities;
using VentionTest.Service.DTOs.Capital;
using VentionTest.Service.Exceptions;
using VentionTest.Service.Interfaces;

namespace VentionTest.Service.Services
{
    public class CapitalService : ICapitalService
    {
        private readonly IRepository<Capital> _capitalRepository;
        private readonly IMapper _mapper;

        public CapitalService(IMapper mapper,
            IRepository<Capital> capitalRepository)
        {
            this._mapper = mapper;
            this._capitalRepository = capitalRepository;
        }

        public async Task<CapitalForResultDto> AddAsync(CapitalForCreationDto dto)
        {
            var existModel = await _capitalRepository.SelectAsync(c => c.Name == dto.Name);
            if (existModel != null && !existModel.IsDeleted)
                throw new CustomException(409, "Capital Already exist");

            var mapped = _mapper.Map<Capital>(dto);
            mapped.CreatedAt = DateTime.UtcNow;

            var addedModel = await _capitalRepository.InsertAsync(mapped);
            await _capitalRepository.SaveAsync();

            return _mapper.Map<CapitalForResultDto>(mapped);
        }

        public async Task<IEnumerable<CapitalForResultDto>> RetrieveAllAsync()
        {
            var capitals = await _capitalRepository.SelectAll()
                .Where(u => u.IsDeleted == false)
                .ToListAsync();

            return _mapper.Map<IEnumerable<CapitalForResultDto>>(capitals);
        }

        public async Task<CapitalForResultDto> RetrieveByIdAsync(long id)
        {
            var capital = await _capitalRepository.SelectAsync(u => u.Id == id);
            if (capital is null || capital.IsDeleted)
                throw new CustomException(404, "Capital Not Found");

            return _mapper.Map<CapitalForResultDto>(capital);
        }
        public async Task<CapitalForResultDto> ModifyAsync(long id, CapitalForUpdateDto dto)
        {
            var capital = await _capitalRepository.SelectAsync(u => u.Id == id);
            if (capital is null || capital.IsDeleted)
                throw new CustomException(404, "Couldn't found capital for given Id");

            var modifiedModel = _mapper.Map(dto, capital);
            modifiedModel.UpdatedAt = DateTime.UtcNow;

            await _capitalRepository.SaveAsync();

            return _mapper.Map<CapitalForResultDto>(capital);

        }
        public async Task<bool> RemoveAsync(long id)
        {
            var capital = await _capitalRepository.SelectAsync(u => u.Id == id);
            if (capital is null || capital.IsDeleted)
            {
                throw new CustomException(404, "Couldn't find capital for this given Id");
            }
            await _capitalRepository.DeleteAsync(u => u.Id == id);

            return true;
        }
    }
}
