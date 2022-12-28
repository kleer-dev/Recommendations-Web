using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Recommendations.Application.Common.Interfaces;

namespace Recommendations.Application.Common.Firebase;

public class FirebaseService : IFirebaseService
{
    private readonly StorageClient _storageClient;
    private readonly string _bucketName;

    public FirebaseService(StorageClient storageClient, string bucketName)
    {
        _storageClient = storageClient;
        _bucketName = bucketName;
    }

    public async Task<string> UploadImage(IFormFile formFile)
    {
        var fileStream = formFile.OpenReadStream();
        var fileType = formFile.ContentType;
        var folderName = Guid.NewGuid().ToString();
        var fileName = $"{folderName}/{formFile.FileName}";

        await CreateFolder(folderName);
        await _storageClient.UploadObjectAsync(_bucketName, 
            fileName, fileType, fileStream);
        
        return await GetFileLink(fileName);
    }

    private async Task<string> GetFileLink(string fileName)
    {
        var file = await _storageClient.GetObjectAsync(_bucketName, fileName);
        return file.MediaLink;
    }

    private async Task CreateFolder(string folderName)
    {
        const string folderContentType = "application/x-directory";
        await _storageClient.UploadObjectAsync(_bucketName, $"{folderName}/",
            folderContentType, new MemoryStream());
    }
}