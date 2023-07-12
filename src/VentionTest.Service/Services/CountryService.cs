using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentionTest.DAL.IRepositories;
using VentionTest.Domain.Entities;
using VentionTest.Service.DTOs.City;
using VentionTest.Service.DTOs.Country; 
using VentionTest.Service.Exceptions;
using VentionTest.Service.Interfaces;

namespace VentionTest.Service.Services
{
    public class CountryService : ICountryService
    {
        private readonly IRepository<Country> _countryRepository;
        private readonly IMapper _mapper;

        public CountryService(IMapper mapper,
            IRepository<Country> countryRepository)
        {
            this._mapper = mapper;
            this._countryRepository = countryRepository;
        }

        public async Task<CountryForResultDto> AddAsync(CountryForCreationDto dto)
        {
            var existModel = await _countryRepository.SelectAsync(c => c.Name == dto.Name);
            if (existModel != null && !existModel.IsDeleted)
                throw new CustomException(409, "Capital Already exist");

            var mapped = _mapper.Map<Country>(dto);
            mapped.CreatedAt = DateTime.UtcNow;

            var addedModel = await _countryRepository.InsertAsync(mapped);
            await _countryRepository.SaveAsync();

            return _mapper.Map<CountryForResultDto>(mapped);
        }

        public async Task<IEnumerable<CountryForResultDto>> RetrieveAllAsync()
        {
            var countries = await _countryRepository.SelectAll(c => c.IsDeleted == false)
                .Where(u => u.IsDeleted == false)
                .ToListAsync();

            return _mapper.Map<IEnumerable<CountryForResultDto>>(countries);
        }

        public async Task<CountryForResultDto> RetrieveByIdAsync(long id)
        {
            var country = await _countryRepository.SelectAsync(u => u.Id == id);
            if (country is null || country.IsDeleted)
                throw new CustomException(404, "Country Not Found");

            return _mapper.Map<CountryForResultDto>(country);
        }
        public async Task<CountryForResultDto> ModifyAsync(long id, CountryForUpdateDto dto)
        {
            var country = await _countryRepository.SelectAsync(u => u.Id == id);
            if (country is null || country.IsDeleted)
                throw new CustomException(404, "Couldn't found country for given Id");

            var modifiedModel = _mapper.Map(dto, country);
            modifiedModel.UpdatedAt = DateTime.UtcNow;

            await _countryRepository.SaveAsync();

            return _mapper.Map<CountryForResultDto>(country);

        }
        public async Task<bool> RemoveAsync(long id)
        {
            var country = await _countryRepository.SelectAsync(u => u.Id == id);
            if (country is null || country.IsDeleted)
            {
                throw new CustomException(404, "Couldn't find country for this given Id");
            }
            await _countryRepository.DeleteAsync(u => u.Id == id);

            return true;
        }
    }
}
