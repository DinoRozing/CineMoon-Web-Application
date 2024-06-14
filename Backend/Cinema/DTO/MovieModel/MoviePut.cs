namespace DTO.MovieModel;

public class MoviePut
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Title { get; set; }
    public string? Genre { get; set; }
    public string? Description { get; set; }
    public int Duration { get; set; }
    public string? Language { get; set; }
    public string? CoverUrl { get; set; }
    public string? TrailerUrl { get; set; }
    public List<string>? ActorNames { get; set; }
}