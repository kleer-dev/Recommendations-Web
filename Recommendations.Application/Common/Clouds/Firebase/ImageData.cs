using AutoMapper;
using Recommendations.Application.Common.Mappings;
using Recommendations.Domain;

namespace Recommendations.Application.Common.Clouds.Firebase;

public class ImageData : IMapWith<Image>
{
    public string FileName { get; set; }
    public string FolderName { get; set; }
    public string Url { get; set; }
    
    public ImageData(string fileName, string folderName, string url)
    {
        FileName = fileName;
        FolderName = folderName;
        Url = url;
    }

    public ImageData() { }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ImageData, Image>();
    }
}