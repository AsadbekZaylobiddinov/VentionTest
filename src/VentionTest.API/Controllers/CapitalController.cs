using Microsoft.AspNetCore.Mvc;
using VentionTest.API.Models;
using VentionTest.Service.DTOs.Capital;
using VentionTest.Service.Interfaces;

namespace VentionTest.API.Controllers
{
    public class CapitalController : BaseController
    {
        private readonly ICapitalService capitalService;
        public CapitalController(ICapitalService userService)
        {
            this.capitalService = userService;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        public async Task<ActionResult<List<CapitalForResultDto>>> GetAllAsync()
            => Ok(new Response
            {
                Code = 200,
                Message = "OK",
                Data = await capitalService.RetrieveAllAsync()
            });

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CapitalForResultDto>> GetByIdAsync(long id)
            => Ok(new Response
            {
                Code = 200,
                Message = "OK",
                Data = await capitalService.RetrieveByIdAsync(id)
            });

        /// <summary>
        /// Create new users
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CapitalForResultDto>> PostAsync(CapitalForCreationDto dto)
            => Ok(new Response
            {
                Code = 200,
                Message = "OK",
                Data = await capitalService.AddAsync(dto)
            });

        /// <summary>
        /// Update users info
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// 
        [HttpPut("{id}")]
        public async Task<ActionResult<CapitalForResultDto>> UpdateAsync(long id, CapitalForUpdateDto dto)
            => Ok(new Response
            {
                Code = 200,
                Message = "OK",
                Data = await capitalService.ModifyAsync(id, dto)
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
                Data = await capitalService.RemoveAsync(id)
            });

    }
}
