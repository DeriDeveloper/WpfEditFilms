using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfEditFilms.Models
{
	public class Video
	{
		public int Id { get; set; }
		public string? FileGuid { get; set; }
		//public File? File { get; set; }
		public int Duration { get; set; }
	}
}
