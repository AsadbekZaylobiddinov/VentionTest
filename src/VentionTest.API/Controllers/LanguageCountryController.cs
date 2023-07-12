using Microsoft.AspNetCore.Mvc;
using VentionTest.API.Models;
using VentionTest.Service.DTOs.Language;
using VentionTest.Service.DTOs.LanguageCountry;
using VentionTest.Service.Interfaces;

namespace VentionTest.API.Controllers
{
    public class LanguageCountryController : BaseController
    {
        private readonly ILanguageCountryService languageCountryService;
        public LanguageCountryController(ILanguageCountryService languageCountryService)
        {
            this.languageCountryService = languageCountryService;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        public async Task<ActionResult<List<LanguageCountryForResultDto>>> GetAllAsync()
            => Ok(new Response
            {
                Code = 200,
                Message = "OK",
                Data = await languageCountryService.RetrieveAllAsync()
            });

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(long id)
            => Ok(new Response
            {
                Code = 200,
                Message = "OK",
                Data = await languageCountryService.RetrieveByIdAsync(id)
            });

        /// <summary>
        /// Create new users
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<LanguageCountryForResultDto>> PostAsync(LanguageCountryForCreationDto dto)
            => Ok(new Response
            {
                Code = 200,
                Message = "OK",
                Data = await languageCountryService.AddAsync(dto)
            });

        /// <summary>
        /// Update users info
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// 
        [HttpPut("{id}")]
        public async Task<ActionResult<LanguageCountryForResultDto>> UpdateAsync(long id, LanguageCountryForUpdateDto dto)
            => Ok(new Response
            {
                Code = 200,
                Message = "OK",
                Data = await languageCountryService.ModifyAsync(id, dto)
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
                Data = await languageCountryService.RemoveAsync(id)
            });
    }
}
