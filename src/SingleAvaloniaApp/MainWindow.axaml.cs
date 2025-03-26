using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using Avalonia.Media;
using Avalonia;
using System.Runtime.InteropServices;
using Avalonia.Interactivity;

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
            int width = _random.Next(640, 800 + 1);
            int height = _random.Next(480, 600 + 1);

            var bitmap = GenerateRandomImage(width, height);

            DisplayImage.Source = bitmap;

            this.Width = width;
            this.Height = height + 50;
        }

        private Bitmap GenerateRandomImage(int width, int height)
        {
            var pixels = new int[width * height];

            // Remplir le tableau avec des couleurs aléatoires
            for (int i = 0; i < pixels.Length; i++)
            {
                var color = Color.FromArgb(
                    255, // alpha
                    (byte)_random.Next(256), // red
                    (byte)_random.Next(256), // green
                    (byte)_random.Next(256)  // blue
                );
                pixels[i] = (int)color.ToUInt32(); // Convertir en format ARGB 32 bits
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

        private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
        }
    }
}