using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfEditFilms.Models
{
	public class Trailer
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public int PreviewId { get; set; }
		public Image? Preview { get; set; } = new Image();
		public List<FilmTag>? Tages { get; set; } = new List<FilmTag>();
		public int VideoId { get; set; }
		public Video? Video { get; set; } = new Video();
	}
}
