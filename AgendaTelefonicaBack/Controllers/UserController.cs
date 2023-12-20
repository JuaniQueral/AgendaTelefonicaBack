using AgendaTelefonicaBack.Data.Interfaces;
using AgendaTelefonicaBack.Models;
using AgendaTelefonicaBack.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaTelefonicaBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly InterfazUsuario _interfazUsuario;


        public UserController(IMapper mapper, InterfazUsuario interfazUsuario)
        {
            _mapper = mapper;
            _interfazUsuario = interfazUsuario;
        }



        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var Listuser = await _interfazUsuario.GetListUsuario();

                var ListuserDTO = _mapper.Map<List<UsuarioDTO>>(Listuser);

                return Ok(ListuserDTO);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetOneById(int id)
        {
            try
            {
                var user = await _interfazUsuario.GetUsuarioById(id);

                if (user == null)
                {
                    return NotFound();
                }
                var userDTO = _mapper.Map<UsuarioDTO>(user);
                return Ok(userDTO);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                int UserId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);


                if (UserId != id)
                {
                    return BadRequest();
                }

                var user = await _interfazUsuario.GetUsuarioById(id);

                if (user == null)
                {
                    return NotFound();
                }

                await _interfazUsuario.DeleteUser(user);

                return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]

        public async Task<IActionResult> NewUser(UsuarioDTO userDTO)
        {
            try
            {
                var user = _mapper.Map<Usuario>(userDTO);

                var userr = await _interfazUsuario.AddUser(user);

                var userItem = _mapper.Map<UsuarioDTO>(userr);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> EditUser(UsuarioDTO userDTO)
        {
            try
            {
                int UserId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);

                if (UserId != userDTO.Id)
                {
                    return BadRequest();
                }

                var user = _mapper.Map<Usuario>(userDTO);

                await _interfazUsuario.UpdateUsuario(user);

                return NoContent();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }


    }


}


