namespace Company.A1.PL.Helpers
{
    public  static class DocumentSettings
    {
         // Upload any file

        public static string UploadFile(IFormFile file, string folderName) // return image name to store in db
        {
            // 1 Get Folder Location

            //string folderPath = ""+folderName; 
            //var folderPath=Directory.GetCurrentDirectory() +"\\wwwroot\\files\\" +folderName;
           // "D:\.NET\MVC\Company.A1"
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName);

            // 2 Get FileName and Make it Unique

            var fileName = $"{Guid.NewGuid()}{file.FileName}";

            // File Path
            var filePath = Path.Combine(folderPath, fileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);

            return fileName;
        }

        // delete any file

        public static void DeleteFile(string fileName, string folderName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

        }

    }
}
