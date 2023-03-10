using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfEditFilms
{
	internal class Config
	{
		//Для тестов
		private readonly static string UrlWebApi = "https://localhost:7036/api";
		
		internal readonly static string URLWebApiAccounts = $"{UrlWebApi}/accounts";

		internal readonly static string URLWebApiFiles = $"{UrlWebApi}/files";

		internal readonly static string URLWebApiFilms = $"{UrlWebApi}/films";

		internal readonly static string URLWebApiTrailers = $"{UrlWebApi}/trailers";




		internal readonly static string CurrentDirectory = System.IO.Directory.GetCurrentDirectory();

		internal readonly static string FileSettingJson = $"{CurrentDirectory}\\Settings.json";

		internal readonly static string DirectoryLogs = $"{CurrentDirectory}\\Logs";
	}
}
