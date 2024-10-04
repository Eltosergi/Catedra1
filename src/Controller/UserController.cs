using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catedra_1.src.Dtos;
using Catedra_1.src.Interface;
using Catedra_1.src.Mappers;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

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

        [HttpGet]
        public async Task<IActionResult> GetAll(string sort = null, string gender = null)
        {
            var users = await _UserRepository.GetAll();
            if (!string.IsNullOrEmpty(gender))
            {
                users = users.Where(u => u.Gender.Equals(gender, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(sort))
            {
                users = sort.ToLower() == "asc" 
                    ? users.OrderBy(u => u.Name).ToList() 
                    : users.OrderByDescending(u => u.Name).ToList();
            }

            var userDtos = users.Select(p => p.ToUserDto());
            
            return Ok(userDtos);
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
            var existingUser = await _UserRepository.GetByRut(userModel.Rut);
            if (existingUser != null)
            {
                return Conflict("El RUT ya existe."); 
            }
            if(userModel.Name.Length < 3 || !isValid(userModel.Email) || !EsFechaValida(userModel.BirthDay) || !isValidGenere(userModel.Gender)){
                return BadRequest("Alguna validación no fue cumplida.");
            }

            await _UserRepository.Post(userModel);
            return CreatedAtAction(nameof(GetById), new { id = userModel.Id }, userModel.ToUserDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var user = await _UserRepository.Delete(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateUserRequestDto updateDto)
        {
            var userModel = await _UserRepository.Put(id, updateDto);
            if (userModel == null)
            {
                return NotFound();
            }

            if(userModel.Name.Length < 3 || !isValid(userModel.Email) || !isValidGenere(userModel.Gender) || !EsFechaValida(userModel.BirthDay)){
                return BadRequest("Alguna validación no fue cumplida.");
            }
            return Ok(userModel.ToUserDto());
        }


        public static bool isValid(string email)
        {
            
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string patronCorreo = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, patronCorreo);
        }
        public static bool EsFechaValida(string fechaNacimiento)
        {
            
            string formato = "dd/MM/yyyy";
            
            
            if (DateTime.TryParseExact(fechaNacimiento, formato, 
                                    System.Globalization.CultureInfo.InvariantCulture, 
                                    System.Globalization.DateTimeStyles.None, 
                                    out DateTime fechaConvertida))
            {
                
                if (fechaConvertida < DateTime.Now)
                {
                    return true; 
                }
            }

            return false; 
        }
        
        public static bool isValidGenere(string genere){
            if (genere == "masculino"|| genere =="femenino" || genere == "otro" || genere == "prefiero no decirlo"){
                return true;
            }
            return false;
        }



    }

    
}