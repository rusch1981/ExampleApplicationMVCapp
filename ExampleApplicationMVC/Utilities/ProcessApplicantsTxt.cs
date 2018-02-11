using System;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Web;
using ExampleApplicationMVC.Models;
using StructureMap.Building;

namespace ExampleApplicationMVC.Utilities
{
    public class ProcessApplicantsTxt : IProcessApplicants
    {
        private readonly string _txtLocation = HttpContext.Current.Server.MapPath("~") + "/Uploads/UploadTxt/";
        private readonly string _fileLocation = HttpContext.Current.Server.MapPath("~") + "/Uploads/UploadFiles/";
        private EmailUtil _emailemailUtil;

        public void Process()
        {
            string[] txtFilePaths = Directory.GetFiles(_txtLocation);

            if (txtFilePaths.Any())
            {
                foreach (var path in txtFilePaths)
                {
                    var name = Path.GetFileName(path);
                    if (name.Equals("test.json")) continue;
                    Init();

                    var message = RetrieveMessageBody(name);
                    var filePath = RetrieveFileLocation(message);

                    _emailemailUtil.SendEmail(name, message, filePath);

                    CleanUp(path, filePath);
                }
            }
        }

        private void CleanUp(string namePath, string filePath)
        {
            File.Delete(namePath);
            File.Delete(filePath);
        }

        private void Init()
        {
            _emailemailUtil = new EmailUtil();
        }

        private string RetrieveMessageBody(string name)
        {
            return File.ReadAllText(_txtLocation + name);
        }

        private string RetrieveFileLocation(string message)
        {
            var matcher = new Regex(@"\*\*\*\*.+\*\*\*\*");
            var fileName = matcher.Match(message).ToString();
            fileName =  fileName.Substring(4);
            fileName = fileName.Substring(0, fileName.Length - 4);
            return _fileLocation + fileName;
        }
    }
}