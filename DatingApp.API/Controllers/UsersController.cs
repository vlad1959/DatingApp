using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    // url: http://localhost:5000/api/users
    //only authorized user can get access
    [ServiceFilter(typeof(LogUserActivity))] //this will use this class to update activity date whenever any method is called fom this controller
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;

        public UsersController(IDatingRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetUsers();
            var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);
            return Ok(usersToReturn);
        }
        [HttpGet("{id}", Name="GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);
            // will map source, which is user to UserForDetailedDto
            var userToReturn = _mapper.Map<UserForDetailedDto>(user);
            return Ok(userToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto) {
            //if id passed does not match user id in a token - return unauthorized.
           
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
               return Unauthorized();

            var userFromRepo = await _repo.GetUser(id);  
            _mapper.Map(userForUpdateDto, userFromRepo); //will map(update) user from repo (database) with user dto

            if (await _repo.SaveAll())
                return NoContent();
            else
                throw new System.Exception($"Updating user {id} failed on save");
        }
    }
}