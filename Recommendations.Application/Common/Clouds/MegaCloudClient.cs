using CG.Web.MegaApiClient;
using Microsoft.AspNetCore.Http;
using Recommendations.Application.Common.Interfaces;

namespace Recommendations.Application.Common.Clouds;

public class MegaCloudClient : IMegaCloudClient
{
    private readonly MegaApiClient _megaApiClient;
    private const string BaseCloudPath = "RecommendationsWeb";

    public MegaCloudClient(string email, string password)
    {
        _megaApiClient = new MegaApiClient();
        _megaApiClient.Login(email, password);
    }

    public async Task<string> UploadFile(IFormFile file)
    {
        IEnumerable<INode> nodes = await _megaApiClient.GetNodesAsync();
        var root = nodes.Single(r => r.Name == BaseCloudPath);
        var fileStream = await GetFileStream(file);
        
        var fileNode = await _megaApiClient.UploadAsync(fileStream, file.FileName, root);
        var fileUri = await _megaApiClient.GetDownloadLinkAsync(fileNode);

        return fileUri.AbsoluteUri;
    }

    private static async Task<Stream> GetFileStream(IFormFile file)
    {
        var stream = new MemoryStream();
        await file.CopyToAsync(stream);

        return stream;
    }
}