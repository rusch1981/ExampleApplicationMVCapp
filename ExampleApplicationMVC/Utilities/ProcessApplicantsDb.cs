using ExampleApplicationMVC.DAL;
using ExampleApplicationMVC.Models;
using System.IO;
using System.Web;

namespace ExampleApplicationMVC.Utilities
{
    public class ProcessApplicantsDb : IProcessApplicants
    {
        private readonly string _fileLocation = HttpContext.Current.Server.MapPath("~") + "/Uploads/UploadFiles/";
        private EmailUtil _emailemailUtil;
        private ApplicantRepository _applicantRepository;

        public void Process()
        {
            Init();

            var ids = _applicantRepository.GetIncompleteApplicants();

            foreach(var id in ids)
            {
                Applicant applicant = NewMethod(id);
                _emailemailUtil.SendEmail(GetName_Id(id, applicant), applicant.Message, GetFilePath(applicant));
                File.Delete(GetFilePath(applicant));
                _applicantRepository.SetApplicantToComplete(id);
            }
        }

        private static string GetName_Id(int id, Applicant applicant)
        {
            return applicant.Name + "_" + id;
        }

        private string GetFilePath(Applicant applicant)
        {
            return _fileLocation + applicant.FileName;
        }

        private Models.Applicant NewMethod(int id)
        {
            return _applicantRepository.GetApplicant(id);
        }

        private void Init()
        {
            _emailemailUtil = new EmailUtil();
            _applicantRepository = new ApplicantRepository();
        }
    }
}