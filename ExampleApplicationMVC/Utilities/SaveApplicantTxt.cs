using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExampleApplicationMVC.Models;

namespace ExampleApplicationMVC.Utilities
{
    public class SaveApplicantTxt : ISaveApplicant
    {
        public void Save(Applicant applicant)
        {
            var txtFileName = Guid.NewGuid() + applicant.Name + ".txt";
            var fileFileName = Guid.NewGuid() + applicant.File.FileName;

            applicant.File.SaveAs(HttpContext.Current.Server.MapPath("~") + "/Uploads/UploadFiles/" + fileFileName);
            System.IO.File.WriteAllText(HttpContext.Current.Server.MapPath("~") + "/Uploads/UploadTxt/" + txtFileName,
                applicant.Message + "****" + fileFileName + "****");
        }

    }
}