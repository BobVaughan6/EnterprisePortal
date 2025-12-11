using Microsoft.Extensions.Configuration;

namespace HailongConsulting.API.Common.Helpers
{
    public class FileHelper
    {
        private readonly IConfiguration _configuration;
        private readonly string[] _allowedExtensions;
        private readonly long _maxFileSize;

        public FileHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            
            // 从配置读取允许的扩展名
            var imageExts = _configuration["FileUpload:AllowedImageExtensions"]?.Split(',') ?? Array.Empty<string>();
            var docExts = _configuration["FileUpload:AllowedDocumentExtensions"]?.Split(',') ?? Array.Empty<string>();
            var videoExts = _configuration["FileUpload:AllowedVideoExtensions"]?.Split(',') ?? Array.Empty<string>();
            _allowedExtensions = imageExts.Concat(docExts).Concat(videoExts).ToArray();
            
            // 从配置读取最大文件大小
            _maxFileSize = long.TryParse(_configuration["FileUpload:MaxFileSize"], out var size)
                ? size
                : 10 * 1024 * 1024; // 默认10MB
        }

        public bool IsValidFile(IFormFile file, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (file == null || file.Length == 0)
            {
                errorMessage = "文件不能为空";
                return false;
            }

            if (file.Length > _maxFileSize)
            {
                errorMessage = $"文件大小不能超过 {_maxFileSize / 1024 / 1024}MB";
                return false;
            }

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_allowedExtensions.Contains(extension))
            {
                errorMessage = $"不支持的文件类型。允许的类型: {string.Join(", ", _allowedExtensions)}";
                return false;
            }

            return true;
        }

        public string GenerateUniqueFileName(string originalFileName)
        {
            var extension = Path.GetExtension(originalFileName);
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(originalFileName);
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            var randomString = Guid.NewGuid().ToString("N").Substring(0, 8);
            return $"{fileNameWithoutExtension}_{timestamp}_{randomString}{extension}";
        }

        public string GetRelativePath(string category, string fileName)
        {
            var uploadPath = _configuration["FileUpload:UploadPath"] ?? "uploads/attachments";
            var yearMonth = DateTime.Now.ToString("yyyyMM");
            return Path.Combine(uploadPath, category, yearMonth, fileName).Replace("\\", "/");
        }

        public string GetPhysicalPath(string webRootPath, string relativePath)
        {
            if (string.IsNullOrEmpty(webRootPath))
            {
                throw new ArgumentNullException(nameof(webRootPath), "WebRootPath 不能为空");
            }

            if (string.IsNullOrEmpty(relativePath))
            {
                throw new ArgumentNullException(nameof(relativePath), "RelativePath 不能为空");
            }

            return Path.Combine(webRootPath, relativePath.Replace("/", Path.DirectorySeparatorChar.ToString()));
        }

        public void EnsureDirectoryExists(string directoryPath)
        {
            if (string.IsNullOrEmpty(directoryPath))
            {
                throw new ArgumentNullException(nameof(directoryPath), "DirectoryPath 不能为空");
            }

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        public bool DeleteFile(string filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    return false;
                }

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

        /// <summary>
        /// 根据文件扩展名自动识别分类
        /// </summary>
        public string GetCategoryByExtension(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            
            // 图片类型
            var imageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp", ".svg", ".ico" };
            if (imageExtensions.Contains(extension))
            {
                return "image";
            }
            
            // 文档类型
            var documentExtensions = new[] { ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".txt", ".csv", ".rtf" };
            if (documentExtensions.Contains(extension))
            {
                return "document";
            }
            
            // 视频类型
            var videoExtensions = new[] { ".mp4", ".avi", ".mov", ".wmv", ".flv", ".mkv", ".webm" };
            if (videoExtensions.Contains(extension))
            {
                return "video";
            }
            
            // 其他类型
            return "other";
        }
    }
}