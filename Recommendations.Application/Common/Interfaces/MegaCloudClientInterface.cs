using Microsoft.AspNetCore.Http;

namespace Recommendations.Application.Common.Interfaces;

public interface IMegaCloudClient
{
    Task<string> UploadFile(IFormFile file);
}