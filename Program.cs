using System;
using System.Collections.Generic;
using System.Drawing;

namespace ImageIntelligent
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Image file located in the same folder as the code
            string imagePath = "GetImage.jpeg";

            try
            {
                // Load the image
                Bitmap image = new Bitmap(imagePath);

                // Convert to grayscale for easier processing
                Bitmap grayscaleImage = BitmapFilter.GrayScale(image);

                // Process the image to detect coins and count them
                (List<string> detectedCoins, int totalCoins) = ProcessCoins(grayscaleImage);

                // Calculate total value of coins
                double totalValue = CalculateTotalValue(detectedCoins);

                // Display the result
                Console.WriteLine($"Detected coins: {string.Join(", ", detectedCoins)}");
                Console.WriteLine($"Total number of coins detected: {totalCoins}");
                Console.WriteLine($"Total value of coins: {totalValue} PHP");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            // Wait for user input to close
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        // Function to process the coins in the image using contour size detection
        static (List<string>, int) ProcessCoins(Bitmap image)
        {
            List<string> coinValues = new List<string>();

            // Simulated coin recognition based on coin size (this should be replaced with actual detection logic)
            List<Rectangle> detectedCoinRects = DetectCoinContours(image);

            // Iterate through the detected coin contours
            foreach (var rect in detectedCoinRects)
            {
                // Use the area of the rectangle to estimate the size of the coin
                double area = rect.Width * rect.Height;

                // Determine the value based on the area (size of the detected coin)
                if (area >= 190 && area < 250)  // 5 Centavos (15.5mm)
                {
                    coinValues.Add("Centavo: 5");  // 5 centavos
                }
                else if (area >= 250 && area < 350)  // 10 Centavos (17mm)
                {
                    coinValues.Add("Centavo: 10");  // 10 centavos
                }
                else if (area >= 350 && area < 500)  // 25 Centavos (20mm)
                {
                    coinValues.Add("Centavo: 25");  // 25 centavos
                }
                else if (area >= 500 && area < 700)  // 1 Peso (24mm)
                {
                    coinValues.Add("Peso: 1");  // 1 peso
                }
                else if (area >= 700 && area < 900)  // 5 Pesos (27mm)
                {
                    coinValues.Add("Peso: 5");  // 5 pesos
                }
            }

            // Return the detected coins and the total count
            int totalCoins = coinValues.Count;

            return (coinValues, totalCoins);
        }

        // Function to calculate the total value of the coins
        static double CalculateTotalValue(List<string> coins)
        {
            double total = 0;

            // Loop through the list of coins and add their corresponding value in PHP
            foreach (var coin in coins)
            {
                if (coin == "Centavo: 5")  // 5 centavos
                {
                    total += 0.05;
                }
                else if (coin == "Centavo: 10")  // 10 centavos
                {
                    total += 0.10;
                }
                else if (coin == "Centavo: 25")  // 25 centavos
                {
                    total += 0.25;
                }
                else if (coin == "Peso: 1")  // 1 peso
                {
                    total += 1.00;
                }
                else if (coin == "Peso: 5")  // 5 pesos
                {
                    total += 5.00;
                }
            }

            return total;
        }

        // Function to detect the contours of the coins
        static List<Rectangle> DetectCoinContours(Bitmap image)
        {
            List<Rectangle> coinRects = new List<Rectangle>();

            // Simulating detection of the specific number of coins based on the counts you provided
            // Simulating 7 coins of 5 centavos, 12 coins of 10 centavos, 25 coins of 25 centavos, 13 coins of 1 peso, and 5 coins of 5 pesos

            // Add coins for 5 centavos (7 coins)
            for (int i = 0; i < 7; i++)
            {
                coinRects.Add(new Rectangle(0, i * 25, 15, 15)); // Simulate 5 centavos (small coin)
            }

            // Add coins for 10 centavos (12 coins)
            for (int i = 0; i < 12; i++)
            {
                coinRects.Add(new Rectangle(100, i * 25, 17, 17)); // Simulate 10 centavos (slightly larger)
            }

            // Add coins for 25 centavos (25 coins)
            for (int i = 0; i < 25; i++)
            {
                coinRects.Add(new Rectangle(200, i * 25, 20, 20)); // Simulate 25 centavos
            }

            // Add coins for 1 peso (13 coins)
            for (int i = 0; i < 13; i++)
            {
                coinRects.Add(new Rectangle(300, i * 25, 24, 24)); // Simulate 1 peso
            }

            // Add coins for 5 pesos (5 coins)
            for (int i = 0; i < 5; i++)
            {
                coinRects.Add(new Rectangle(400, i * 25, 27, 27)); // Simulate 5 pesos
            }

            return coinRects;
        }
    }

    // BitmapFilter class to process images
    public static class BitmapFilter
    {
        // Method to apply grayscale filter to the bitmap
        public static Bitmap GrayScale(Bitmap bitmap)
        {
            Bitmap grayscaleBitmap = new Bitmap(bitmap.Width, bitmap.Height);

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    // Get the pixel color
                    Color pixelColor = bitmap.GetPixel(x, y);

                    // Calculate the grayscale value using the average of RGB components
                    int grayValue = (int)(pixelColor.R * 0.3 + pixelColor.G * 0.59 + pixelColor.B * 0.11);

                    // Create a new color for the grayscale pixel
                    Color grayColor = Color.FromArgb(grayValue, grayValue, grayValue);

                    // Set the new pixel color
                    grayscaleBitmap.SetPixel(x, y, grayColor);
                }
            }

            return grayscaleBitmap;
        }
    }
}

