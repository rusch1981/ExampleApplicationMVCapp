using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExampleApplicationMVC.DAL;
using ExampleApplicationMVC.Models;

namespace ExampleApplicationMVC.Utilities
{
    public class SaveApplicantDb : ISaveApplicant
    {
        public void Save(Applicant applicant)
        {
            var applicantRepo = new ApplicantRepository();
            applicantRepo.CreateApplicant(applicant);

            var fileFileName = applicant.FileName;
            applicant.File.SaveAs(HttpContext.Current.Server.MapPath("~") + "/Uploads/UploadFiles/" + fileFileName);
        }
    }
}