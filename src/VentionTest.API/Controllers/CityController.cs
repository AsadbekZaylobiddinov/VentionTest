using Microsoft.AspNetCore.Mvc;
using VentionTest.API.Models;
using VentionTest.Service.DTOs.Capital;
using VentionTest.Service.DTOs.City;
using VentionTest.Service.Interfaces;

namespace VentionTest.API.Controllers
{
    public class CityController : BaseController
    {
        private readonly ICityService cityService;
        public CityController(ICityService cityService)
        {
            this.cityService = cityService;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        public async Task<ActionResult<List<CityForResultDto>>> GetAllAsync([FromQuery] long countryId)
            => Ok(new Response
            {
                Code = 200,
                Message = "OK",
                Data = await cityService.RetrieveAllAsync(countryId)
            });

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CityForResultDto>> GetByIdAsync(long id)
            => Ok(new Response
            {
                Code = 200,
                Message = "OK",
                Data = await cityService.RetrieveByIdAsync(id)
            });

        /// <summary>
        /// Create new users
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CityForResultDto>> PostAsync(CityForCreationDto dto)
            => Ok(new Response
            {
                Code = 200,
                Message = "OK",
                Data = await cityService.AddAsync(dto)
            });

        /// <summary>
        /// Update users info
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// 
        [HttpPut("{id}")]
        public async Task<ActionResult<CityForResultDto>> UpdateAsync(long id, CityForUpdateDto dto)
            => Ok(new Response
            {
                Code = 200,
                Message = "OK",
                Data = await cityService.ModifyAsync(id, dto)
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
                Data = await cityService.RemoveAsync(id)
            });
    }
}
