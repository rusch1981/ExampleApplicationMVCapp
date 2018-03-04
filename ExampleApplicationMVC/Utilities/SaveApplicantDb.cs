using System.Web;
using ExampleApplicationMVC.DAL;
using ExampleApplicationMVC.Models;

namespace ExampleApplicationMVC.Utilities
{
    public class SaveApplicantDb : ISaveApplicant
    {
        private ApplicantRepository _applicantRepository;

        public SaveApplicantDb(ApplicantRepository applicantRepository)
        {
            _applicantRepository = applicantRepository;
        }

        public void Save(Applicant applicant)
        {
            _applicantRepository.CreateApplicant(applicant);

            applicant.File.SaveAs(HttpContext.Current.Server.MapPath("~") + "/Uploads/UploadFiles/" + applicant.FileName);
        }
    }
}