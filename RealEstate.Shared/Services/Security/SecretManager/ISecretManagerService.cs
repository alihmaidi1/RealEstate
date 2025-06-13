namespace RealEstate.Shared.Security.SecretManager;
public interface ISecretManagerService
{
    
    
    public Task<string> GetSecret(string Key);
    
    public Task InvalidateSecret(string Key);
    
        
}
