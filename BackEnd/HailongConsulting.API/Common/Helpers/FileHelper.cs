namespace HailongConsulting.API.Common.Helpers
{
    public static class FileHelper
    {
        private static readonly string[] AllowedExtensions = { ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".jpg", ".jpeg", ".png" };
        private static readonly long MaxFileSize = 10 * 1024 * 1024; // 10MB

        public static bool IsValidFile(IFormFile file, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (file == null || file.Length == 0)
            {
                errorMessage = "文件不能为空";
                return false;
            }

            if (file.Length > MaxFileSize)
            {
                errorMessage = $"文件大小不能超过 {MaxFileSize / 1024 / 1024}MB";
                return false;
            }

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!AllowedExtensions.Contains(extension))
            {
                errorMessage = $"不支持的文件类型。允许的类型: {string.Join(", ", AllowedExtensions)}";
                return false;
            }

            return true;
        }

        public static string GenerateUniqueFileName(string originalFileName)
        {
            var extension = Path.GetExtension(originalFileName);
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(originalFileName);
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            var randomString = Guid.NewGuid().ToString("N").Substring(0, 8);
            return $"{fileNameWithoutExtension}_{timestamp}_{randomString}{extension}";
        }

        public static string GetRelativePath(string category, string fileName)
        {
            var yearMonth = DateTime.Now.ToString("yyyyMM");
            return Path.Combine("uploads", "attachments", category, yearMonth, fileName).Replace("\\", "/");
        }

        public static string GetPhysicalPath(string webRootPath, string relativePath)
        {
            return Path.Combine(webRootPath, relativePath.Replace("/", Path.DirectorySeparatorChar.ToString()));
        }

        public static void EnsureDirectoryExists(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        public static bool DeleteFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}