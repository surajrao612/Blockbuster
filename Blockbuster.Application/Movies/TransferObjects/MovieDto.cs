using static Blockbuster.Domain.Enums.Enums;

namespace Blockbuster.Application.Movies.TransferObjects
{
    public class MovieDto
    {
        public string Title { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public string ID { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Poster { get; set; } = string.Empty;
        public MovieProvider MovieProvider { get; set; }
    }
}
