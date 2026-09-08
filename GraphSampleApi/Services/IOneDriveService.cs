namespace GraphSampleApi.Services;

public interface IOneDriveService
{
    Task<string> UploadSmallFileAsync(string fileName, Stream fileStream);
}
