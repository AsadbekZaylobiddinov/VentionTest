using Microsoft.AspNetCore.Mvc;
using VentionTest.API.Models;
using VentionTest.Service.DTOs.Country;
using VentionTest.Service.DTOs.Language;
using VentionTest.Service.Interfaces;

namespace VentionTest.API.Controllers
{
    public class LanguageController : BaseController
    {
        private readonly ILanguageService languageService;
        public LanguageController(ILanguageService languageService)
        {
            this.languageService = languageService;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        public async Task<ActionResult<List<LanguageForResultDto>>> GetAllAsync()
            => Ok(new Response
            {
                Code = 200,
                Message = "OK",
                Data = await languageService.RetrieveAllAsync()
            });

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<LanguageForResultDto>> GetByIdAsync(long id)
            => Ok(new Response
            {
                Code = 200,
                Message = "OK",
                Data = await languageService.RetrieveByIdAsync(id)
            });

        /// <summary>
        /// Create new users
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<LanguageForResultDto>> PostAsync(LanguageForCreationDto dto)
            => Ok(new Response
            {
                Code = 200,
                Message = "OK",
                Data = await languageService.AddAsync(dto)
            });

        /// <summary>
        /// Update users info
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// 
        [HttpPut("{id}")]
        public async Task<ActionResult<LanguageForResultDto>> UpdateAsync(long id, LanguageForUpdateDto dto)
            => Ok(new Response
            {
                Code = 200,
                Message = "OK",
                Data = await languageService.ModifyAsync(id, dto)
            });

        /// <summary>
        /// Delete by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(long id)
            => Ok(new Response
            {
                Code = 200,
                Message = "OK",
                Data = await languageService.RemoveAsync(id)
            });
    }
}
