
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;
using RealEstate.Shared.OperationResult;

namespace RealEstate.Shared.Services.File;

public class AwsStorageService : IAwsStorageService
{

    private readonly IConfiguration _config;
    private readonly AmazonS3Client _s3Client;


public AwsStorageService(
        IConfiguration config)
    {
        _config = config;
        var region = _config["AWS:Region"];
        _s3Client = new AmazonS3Client(
            _config["AWS:AccessKey"],
            _config["AWS:SecretKey"],
            Amazon.RegionEndpoint.APSoutheast2
            );
    }

    public async Task<OperationResult<string>> CompleteChunkedUpload(string uploadId, string fileName, List<Amazon.S3.Model.PartETag> partETags)
    {
        var completeRequest = new CompleteMultipartUploadRequest
        {
            BucketName = _config["AWS:BucketName"],
            Key = fileName,
            UploadId = uploadId,
            PartETags = partETags
        };

        var response=await _s3Client.CompleteMultipartUploadAsync(completeRequest);
        return OperationResultExtension.SetSuccess(response.Location);
    }

    public async Task<OperationResult<string>> GenerateChunkUploadUrl(string uploadId, int partNumber, string fileName)
    {
         var request = new GetPreSignedUrlRequest
        {
            BucketName = _config["AWS:BucketName"],
            Key = Guid.NewGuid().ToString(),
            Verb = HttpVerb.PUT,
            Expires = DateTime.UtcNow.AddHours(1),
            PartNumber = partNumber,
            UploadId = uploadId
        };

        return OperationResultExtension.Created(_s3Client.GetPreSignedURL(request));
    }

    public async Task<OperationResult.OperationResult<string>> GenerateImageUploadUrl(string fileName)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = _config["AWS:BucketName"],
            Key = $"{_config["AWS:ImageContainer"]}/{Guid.NewGuid()}/{fileName}",
            Verb = HttpVerb.PUT,
            Expires = DateTime.UtcNow.AddSeconds(Double.Parse(_config["AWS:PresignedUrlExpiration"]??"122"))
        };
        return OperationResult.OperationResultExtension.Created(_s3Client.GetPreSignedURL(request));
        
    }

    public async Task<OperationResult<ChunkedUploadResponse>> InitiateChunkedVideoUpload(string fileName)
    {
         var initiateRequest = new InitiateMultipartUploadRequest
        {
            BucketName = _config["AWS:BucketName"],
            Key = $"{_config["AWS:VideoContainer"]}/{Guid.NewGuid()}/{fileName}"
        };

        var response = await _s3Client.InitiateMultipartUploadAsync(initiateRequest);
        return OperationResultExtension.Created(new ChunkedUploadResponse
        {
            UploadId = response.UploadId,
            Key = response.Key
        });


        
    }
}
