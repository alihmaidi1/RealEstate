using Amazon.S3.Model;
using RealEstate.Shared.OperationResult;

namespace RealEstate.Shared.Services.File;

public interface IAwsStorageService
{
     Task<Result<string>> GenerateImageUploadUrl(string fileName);
    Task<Result<ChunkedUploadResponse>> InitiateChunkedVideoUpload(string fileName);
    Task<Result<string>> GenerateChunkUploadUrl(string uploadId, int partNumber, string fileName);
    Task<Result<string>> CompleteChunkedUpload(string uploadId, string fileName, List<Amazon.S3.Model.PartETag> partETags);

}
