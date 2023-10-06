namespace Contracts.Aws;

public class AwsFileUploadDto
{
    public string FileName { get; set; }
    public Stream Stream { get; set; }
    public string Type { get; set; }
}