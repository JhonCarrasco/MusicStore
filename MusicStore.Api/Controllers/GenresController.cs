using Azure;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Entities;
using MusicStore.Repositories;
using System.Net;

namespace MusicStore.Api.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreRepository repository;
        private readonly ILogger<GenresController> logger;

        public GenresController(IGenreRepository repository, ILogger<GenresController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = new BaseResponseGeneric<ICollection<GenreResponseDTO>>();
            try
            {
                response.Data = await repository.GetAsync();
                response.Success = true;

                logger.LogInformation("Obteniendo todos los géneros musicales.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al obtener la información.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
                
                return BadRequest(response);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = new BaseResponseGeneric<GenreResponseDTO>();
            try
            {
                response.Data = await repository.GetAsync(id);
                response.Success = true;

                logger.LogInformation("Obteniendo el género musical.");
                return response.Data is not null ? Ok(response) : NotFound();
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al obtener la información.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");

                return BadRequest(response);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(GenreRequestDTO genreRequestDTO)
        {
            var response = new BaseResponseGeneric<int>();
            try
            {
                var id = await repository.AddAsync(genreRequestDTO);
                response.Data =id;
                response.Success = true;

                logger.LogInformation($"Se ha creado el género musical con el id {id}.");
                return StatusCode((int)HttpStatusCode.Created, response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al crear el género musical.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");

                return BadRequest(response);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, GenreRequestDTO genreRequestDTO)
        {
            var response = new BaseResponse();
            try
            {
                await repository.UpdateAsync(id, genreRequestDTO);
                response.Success = true;

                logger.LogInformation($"Se ha actualizado el género musical con el id {id}.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al actualizar el género musical.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");

                return BadRequest(response);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = new BaseResponse();
            try
            {
                await repository.DeleteAsync(id);
                response.Success = true;

                logger.LogInformation($"Se ha borrado el género musical con el id {id}.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al borrar el género musical.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");

                return BadRequest(response);
            }
        }
    }
}
