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
    public class UserController
    {
        private readonly IUserRepository _UserRepository;
        public UserController(IUserRepository userRepository)
        {
           _UserRepository = userRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserRquestDto userRquestDto)
        {
            var userModel = userRquestDto.ToUserFromCreateDto();
            await _productRepository.Post(productModel);
            return CreatedAtAction(nameof(GetById), new { id = productModel.Id }, productModel.ToProductDto());
        }
    }

    public interface IProductRepository
    {
    }
}