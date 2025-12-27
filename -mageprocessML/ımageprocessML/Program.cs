using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

string img1Path = "img1.jpg";
string img2Path = "img2.jpg";

using var img1 = Image.Load<Rgb24>(img1Path);
using var img2 = Image.Load<Rgb24>(img2Path);

int width = Math.Min(img1.Width, img2.Width);
int height = Math.Min(img1.Height, img2.Height);

long diff = 0;
long total = width * height * 3;

for (int y = 0; y < height; y++)
{
    for (int x = 0; x < width; x++)
    {
        Rgb24 p1 = img1[x, y];
        Rgb24 p2 = img2[x, y];

        diff += Math.Abs(p1.R - p2.R);
        diff += Math.Abs(p1.G - p2.G);
        diff += Math.Abs(p1.B - p2.B);
    }
}

double similarity = 100 - ((double)diff / (255 * total) * 100);

Console.WriteLine($"Benzerlik: %{similarity:F2}");