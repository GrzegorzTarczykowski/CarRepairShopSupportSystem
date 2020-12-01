using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        }
    }
}
