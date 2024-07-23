using Microsoft.EntityFrameworkCore;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Entities;
using MusicStore.Persistence;

namespace MusicStore.Repositories.Implements
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext context;

        public GenreRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<GenreResponseDTO>> GetAsync()
        {
            var items = await context.Genres.ToListAsync();

            // Mapping
            var genreResponseDTO = items.Select(x => new GenreResponseDTO
            {
                Id = x.Id,
                Name = x.Name,
                Status = x.Status
            }).ToList();
            
            return genreResponseDTO;
        }

        public async Task<GenreResponseDTO?> GetAsync(int id)
        {
            var item = await context.Genres.FirstOrDefaultAsync(x => x.Id == id);
            var genreResponseDTO = new GenreResponseDTO();
            if (item is not null)
            {
                // Mapping
                genreResponseDTO.Id = item.Id;
                genreResponseDTO.Name = item.Name;
                genreResponseDTO.Status = item.Status;
            }
            else
            {
                throw new InvalidOperationException($"no se encontró el registro con id: {id}");
            }

            return genreResponseDTO;
        }

        public async Task<int> AddAsync(GenreRequestDTO genreRequestDTO)
        {
            var genre = new Genre()
            {
                Name = genreRequestDTO.Name,
                Status = genreRequestDTO.Status
            };

            context.Genres.Add(genre);
            await context.SaveChangesAsync();
            return genre.Id;
        }

        public async Task UpdateAsync(int id, GenreRequestDTO genreRequestDTO)
        {
            var item = await context.Genres.FirstOrDefaultAsync(x => x.Id == id);


            if (item is not null)
            {
                item.Name = genreRequestDTO.Name;
                item.Status = genreRequestDTO.Status;
                context.Genres.Update(item);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"no se encontró el registro con id: {id}");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var item = await context.Genres.FirstOrDefaultAsync(x => x.Id == id);
            if (item is not null)
            {
                context.Genres.Remove(item);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"no se encontró el registro con id: {id}");
            }
        }
    }
}
