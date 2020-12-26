namespace CarRepairShopSupportSystem.WebAPI.BLL.IService
{
    public interface IAndroidPackageKitService
    {
        long GetApkVersion(string aaptToolPath, string apkFilePath);
    }
}
