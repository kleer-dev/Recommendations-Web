using Microsoft.AspNetCore.Http;
using Recommendations.Application.Common.Clouds.Firebase;

namespace Recommendations.Application.Interfaces;

public interface IFirebaseService
{
    public Task<ImageData> UploadFile(IFormFile formFile, string folderName);
    public Task<IEnumerable<ImageData>> UploadFiles(IEnumerable<IFormFile> formFiles, string folderName);
    public Task CreateFolder(string folderName);
    public Task DeleteFolder(string folderName);
    public Task<IEnumerable<ImageData>> UpdateFiles(IEnumerable<IFormFile> files, string folderName);
}