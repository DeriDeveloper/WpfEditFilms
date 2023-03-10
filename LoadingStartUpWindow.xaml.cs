using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfEditFilms
{
	public partial class LoadingStartUpWindow : Window
	{
		public LoadingStartUpWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			Init();
		}

		private async void Init()
		{
			try
			{
				this.WindowState = WindowState.Normal;


				if (!System.IO.File.Exists(Config.FileSettingJson))
					System.IO.File.WriteAllText(Config.FileSettingJson, JsonConvert.SerializeObject(new WpfEditFilms.Models.Settings(), Formatting.Indented));

				if (!System.IO.Directory.Exists(Config.DirectoryLogs))
					System.IO.Directory.CreateDirectory(Config.DirectoryLogs);

				if (!System.IO.File.Exists(Config.FileSettingJson))
					System.IO.File.WriteAllText(Config.FileSettingJson, JsonConvert.SerializeObject(new WpfEditFilms.Models.Settings(), Formatting.Indented));

				var settings = Services.Background.Worker.GetSettings();

				if (string.IsNullOrEmpty(settings?.Account?.TokenAccess))
				{
					new AuthWindow().Show();
				}
				else
				{
					if (await Services.Background.Worker.CheckAuthTokenAccessAsync(settings.Account.Id, settings.Account.TokenAccess))
					{
						new MainWindow().Show();
					}
					else
					{
						settings.Account = null;

						Services.Background.Worker.SetSettings(settings);

						new AuthWindow().Show();
					}
				}

				this.Close();
			}
			catch(Exception error)
			{
				Services.Background.Worker.AddLog(error);

				MessageBox.Show(error.Message);
			}

		}

		
	}
}
