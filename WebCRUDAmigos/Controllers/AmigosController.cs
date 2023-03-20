using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebCRUDAmigos.Data;
using WebCRUDAmigos.Model;

namespace WebCRUDAmigos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AmigosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AmigosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Criar")]
        public async Task<ActionResult<Amigo>> Create(Amigo amigo)
        {
            try
            {
                _context.Amigos.Add(amigo);
                await _context.SaveChangesAsync();

                return Ok(amigo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAmigos")]
        public async Task<ActionResult<Amigo>> GetAmigos()
        {
            try
            {
                return Ok(_context.GetAmigos());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Atualizar/{Id}")]
        public async Task<ActionResult<Amigo>> AtualizarAmigo(Amigo amigo, int Id)
        {
            try
            {
                return Ok(_context.PutAmigo(Id, amigo));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        [Route("Deletar/{Id}")]
        public async Task<ActionResult<Amigo>> DeletarAmigo(int Id)
        {
            try
            {
                return Ok(_context.DeletarAmigo(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
