using MusicStore.Dto.Request;
using MusicStore.Dto.Response;

namespace MusicStore.Repositories
{
    public interface IGenreRepository
    {
        Task<int> AddAsync(GenreRequestDTO genre);
        Task DeleteAsync(int id);
        Task<List<GenreResponseDTO>> GetAsync();
        Task<GenreResponseDTO?> GetAsync(int id);
        Task UpdateAsync(int id, GenreRequestDTO genre);
    }
}