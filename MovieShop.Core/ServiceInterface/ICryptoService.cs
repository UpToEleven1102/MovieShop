namespace MovieShop.Core.ServiceInterface
{
    public interface ICryptoService
    {
        string GenerateRandomSalt();

        string HashPasswordWithSalt(string password, string salt);
    }
}