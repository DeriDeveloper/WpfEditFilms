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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfEditFilms;

namespace WpfEditFilms
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class AuthWindow : Window
	{

		private bool LoadedWindow { get; set; } = false;

		public AuthWindow()
		{
			InitializeComponent();
		}

		private async void EnterButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var login = LoginTextBox.Text;
				var password = PasswordBox.Password;

				if (string.IsNullOrEmpty(login))
				{
					MessageBox.Show("Логин не должен быть пустым!");
					return;
				}

				if (string.IsNullOrEmpty(password))
				{
					MessageBox.Show("Пароль не должен быть пустым!");
					return;
				}

				var account = await WpfEditFilms.Services.Background.Worker.AuthAsync(login, password);

				if (account != null)
				{
					var settings = WpfEditFilms.Services.Background.Worker.GetSettings();

					settings.Account = account;

					WpfEditFilms.Services.Background.Worker.SetSettings(settings);

					new MainWindow().Show();

					this.Close();
				}
			}
			catch (Exception error)
			{
				Services.Background.Worker.AddLog(error);

				MessageBox.Show(error.Message);
			}
		}

		private void CheckFilledFields()
		{
			bool statusLogin = false;
			bool statusPassword = false;


			if (LoginTextBox.Text.Length > 0)
				statusLogin = true;

			if (PasswordBox.Password.Length > 0)
				statusPassword = true;


			if (statusLogin && statusPassword)
			{
				EnterButton.IsEnabled = true;
			}
			else
			{
				EnterButton.IsEnabled = false;
			}
		}

		private void LoginTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (!LoadedWindow)
				return;

			CheckFilledFields();
		}

		private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
		{
			if (!LoadedWindow)
				return;


			CheckFilledFields();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			LoadedWindow = true;
		}
	}
}
