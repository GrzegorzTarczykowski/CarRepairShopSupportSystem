using CarRepairShopSupportSystem.WebAPI.BLL.IService;
using System.Text.RegularExpressions;

namespace CarRepairShopSupportSystem.WebAPI.BLL.Service
{
    public class AndroidPackageKitService : IAndroidPackageKitService
    {
        public long GetApkVersion(string aaptToolPath, string apkFilePath)
        {
            System.Diagnostics.Process cmd = new System.Diagnostics.Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine($"cd {aaptToolPath}");
            cmd.StandardInput.WriteLine($"aapt d badging {apkFilePath} | find \"package\"");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            string output = cmd.StandardOutput.ReadToEnd();
            long versionCode = long.Parse(Regex.Match(output, "versionCode='(?<versionCode>[0-9]+)'").Groups["versionCode"].Value);
            return versionCode;
        }
    }
}
