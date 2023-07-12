using Microsoft.AspNetCore.Mvc;
using VentionTest.API.Models;
using VentionTest.Service.DTOs.City;
using VentionTest.Service.DTOs.Country;
using VentionTest.Service.Interfaces;

namespace VentionTest.API.Controllers
{
    public class CountryController : BaseController
    {
        private readonly ICountryService countryService;
        public CountryController(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        public async Task<ActionResult<List<CountryForResultDto>>> GetAllAsync()
            => Ok(new Response
            {
                Code = 200,
                Message = "OK",
                Data = await countryService.RetrieveAllAsync()
            });

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryForResultDto>> GetByIdAsync(long id)
            => Ok(new Response
            {
                Code = 200,
                Message = "OK",
                Data = await countryService.RetrieveByIdAsync(id)
            });

        /// <summary>
        /// Create new users
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CountryForResultDto>> PostAsync(CountryForCreationDto dto)
            => Ok(new Response
            {
                Code = 200,
                Message = "OK",
                Data = await countryService.AddAsync(dto)
            });

        /// <summary>
        /// Update users info
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// 
        [HttpPut("{id}")]
        public async Task<ActionResult<CountryForResultDto>> UpdateAsync(long id, CountryForUpdateDto dto)
            => Ok(new Response
            {
                Code = 200,
                Message = "OK",
                Data = await countryService.ModifyAsync(id, dto)
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
                Data = await countryService.RemoveAsync(id)
            });
    }
}
