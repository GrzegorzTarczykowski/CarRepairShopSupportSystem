namespace CarRepairShopSupportSystem.BLL.IService
{
    public interface ICryptographyService
    {
        byte[] GenerateRandomSalt();
        byte[] GenerateSHA512(string explicitText);
        byte[] GenerateSHA512(string explicitText, byte[] salt);
    }
}
