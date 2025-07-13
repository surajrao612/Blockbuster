using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockbuster.Domain.Entities;

public class Movie
{
    public string Title { get; set; } = string.Empty;
    public string Year { get; set; } = string.Empty;
    public string ID { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Poster { get; set; } = string.Empty;
}

