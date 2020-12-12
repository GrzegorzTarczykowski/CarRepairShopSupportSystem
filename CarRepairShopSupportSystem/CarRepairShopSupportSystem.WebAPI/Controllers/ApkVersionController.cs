using CarRepairShopSupportSystem.WebAPI.Handler;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace CarRepairShopSupportSystem.WebAPI.Controllers
{
    public class ApkVersionController : ApiController
    {
        //[Authorize(Roles = "SuperAdmin, Admin, User, Guest")]
        [HttpGet]
        public long Get()
        {
            System.Diagnostics.Process cmd = new System.Diagnostics.Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine(@"cd C:\Program Files (x86)\Android\android-sdk\build-tools\29.0.2");
            cmd.StandardInput.WriteLine("aapt d badging C:\\Users\\Grzegorz\\Pictures\\apk\\CarRepairShopSupportSystem.apk | find \"package\"");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            string output = cmd.StandardOutput.ReadToEnd();
            long versionCode = long.Parse(Regex.Match(output, "versionCode='(?<versionCode>[0-9]+)'").Groups["versionCode"].Value);
            return versionCode;

            //Read the manifest of an Android apk file using C# .Net
            string apkPath = "C:\\Users\\Grzegorz\\Pictures\\apk\\CarRepairShopSupportSystem.apk";
            byte[] bytes = new byte[50 * 1024];
            using (ZipArchive zip = new ZipArchive(File.OpenRead(apkPath)))
            using (Stream stream = zip.GetEntry("AndroidManifest.xml").Open())
            {
                stream.Read(bytes, 0, bytes.Length);
            }

            AndroidDecompress decompress = new AndroidDecompress();
            string content = decompress.decompressXML(bytes);
            long versionCodeFromZip
                = long.Parse(Regex.Match(content, "versionCode=\"(?<versionCode>[0-9]+)\"").Groups["versionCode"].Value);
        }
    }
}
