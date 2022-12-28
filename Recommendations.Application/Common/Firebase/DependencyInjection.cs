using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Recommendations.Application.Common.Interfaces;

namespace Recommendations.Application.Common.Firebase;

public static class DependencyInjection
{
    public static void AddFirebaseService(this IServiceCollection services,
        IConfiguration configuration)
    {
        var bucketName = configuration["Firebase:Bucket"];
        var firebaseCredentials = configuration.GetSection("Firebase")
            .Get<JsonCredentialParameters>();
        var googleCredential = GoogleCredential.FromJsonParameters(firebaseCredentials);
        var storageClient = StorageClient.Create(googleCredential);

        if (storageClient is null || bucketName is null)
            throw new NullReferenceException("Missing data for Firebase");

        services.AddSingleton<IFirebaseService, FirebaseService>(_ =>
            new FirebaseService(storageClient, bucketName));
    }
}