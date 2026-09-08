using Microsoft.Graph;

namespace GraphSampleApi.Services;

public class OneDriveService(GraphServiceClient graphClient, IConfiguration configuration) : IOneDriveService
{
    private readonly string _folderName = configuration["OneDrive:FolderName"] ?? "Uploads";

    public async Task<string> UploadSmallFileAsync(string fileName, Stream fileStream)
    {
        var myDrive = await graphClient.Me.Drive.GetAsync();
        var item = await graphClient.Drives[myDrive?.Id]
            .Items["root"]
            .ItemWithPath($"{_folderName}/{fileName}")
            .Content
            .PutAsync(fileStream);
        return item!.WebUrl!;
    }
}
