using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catedra_1.src.Dtos;
using Catedra_1.src.Interface;
using Catedra_1.src.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Catedra_1.src.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly IUserRepository _UserRepository;
        public UserController(IUserRepository userRepository)
        {
           _UserRepository = userRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var user = await _UserRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user.ToUserDto());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserRquestDto userRquestDto)
        {
            var userModel = userRquestDto.ToUserFromCreateDto();
            await _UserRepository.Post(userModel);
            return CreatedAtAction(nameof(GetById), new { id = userModel.Id }, userModel.ToUserDto());
        }
    }

    
}