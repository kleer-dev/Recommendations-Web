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

    public async Task<ImageData> UploadFile(IFormFile formFile, string folderName)
    {
        var fileStream = formFile.OpenReadStream();
        var fileType = formFile.ContentType;
        var fileName = $"{folderName}/{formFile.FileName}";

        await CreateFolder(folderName);
        var uploadResponse = await _storageClient.UploadObjectAsync(_bucketName,
            fileName, fileType, fileStream);

        return new ImageData(fileName, folderName, uploadResponse.MediaLink);
    }

    public async Task<IEnumerable<ImageData>> UploadFiles(IEnumerable<IFormFile> formFiles,
        string folderName)
    {
        var imagesData = new List<ImageData>();
        foreach (var file in formFiles)
        {
            var imageData = await UploadFile(file, folderName);
            imagesData.Add(imageData);
        }

        return imagesData;
    }

    public async Task CreateFolder(string folderName)
    {
        const string folderContentType = "application/x-directory";
        await _storageClient.UploadObjectAsync(_bucketName, $"{folderName}/",
            folderContentType, new MemoryStream());
    }

    public async Task DeleteFolder(string folderName)
    {
        if (string.IsNullOrEmpty(folderName))
            return;
        var folders = _storageClient
            .ListObjectsAsync(_bucketName, folderName);
        await foreach (var folder in folders)
            await _storageClient.DeleteObjectAsync(_bucketName, folder.Name);
    }

    public async Task<IEnumerable<ImageData>> UpdateFiles(IEnumerable<IFormFile> files,
        string folderName)
    {
        await DeleteFolder(folderName);
        return await UploadFiles(files, folderName);
    }
}