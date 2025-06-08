using Amazon.S3.Model;
using RealEstate.Shared.OperationResult;

namespace RealEstate.Shared.Services.File;

public interface IAwsStorageService
{
     Task<OperationResult.OperationResult<string>> GenerateImageUploadUrl(string fileName);
    Task<OperationResult<ChunkedUploadResponse>> InitiateChunkedVideoUpload(string fileName);
    Task<OperationResult<string>> GenerateChunkUploadUrl(string uploadId, int partNumber, string fileName);
    Task<OperationResult<string>> CompleteChunkedUpload(string uploadId, string fileName, List<Amazon.S3.Model.PartETag> partETags);

}
