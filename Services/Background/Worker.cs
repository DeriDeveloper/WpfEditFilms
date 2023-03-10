using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using WpfEditFilms.Models;
using static System.Net.WebRequestMethods;

namespace WpfEditFilms.Services.Background
{
	internal class Worker
	{

		internal static async Task<Models.Account?> AuthAsync(string login, string password)
		{
			try
			{
				using (var client = new HttpClient())
				{
					var response = await client.GetAsync(Config.URLWebApiAccounts + $"?login={login}&password={password}");

					switch (response.StatusCode)
					{
						case System.Net.HttpStatusCode.OK:
							{
								var jsonResponse = await response.Content.ReadAsStringAsync();

								var account = JsonConvert.DeserializeObject<Account>(jsonResponse);

								return account;
							}
						default:
							{
								MessageBox.Show("Неверный логин или пароль");
								return null;
							}
					}
				}
			}
			catch (Exception error)
			{
				Services.Background.Worker.AddLog(error);

				MessageBox.Show(error.Message);

				return null;
			}
		}

		internal static void AddLog(Exception error)
		{
			var date = DateTime.Now.ToString("yyyy-MM-dd");

			var path = Config.DirectoryLogs + "\\" + date + ".txt";

			if (!System.IO.File.Exists(path))
				System.IO.File.WriteAllText(path, "");

			System.IO.File.AppendAllText(path, error.ToString()+"\n\n");

		}

		internal static async Task<bool> CheckAuthTokenAccessAsync(int id, string tokenAccess)
		{
			try
			{
				using (var client = new HttpClient())
				{
					var response = await client.GetAsync(Config.URLWebApiAccounts + $"/{id}?token_access={tokenAccess}");

					switch (response.StatusCode)
					{
						case System.Net.HttpStatusCode.OK:
							{
								return true;
							}
						default:
							{
								return false;
							}
					}
				}
			}
			catch (Exception error)
			{
				Services.Background.Worker.AddLog(error);

				MessageBox.Show(error.Message);

				return false;
			}
		}

		internal static Settings GetSettings()
		{
			return JsonConvert.DeserializeObject<Settings>(System.IO.File.ReadAllText(Config.FileSettingJson));
		}

		internal static void SetSettings(Settings settings)
		{
			System.IO.File.WriteAllText(Config.FileSettingJson, JsonConvert.SerializeObject(settings, Formatting.Indented));
		}

		internal static async Task<string?> UploadFileToServerAsync(string path)
		{


			try
			{
				var settings = Services.Background.Worker.GetSettings();

				if (settings?.Account?.Id != 0)
				{
					var client = new HttpClient();
					var request = new HttpRequestMessage(HttpMethod.Post, Config.URLWebApiFiles + $"?user_id={settings.Account.Id}&token_access={settings.Account.TokenAccess}");
					var content = new MultipartFormDataContent();
					content.Add(new StreamContent(System.IO.File.OpenRead(path)), "file", path);
					request.Content = content;
					var response = await client.SendAsync(request);
					response.EnsureSuccessStatusCode();
					var guidFile = await response.Content.ReadAsStringAsync();

					return guidFile;
				}
				else
				{
					MessageBox.Show("Выйдете и зайдите в аккаунт заново");
				}
			}
			catch (Exception error)
			{
				Services.Background.Worker.AddLog(error);

				MessageBox.Show(error.Message);

			}

			return null;
		}

		internal static async Task<bool> AddUploadFilmToServerAsync(Film film)
		{
			try
			{
				var jsonContent = JsonConvert.SerializeObject(film);

				var srttings = Services.Background.Worker.GetSettings();

				var client = new HttpClient();
				var request = new HttpRequestMessage(HttpMethod.Post, $"{Config.URLWebApiFilms}?user_id={srttings.Account.Id}&token_access={srttings.Account.TokenAccess}");
				var content = new StringContent(jsonContent, null, "application/json");
				request.Content = content;
				var response = await client.SendAsync(request);
				response.EnsureSuccessStatusCode();

				var responseString  = await response.Content.ReadAsStringAsync();

				var asd = 0;
			}
			catch (Exception error)
			{
				Services.Background.Worker.AddLog(error);

				MessageBox.Show(error.Message);
			}

			return false;
		}

		internal static async Task<Trailer?> AddUploadTrailerToServerAsync(Trailer trailer)
		{
			try
			{
				var jsonContent = JsonConvert.SerializeObject(trailer);

				var srttings = Services.Background.Worker.GetSettings();

				var client = new HttpClient();
				var request = new HttpRequestMessage(HttpMethod.Post, $"{Config.URLWebApiTrailers}?user_id={srttings.Account.Id}&token_access={srttings.Account.TokenAccess}");
				var content = new StringContent(jsonContent, null, "application/json");
				request.Content = content;
				var response = await client.SendAsync(request);
				response.EnsureSuccessStatusCode();

				var responseString = await response.Content.ReadAsStringAsync();

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{ 
					return JsonConvert.DeserializeObject<Trailer>(responseString);
				}
				else
				{
					MessageBox.Show(responseString);

					return null;
				}
			}
			catch (Exception error)
			{
				Services.Background.Worker.AddLog(error);

				MessageBox.Show(error.Message);
			}

			return null;
		}
	}
}
