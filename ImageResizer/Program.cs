using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;

namespace ImageResizer
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string sourceImgFolder = GetSourceImageFolder();
            ResizeImages(sourceImgFolder);

        }

        private static void ResizeImages(string sourceImgFolder)
        {
            string[] imagesToBeResized = GetImagesToBeResized(sourceImgFolder);

            string destinationFolderPath = GetDestinationFolderPath(sourceImgFolder);
            RemoveExistingDestinationFolder(destinationFolderPath);

            foreach(string image in imagesToBeResized)
            {
                var resizedImage = ResizeImage(image);
                string destimationImagePath = GetDestinationImagePath(image, sourceImgFolder, destinationFolderPath);
                CreateDestinationDirectory(destimationImagePath);
                saveImage(resizedImage, destimationImagePath);
            }
        }

        private static void CreateDestinationDirectory(string destimationImagePath)
        {
            if (!Directory.Exists(Path.GetDirectoryName(destimationImagePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(destimationImagePath));
            }
        }

        private static void RemoveExistingDestinationFolder(string destinationFolderPath)
        {
           if (Directory.Exists(destinationFolderPath))
            {
                Directory.Delete(destinationFolderPath);
            }

            Directory.CreateDirectory(destinationFolderPath);
        }

        private static void saveImage(Image<Rgba64> resizedImage, string destimationImagePath)
        {

            resizedImage.Save(destimationImagePath);
            resizedImage.Dispose();
        }

        private static string GetDestinationImagePath(string image, string sourceImgFolder, string destinationFolderPath)
        {
            return destinationFolderPath + image.Substring(sourceImgFolder.Length - 1);
        }

 
        private static string GetDestinationFolderPath(string sourceImgFolder)
        {
            return sourceImgFolder + "Resized";
        }

        private static string[] GetImagesToBeResized(string imagePath)
        {
            return Directory.GetFiles(imagePath, "*.jpg", SearchOption.AllDirectories);
        }

      

        private static Image<Rgba64> ResizeImage(string imageFile)
        {
            // Image.Load(string path) is a shortcut for our default type. 
            // Other pixel formats use Image.Load<TPixel>(string path))
            Image<Rgba64> image = Image.Load<Rgba64>(File.ReadAllBytes(imageFile));

            image.Mutate(x => x
                 .Resize(200, 110));

            return image;

        }

        private static string GetSourceImageFolder()
        {
            Console.WriteLine("What is the SourceImage Path to Resize?");
            //return Console.ReadLine();

            return @"D:\Projects\My\SeaMaster\img\products";


        }


    }
}
