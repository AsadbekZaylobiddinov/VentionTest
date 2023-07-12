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
using VentionTest.Service.DTOs.City;
using VentionTest.Service.Exceptions;
using VentionTest.Service.Interfaces;

namespace VentionTest.Service.Services
{
    public class CityService : ICityService
    {
        private readonly IRepository<City> _cityRepository;
        private readonly IMapper _mapper;

        public CityService(IMapper mapper,
            IRepository<City> cityRepository)
        {
            this._mapper = mapper;
            this._cityRepository = cityRepository;
        }

        public async Task<CityForResultDto> AddAsync(CityForCreationDto dto)
        {
            var existModel = await _cityRepository.SelectAsync(c => c.Name == dto.Name);
            if (existModel != null && !existModel.IsDeleted)
                throw new CustomException(409, "Capital Already exist");

            var mapped = _mapper.Map<City>(dto);
            mapped.CreatedAt = DateTime.UtcNow;

            var addedModel = await _cityRepository.InsertAsync(mapped);
            await _cityRepository.SaveAsync();

            return _mapper.Map<CityForResultDto>(mapped);
        }
        
        public async Task<IEnumerable<CityForResultDto>> RetrieveAllAsync(long countryId)
        {
            var cities = await _cityRepository.SelectAll(c => c.CountryId == countryId)
                .Where(u => u.IsDeleted == false)
                .ToListAsync();

            return _mapper.Map<IEnumerable<CityForResultDto>>(cities);
        }

        public async Task<CityForResultDto> RetrieveByIdAsync(long id)
        {
            var city = await _cityRepository.SelectAsync(u => u.Id == id);
            if (city is null || city.IsDeleted)
                throw new CustomException(404, "City Not Found");

            return _mapper.Map<CityForResultDto>(city);
        }
        public async Task<CityForResultDto> ModifyAsync(long id, CityForUpdateDto dto)
        {
            var city = await _cityRepository.SelectAsync(u => u.Id == id);
            if (city is null || city.IsDeleted)
                throw new CustomException(404, "Couldn't found city for given Id");

            var modifiedModel = _mapper.Map(dto, city);
            modifiedModel.UpdatedAt = DateTime.UtcNow;

            await _cityRepository.SaveAsync();

            return _mapper.Map<CityForResultDto>(city);

        }
        public async Task<bool> RemoveAsync(long id)
        {
            var city = await _cityRepository.SelectAsync(u => u.Id == id);
            if (city is null || city.IsDeleted)
            {
                throw new CustomException(404, "Couldn't find city for this given Id");
            }
            await _cityRepository.DeleteAsync(u => u.Id == id);

            return true;
        }
    }
}
