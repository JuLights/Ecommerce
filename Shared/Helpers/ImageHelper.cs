using Microsoft.AspNetCore.Http;
using Shared.Exceptions;

namespace Shared.Helpers;

// public static class ImageHelper
// {
//     private static readonly string[] AllowedExtensions = [".jpeg", ".png"];
//     private const long MaxFileSize = 5 * 1024 * 1024; 
//     public static string UploadFile(IFormFile? file)
//     {
//         if (file == null || file.Length == 0)
//         {
//             throw new UserFriendlyException(ErrorMessages.ImageNotUploaded);
//         }
//
//         if (file.Length > MaxFileSize)
//         {
//             throw new UserFriendlyException(ErrorMessages.ImageSizeIsMoreThan5Mb);
//         }
//
//         var fileExtension = Path.GetExtension(file.FileName).ToLower();
//
//         if (Array.IndexOf(AllowedExtensions, fileExtension) < 0)
//         {
//             throw new UserFriendlyException(ErrorMessages.WrongImageExtension);
//         }
//
//         var destinationFolder = Environment.GetEnvironmentVariable("IMAGE_DIR");
//
//         if (destinationFolder == null || !Directory.Exists(destinationFolder))
//         {
//             throw new Exception("Image upload directory not set");
//         }
//
//         Directory.CreateDirectory(destinationFolder);
//         var fileName = Path.GetFileName(file.FileName);
//         var destinationFilePath = Path.Combine(destinationFolder, fileName);
//                 
//         try
//         {
//             using var stream = new FileStream(destinationFilePath, FileMode.Create);
//             file.CopyTo(stream);
//         }
//         catch (Exception ex)
//         {
//             throw new Exception($"Error while uploading the file: {ex.Message}");
//         }
//                 
//         return fileName;
//
//     }
// }