using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ExampleApplicationMVC.Models;
using ExampleApplicationMVC.Utilities;

namespace ExampleApplicationMVC.Controllers.api
{
    public class ApplicationController : ApiController
    {
        private readonly ISaveApplicant _saveApplicant;
        private readonly IProcessApplicants _processApplicant;
        private readonly IEmailUtil _emailUtil;

        public ApplicationController(ISaveApplicant saveApplicant, IProcessApplicants loadApplicants, IEmailUtil emailUtil)
        {
            _saveApplicant = saveApplicant;
            _processApplicant = loadApplicants;
            _emailUtil = emailUtil;
        }

        // POST api/<controller>
        [HttpPost]
        public void Post()
        {
            try
            {
                var applicant = Init();
                _saveApplicant.Save(applicant);
                _processApplicant.Process();
            }
            catch (Exception e)
            {
                _emailUtil.SendEmail(e, "Api Error");
            }
        }

        private Applicant Init()
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var request = HttpContext.Current.Request;
                var httpPostedFile = request.Files["UploadedImage"];
                var applicant = new Applicant
                {
                    Name = request.Params["Name"],
                    Age = Int32.Parse(request.Params["Age"]),
                    Email = request.Params["Name"],
                    AboutYou = request.Params["AboutYou"],
                    Experience = request.Params["Experience"],
                    SkillsTalents = request.Params["SkillsTalents"],
                    File = httpPostedFile
                };
                return applicant;
            }
            return new Applicant();
        }
    }
}