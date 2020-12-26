using CarRepairShopSupportSystem.WebAPI.BLL.IService;
using CarRepairShopSupportSystem.WebAPI.Handler;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace CarRepairShopSupportSystem.WebAPI.Controllers
{
    public class AndroidPackageKitController : ApiController
    {
        private readonly IAndroidPackageKitService androidPackageKitService;

        public AndroidPackageKitController(IAndroidPackageKitService androidPackageKitService)
        {
            this.androidPackageKitService = androidPackageKitService;
        }

        //[Authorize(Roles = "SuperAdmin, Admin, User, Guest")]
        [HttpGet]
        public long GetApkVersion()
        {
            return androidPackageKitService.GetApkVersion(
                @"C:\Program Files (x86)\Android\android-sdk\build-tools\29.0.2"
                , @"C:\Users\Grzegorz\Pictures\apk\CarRepairShopSupportSystem.apk");

            //Read the manifest of an Android apk file using C# .Net
            byte[] bytes = new byte[50 * 1024];
            using (ZipArchive zip = new ZipArchive(File.OpenRead(@"C:\Users\Grzegorz\Pictures\apk\CarRepairShopSupportSystem.apk")))
            using (Stream stream = zip.GetEntry("AndroidManifest.xml").Open())
            {
                stream.Read(bytes, 0, bytes.Length);
            }

            AndroidDecompress decompress = new AndroidDecompress(bytes);
            string content = decompress.DecompressXML();
            long versionCodeFromZip
                = long.Parse(Regex.Match(content, "versionCode=\"(?<versionCode>[0-9]+)\"").Groups["versionCode"].Value);
            return versionCodeFromZip;

            decompress = new AndroidDecompress(bytes);
            versionCodeFromZip = decompress.GetVersionCode();
            return versionCodeFromZip;
        }
    }
}