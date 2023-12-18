using AutoMapper;
using AgendaTelefonicaBack.Models;
using AgendaTelefonicaBack.Models.DTO;
using AgendaTelefonicaBack.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using AgendaTelefonicaBack.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace AgendaTelefonicaBack.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactoController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly InterfazContact _contactoRepository;

        public ContactoController(IMapper mapper, InterfazContact contactoRepository)
        {
            _mapper = mapper;
            _contactoRepository = contactoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                int UserId = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);

                var listContactos = await _contactoRepository.GetListContacto(UserId);

                var listContactosDto = _mapper.Map<IEnumerable<ContactoDTO>>(listContactos);

                return Ok(listContactosDto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                int UserId = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);

                var contacto = await _contactoRepository.GetContacto(id);

                if (contacto == null)
                {
                    return NotFound();
                }

                if (contacto.UsuarioId != UserId)
                {
                    return NotFound();
                }

                var contactoDto = _mapper.Map<ContactoDTO>(contacto);

                return Ok(contactoDto);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                int UserId = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);

                var contacto = await _contactoRepository.GetContacto(id);

                if (contacto == null)
                {
                    return NotFound();
                }

                if (contacto.UsuarioId != UserId)
                {
                    return NotFound();
                }

                await _contactoRepository.DeleteContacto(contacto);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(ContactoDTO contactoDto)
        {
            try
            {
                int UserId = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);

                var contacto = _mapper.Map<Contacto>(contactoDto);

                contacto.FechaCreacion = DateTime.Now;

                contacto.UsuarioId = UserId;

                contacto = await _contactoRepository.AddContacto(contacto);

                var contactoItemDto = _mapper.Map<ContactoDTO>(contacto);

                return CreatedAtAction("Get", new { id = contactoItemDto.Id }, contactoItemDto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ContactoDTO contactoDto)
        {
            try
            {
                int UserId = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);

                var contacto = _mapper.Map<Contacto>(contactoDto);

                if (id != contacto.Id)
                {
                    return BadRequest();
                }

                var contactoItem = await _contactoRepository.GetContacto(id);

                if (contactoItem == null)
                {
                    return NotFound();
                }

                if (contactoItem.UsuarioId != UserId)
                {
                    return NotFound();
                }

                await _contactoRepository.UpdateContacto(contacto);

                return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }

}
