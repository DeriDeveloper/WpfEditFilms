using Microsoft.Win32;
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
using WpfEditFilms.Models;

namespace WpfEditFilms
{
	public partial class EditFilmWindow : Window
	{
		private Film? Film { get; set; }
		private Trailer? Trailer { get; set; }

		private string PathPreviewFilmImage { get; set; }
		private string PathFilmVideo { get; set; }
		private string PathPreviewTrailerImage { get; set; }
		private string PathTrailerVideo { get; set; }


		public EditFilmWindow(Film? film = null, Trailer? trailer = null)
		{
			InitializeComponent();


			this.Film = film;
			this.Trailer = trailer;

			if (this.Film != null)
			{
				UploadDataFilm();
			}

			if (this.Trailer != null)
			{
				UploadDataTrailer();
			}
		}

		private void UploadDataTrailer()
		{

		}
		private void UploadDataFilm()
		{

		}

		private void PreviewFilmImageClearButton_Click(object sender, RoutedEventArgs e)
		{
			PathPreviewFilmImage = "";
			PreviewFilmImage.Source = null;
		}
		private void PreviewTrailerImageClearButton_Click(object sender, RoutedEventArgs e)
		{
			PathPreviewTrailerImage = "";
			PreviewTrailerImage.Source = null;
		}
		private void VideoTrailerClearButton_Click(object sender, RoutedEventArgs e)
		{
			PathTrailerVideo = "";
			VideoTrailerLabelName.Content = "";
		}
		private void VideoFilmClearButton_Click(object sender, RoutedEventArgs e)
		{
			PathTrailerVideo = "";
			VideoTrailerLabelName.Content = "";
		}

		private void PreviewFilmImageUploadButton_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog()
			{
				Filter = "Image file (*.jpg)|*.jpg"
			};

			if (openFileDialog.ShowDialog() == true)
			{
				PathPreviewFilmImage = openFileDialog.FileName;
			}

			UpdatePreviewFilmImage();
		}
		private void PreviewTrailerImageUploadButton_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog()
			{
				Filter = "Image file (*.jpg)|*.jpg"
			};

			if (openFileDialog.ShowDialog() == true)
			{
				PathPreviewTrailerImage = openFileDialog.FileName;
			}

			UpdatePreviewTrailerImage();
		}
		private void VideoTrailerUploadButton_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog()
			{
				Filter = "Videos file (*.mp4)|*.mp4"
			};

			if (openFileDialog.ShowDialog() == true)
			{
				PathTrailerVideo = openFileDialog.FileName;
			}

			UpdateTrailerVideo();
		}
		private void VideoFilmUploadButton_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog()
			{
				Filter = "Videos file (*.mp4)|*.mp4"
			};

			if (openFileDialog.ShowDialog() == true)
			{
				PathFilmVideo = openFileDialog.FileName;
			}

			UpdateFilmVideo();
		}

		private void UpdateTrailerVideo()
		{
			if (string.IsNullOrEmpty(PathTrailerVideo))
				return;

			if (System.IO.File.Exists(PathTrailerVideo))
			{
				VideoTrailerLabelName.Content = PathTrailerVideo;
			}
		}


		private void UpdateFilmVideo()
		{
			if (string.IsNullOrEmpty(PathFilmVideo))
				return;

			if (System.IO.File.Exists(PathFilmVideo))
			{
				VideoFilmLabelName.Content = PathFilmVideo;
			}
		}

		private void UpdatePreviewFilmImage()
		{
			if (string.IsNullOrEmpty(PathPreviewFilmImage))
				return;

			if (System.IO.File.Exists(PathPreviewFilmImage))
			{
				PreviewFilmImage.Source = new BitmapImage(new Uri(PathPreviewFilmImage));
			}
		}
		private void UpdatePreviewTrailerImage()
		{
			if (string.IsNullOrEmpty(PathPreviewTrailerImage))
				return;

			if (System.IO.File.Exists(PathPreviewTrailerImage))
			{
				PreviewTrailerImage.Source = new BitmapImage(new Uri(PathPreviewTrailerImage));
			}
		}

		private void MainWindowMenuItem_Click(object sender, RoutedEventArgs e)
		{
			new MainWindow().Show();

			this.Close();
		}

		private async void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				SaveButton.IsEnabled = false;



				// фильм

				/*
				var nameFilm = NameFilmTextBox.Text;
				var descriptionFilm = DescriptionFilmTextBox.Text;

				if (Film == null)
					Film = new Film();

				Film.Name = nameFilm;
				Film.Description = descriptionFilm;

				if(PathPreviewFilmImage == null)
				{
					MessageBox.Show("Не выбрано превью");
					return;
				}

				if (Film.Preview == null)
					Film.Preview = new Models.Image();


				Film.Preview.FileGuid = await Services.Background.Worker.UploadFileToServerAsync(PathPreviewFilmImage);


				var durationVideoFilmString = DurationVideoFilmTextBox.Text;
				var durationVideoFilm = 0;

				int.TryParse(durationVideoFilmString, out durationVideoFilm);



				if (durationVideoFilm <= 0)
				{
					MessageBox.Show("Длительность видео фильма не должно быть меньше или равно 0");
					return;
				}

				if (Film.Video == null)
					Film.Video = new Video();

				Film.Video.Duration = durationVideoFilm;

				if (PathFilmVideo == null)
				{
					MessageBox.Show("Не выбрано фильм видео");
					return;
				}


				Film.Video.FileGuid = await Services.Background.Worker.UploadFileToServerAsync(PathFilmVideo);
				*/


				//трейлер


				var nameTrailer = NameTrailerTextBox.Text;
				var descriptionTrailer = DescriptionTrailerTextBox.Text;

				if (Trailer == null)
					Trailer = new Trailer();

				Trailer.Name = nameTrailer;
				Trailer.Description = descriptionTrailer;


				if (PathPreviewTrailerImage == null)
				{
					MessageBox.Show("Не выбрано превью трейлера");
					return;
				}

				if (Trailer.Preview == null)
					Trailer.Preview = new Models.Image();

				Trailer.Preview.FileGuid = await Services.Background.Worker.UploadFileToServerAsync(PathPreviewTrailerImage);




				if (PathTrailerVideo == null)
				{
					MessageBox.Show("Не выбрано трейлер видео");
					return;
				}

				if (Trailer.Video == null)
					Trailer.Video = new Video();

				Trailer.Video.FileGuid = await Services.Background.Worker.UploadFileToServerAsync(PathTrailerVideo);


				var durationVideoTreilerString = DurationVideoTrailerTextBox.Text;
				var durationVideoTreiler = 0;

				int.TryParse(durationVideoTreilerString, out durationVideoTreiler);



				if (durationVideoTreiler <= 0)
				{
					MessageBox.Show("Длительность видео трейлера не должно быть меньше или равно 0");
					return;
				}


				Trailer.Video.Duration = durationVideoTreiler;

				var trailerNew = await Services.Background.Worker.AddUploadTrailerToServerAsync(Trailer);

				if (trailerNew != null)
				{
					Trailer = trailerNew;
				}


				/*if (await Services.Background.Worker.AddUploadFilmToServerAsync(Film))
				{

				}
				else
				{

				}*/


				SaveButton.IsEnabled = true;
			}
			catch (Exception error)
			{
				Services.Background.Worker.AddLog(error);

				MessageBox.Show("Произошла не известная ошибка при сохранении!");

				SaveButton.IsEnabled = true;
			}
		}
	}
}
