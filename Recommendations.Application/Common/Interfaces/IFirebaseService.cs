using Microsoft.AspNetCore.Http;

namespace Recommendations.Application.Common.Interfaces;

public interface IFirebaseService
{
    public Task<string> UploadImage(IFormFile formFile);
}