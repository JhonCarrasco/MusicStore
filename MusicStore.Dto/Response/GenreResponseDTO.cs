namespace MusicStore.Dto.Response
{
    public class GenreResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public bool Status { get; set; } = true;
    }
}
