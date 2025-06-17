using Amazon.S3.Model;
using RealEstate.Shared.OperationResult;

namespace RealEstate.Shared.Services.File;

public interface IAwsStorageService
{
    Task<TResult<string>> GenerateImageUploadUrl(string fileName);
    Task<TResult<ChunkedUploadResponse>> InitiateChunkedVideoUpload(string fileName);
    Task<TResult<string>> GenerateChunkUploadUrl(string uploadId, int partNumber, string fileName);
    Task<TResult<string>> CompleteChunkedUpload(string uploadId, string fileName, List<Amazon.S3.Model.PartETag> partETags);
    
}
