// Licensed to the .NET Foundation under one or more agreements.

using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.Extensions.Caching.Memory;

namespace RealEstate.Shared.Security.SecretManager;

public class SecretManagerService: ISecretManagerService
{
    private readonly IAmazonSecretsManager _client;
    private readonly IMemoryCache _memoryCache;

    public SecretManagerService(IMemoryCache memoryCache)
    {   
        _client = new AmazonSecretsManagerClient(RegionEndpoint.APSoutheast2);
        _memoryCache = memoryCache;
        
    }
    public async Task<string> GetSecret(string Key)
    {
        if (_memoryCache.TryGetValue(Key, out string value))
        {
            return value;
        }
        GetSecretValueRequest request = new GetSecretValueRequest
        {
            SecretId = Key
        };
        GetSecretValueResponse secretValueResponse = await _client.GetSecretValueAsync(request);
        _memoryCache.Set(Key, secretValueResponse.SecretString);
        return secretValueResponse.SecretString;
    }

    public Task InvalidateSecret(string Key)
    {
        
        _memoryCache.Remove(Key);
        return Task.CompletedTask;
        

    }
    
}
