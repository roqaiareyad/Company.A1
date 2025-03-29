namespace Company.A1.PL.Helpers
{
    public  static class DocumentSettings
    {
        // 1.Upload
        // ImageName
        public static string UploadFile(IFormFile file , string folderName)
        {
        //    //1. Get Folder Location

        //    //string folderPath = "D:\\.NET\\MVC\\Company.A1\\Company.A1.PL\\wwwroot\\files\\images\\"+ folderName;

        //    //var folderPath =Directory.GetCurrentDirectory()+ "\\wwwroot\\files\\"+ folderName;
               string folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName);

        // 2. Get File Name and Make It Unique

               string  fileName = $"{Guid.NewGuid()}{file.FileName}";

        // File Path
               string filePath = Path.Combine(folderPath, fileName);

               using var fileStream = new FileStream(filePath , FileMode.Create);


               file.CopyTo(fileStream);
               return fileName ;

        }

               //public static string UploadFile(IFormFile file, string folderName)
//        {
//            if (file == null || file.Length == 0)
//            {
//                return null; 
//            }

//            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files", folderName);

//            if (!Directory.Exists(folderPath))
//            {
//                Directory.CreateDirectory(folderPath); 
//            }

//            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
//            var filePath = Path.Combine(folderPath, fileName);

//            using var fileStream = new FileStream(filePath, FileMode.Create);
//            file.CopyTo(fileStream);

//            return fileName;
//        }

        // 2.Delete
        public static void DeleteFile(string fileName, string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName , fileName);

            if (File.Exists(filePath))
                File.Delete(filePath);  
        }
      
    }
}
