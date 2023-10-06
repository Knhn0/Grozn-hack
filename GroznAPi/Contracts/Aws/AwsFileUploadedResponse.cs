namespace Contracts.Aws;

public class AwsFileUploadedResponse
{
    public bool IsUploaded { get; set; }
    public AwsFileDto? File { get; set; }
}