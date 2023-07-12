using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentionTest.DAL.IRepositories;
using VentionTest.Domain.Entities;
using VentionTest.Service.DTOs.Country;
using VentionTest.Service.DTOs.Language;
using VentionTest.Service.Exceptions;
using VentionTest.Service.Interfaces;

namespace VentionTest.Service.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly IRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public LanguageService(IMapper mapper,
            IRepository<Language> languageRepository)
        {
            this._mapper = mapper;
            this._languageRepository = languageRepository;
        }

        public async Task<LanguageForResultDto> AddAsync(LanguageForCreationDto dto)
        {
            var existModel = await _languageRepository.SelectAsync(c => c.Name == dto.Name);
            if (existModel != null && !existModel.IsDeleted)
                throw new CustomException(409, "Language Already exist");

            var mapped = _mapper.Map<Language>(dto);
            mapped.CreatedAt = DateTime.UtcNow;

            var addedModel = await _languageRepository.InsertAsync(mapped);
            await _languageRepository.SaveAsync();

            return _mapper.Map<LanguageForResultDto>(mapped);
        }

        public async Task<IEnumerable<LanguageForResultDto>> RetrieveAllAsync()
        {
            var languages = await _languageRepository.SelectAll(c => c.IsDeleted == false)
                .Where(u => u.IsDeleted == false)
                .ToListAsync();

            return _mapper.Map<IEnumerable<LanguageForResultDto>>(languages);
        }

        public async Task<LanguageForResultDto> RetrieveByIdAsync(long id)
        {
            var language = await _languageRepository.SelectAsync(u => u.Id == id);
            if (language is null || language.IsDeleted)
                throw new CustomException(404, "Language Not Found");

            return _mapper.Map<LanguageForResultDto>(language);
        }
        public async Task<LanguageForResultDto> ModifyAsync(long id, LanguageForUpdateDto dto)
        {
            var language = await _languageRepository.SelectAsync(u => u.Id == id);
            if (language is null || language.IsDeleted)
                throw new CustomException(404, "Couldn't found language for given Id");

            var modifiedModel = _mapper.Map(dto, language);
            modifiedModel.UpdatedAt = DateTime.UtcNow;

            await _languageRepository.SaveAsync();

            return _mapper.Map<LanguageForResultDto>(language);

        }
        public async Task<bool> RemoveAsync(long id)
        {
            var language = await _languageRepository.SelectAsync(u => u.Id == id);
            if (language is null || language.IsDeleted)
            {
                throw new CustomException(404, "Couldn't find language for this given Id");
            }
            await _languageRepository.DeleteAsync(u => u.Id == id);

            return true;
        }
    }
}
