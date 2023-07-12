using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentionTest.DAL.IRepositories;
using VentionTest.Domain.Entities;
using VentionTest.Service.DTOs.Language;
using VentionTest.Service.DTOs.LanguageCountry;
using VentionTest.Service.Exceptions;
using VentionTest.Service.Interfaces;

namespace VentionTest.Service.Services
{
    public class LanguageCountryService : ILanguageCountryService
    {
        private readonly IRepository<LanguageCountry> _languageCountryRepository;
        private readonly IMapper _mapper;

        public LanguageCountryService(IMapper mapper,
            IRepository<LanguageCountry> languageCountryRepository)
        {
            this._mapper = mapper;
            this._languageCountryRepository = languageCountryRepository;
        }

        public async Task<LanguageCountryForResultDto> AddAsync(LanguageCountryForCreationDto dto)
        {
            var existModel = await _languageCountryRepository.SelectAsync(c => c.CountryId == dto.CountryId && c.LanguageId == dto.LanguageId);
            if (existModel != null && !existModel.IsDeleted)
                throw new CustomException(409, "LanguageCountry Already exist");

            var mapped = _mapper.Map<LanguageCountry>(dto);
            mapped.CreatedAt = DateTime.UtcNow;

            var addedModel = await _languageCountryRepository.InsertAsync(mapped);
            await _languageCountryRepository.SaveAsync();

            return _mapper.Map<LanguageCountryForResultDto>(mapped);
        }

        public async Task<IEnumerable<LanguageCountryForResultDto>> RetrieveAllAsync()
        {
            var languageCountries = await _languageCountryRepository.SelectAll(c => c.IsDeleted == false)
                .Where(u => u.IsDeleted == false)
                .ToListAsync();

            return _mapper.Map<IEnumerable<LanguageCountryForResultDto>>(languageCountries);
        }

        public async Task<LanguageCountryForResultDto> RetrieveByIdAsync(long id)
        {
            var languageCountry = await _languageCountryRepository.SelectAsync(u => u.Id == id);
            if (languageCountry is null || languageCountry.IsDeleted)
                throw new CustomException(404, "LanguageCountry Not Found");

            return _mapper.Map<LanguageCountryForResultDto>(languageCountry);
        }
        public async Task<LanguageCountryForResultDto> ModifyAsync(long id, LanguageCountryForUpdateDto dto)
        {
            var languageCountry = await _languageCountryRepository.SelectAsync(u => u.Id == id);
            if (languageCountry is null || languageCountry.IsDeleted)
                throw new CustomException(404, "Couldn't found LanguageCountry for given Id");

            var modifiedModel = _mapper.Map(dto, languageCountry);
            modifiedModel.UpdatedAt = DateTime.UtcNow;

            await _languageCountryRepository.SaveAsync();

            return _mapper.Map<LanguageCountryForResultDto>(languageCountry);

        }
        public async Task<bool> RemoveAsync(long id)
        {
            var languageCountry = await _languageCountryRepository.SelectAsync(u => u.Id == id);
            if (languageCountry is null || languageCountry.IsDeleted)
            {
                throw new CustomException(404, "Couldn't find LanguageCountry for this given Id");
            }
            await _languageCountryRepository.DeleteAsync(u => u.Id == id);

            return true;
        }
    }
}
