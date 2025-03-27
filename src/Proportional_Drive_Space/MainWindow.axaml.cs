using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using Avalonia.Media;
using Avalonia;
using System.Runtime.InteropServices;
using Avalonia.Interactivity;
using DrivePlot;

namespace SingleAvaloniaApp
{
    public partial class MainWindow : Window
    {
        private Random _random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            GenerateAndDisplayImage();
        }

        private void OnRefreshButtonClick(object sender, RoutedEventArgs e)
        {
            GenerateAndDisplayImage();
        }

        private void GenerateAndDisplayImage()
        {
            //var bitmap = GenerateRandomImage(width, height);
            string plot_filepath = Plot_Generator.from_width_get_filepath(800);
            var bitmap = new Bitmap(plot_filepath);

            DisplayImage.Source = bitmap;

            this.Width = bitmap.Size.Width;
            this.Height = bitmap.Size.Height + 50;
        }

        private Bitmap GenerateRandomImage(int width, int height)
        {
            var pixels = new int[width * height];

            for (int i = 0; i < pixels.Length; i++)
            {
                var color = Color.FromArgb(
                    255,
                    (byte)_random.Next(256),
                    (byte)_random.Next(256),
                    (byte)_random.Next(256)
                );
                pixels[i] = (int)color.ToUInt32();
            }

            Vector dpi = new Vector(96, 96); 
  
            var bitmap = new WriteableBitmap( 
                new PixelSize(width, height), 
                dpi, 
                PixelFormat.Bgra8888, 
                AlphaFormat.Premul); 
  
            using (var frameBuffer = bitmap.Lock()) 
            { 
                Marshal.Copy(pixels, 0, frameBuffer.Address, pixels.Length); 
            } 
  
            return bitmap;
        }
    }
}