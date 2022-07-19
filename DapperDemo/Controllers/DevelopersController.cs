using DapperDemo.Core.Model;
using DapperDemo.GlobalException.ErrorModel;
using DapperDemo.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperDemo.Controllers
{
    [Route("api/v1/developers")]
    [ApiController]
    public class DevelopersController : ControllerBase
    {
        private readonly IDeveloperService _developerService;

        public DevelopersController(IDeveloperService developerService)
        {
            _developerService = developerService;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDevelopers()
        {
            var developers = await _developerService.GetAllDevelopersAsync();

            if (developers == null)
            {
                throw new CustomException("An error Occurs");
            }

            return Ok(developers);
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int Id)
        {
            var developer = await _developerService.GetDeveloperByIdAsync(Id);

            return Ok(developer);
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var developer = await _developerService.GetDeveloperByEmail(email);

            return Ok(developer);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult AddDeveloper([FromBody] Developer developer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model not valid");
            }

             _developerService.AddDeveloper(developer);

            return CreatedAtAction(nameof(GetById), new { Id = developer.Id }, developer);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult UpdateDeveloper([FromBody] Developer developer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model not valid");
            }

            _developerService.UpdateDeveloper(developer);

            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DeleteDeveloper(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model not valid");
            }

            _developerService.DeleteDeveloper(id);

            return Ok();
        }
    }
}
