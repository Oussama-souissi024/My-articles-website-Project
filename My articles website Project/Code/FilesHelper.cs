using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace My_articles_website_Project.Code
{
    public class FilesHelper
    {
        private readonly IWebHostEnvironment webHost;

        public FilesHelper(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
        }



        //Upload File
        //public string? UploadFile(IFormFile File, string Folder)
        //{
        //    if (File != null)
        //    {
        //        var fileDir = Path.Combine(webHost.ContentRootPath, Folder);
        //        var fileName = Guid.NewGuid() + "-" + File.FileName;
        //        var filePath = fileDir + fileName;

        //        using(FileStream fileStream = new FileStream(filePath,FileMode.Create))
        //        {
        //            File.CopyTo(fileStream);
        //            return fileName;
        //        }
        //    }
        //    else
        //        return string.Empty;
        //}
        public string? UploadFile(IFormFile File, string Folder)
        {
            if (File != null)
            {
                var fileDir = Path.Combine(webHost.WebRootPath, Folder);

                // Create directory if it doesn't exist
                if (!Directory.Exists(fileDir))
                {
                    Directory.CreateDirectory(fileDir);
                }

                var fileName = Guid.NewGuid() + "-" + File.FileName;
                var filePath = Path.Combine(fileDir, fileName);

                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    try
                    {
                        File.CopyTo(fileStream);
                        return fileName;
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Cannot upload this file");
                        return string.Empty;
                    }
              
                }
            }
            else
            {
                return string.Empty;
            }
        }

    }
}
