using HailongConsulting.API.Common.Helpers;

namespace HailongConsulting.API.Services
{
    public interface IFileUploadService
    {
        Task<(bool success, string? filePath, string? errorMessage)> UploadFileAsync(IFormFile file, string category);
        Task<List<string>> UploadMultipleFilesAsync(List<IFormFile> files, string category);
        bool DeleteFile(string webRootPath, string relativePath);
    }

    public class FileUploadService : IFileUploadService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<FileUploadService> _logger;

        public FileUploadService(IWebHostEnvironment environment, ILogger<FileUploadService> logger)
        {
            _environment = environment;
            _logger = logger;
        }

        public async Task<(bool success, string? filePath, string? errorMessage)> UploadFileAsync(IFormFile file, string category)
        {
            try
            {
                if (!FileHelper.IsValidFile(file, out string errorMessage))
                {
                    return (false, null, errorMessage);
                }

                var uniqueFileName = FileHelper.GenerateUniqueFileName(file.FileName);
                var relativePath = FileHelper.GetRelativePath(category, uniqueFileName);
                var physicalPath = FileHelper.GetPhysicalPath(_environment.WebRootPath, relativePath);

                var directory = Path.GetDirectoryName(physicalPath);
                if (directory != null)
                {
                    FileHelper.EnsureDirectoryExists(directory);
                }

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                _logger.LogInformation($"文件上传成功: {relativePath}");
                return (true, "/" + relativePath, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "文件上传失败");
                return (false, null, "文件上传失败: " + ex.Message);
            }
        }

        public async Task<List<string>> UploadMultipleFilesAsync(List<IFormFile> files, string category)
        {
            var uploadedFiles = new List<string>();

            foreach (var file in files)
            {
                var result = await UploadFileAsync(file, category);
                if (result.success && result.filePath != null)
                {
                    uploadedFiles.Add(result.filePath);
                }
            }

            return uploadedFiles;
        }

        public bool DeleteFile(string webRootPath, string relativePath)
        {
            var physicalPath = FileHelper.GetPhysicalPath(webRootPath, relativePath.TrimStart('/'));
            return FileHelper.DeleteFile(physicalPath);
        }
    }
}