using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.ComponentModel;
using System.Text;

namespace ExampleApplicationMVC.Models
{
    public class Applicant
    {
        private const string _errorMessage = "* Required";

        [Required(ErrorMessage = _errorMessage)]
        public string Name { get; set; }
        [Required(ErrorMessage = _errorMessage)]
        [Range(18,40)]
        public int Age { get; set; }
        [Required(ErrorMessage = _errorMessage)]
        [EmailAddressAttribute]
        public string Email  { get; set; }
        [Required(ErrorMessage = _errorMessage)]
        [DisplayName("About you")]
        public string AboutYou { get; set; }
        [Required(ErrorMessage = _errorMessage)]
        [DisplayName("Experience")]
        public string Experience { get; set; }
        [Required(ErrorMessage = _errorMessage)]
        [DisplayName("Special Skills and Talents")]
        public string SkillsTalents { get; set; }
        public string FileName { get; set; }
        public HttpPostedFile File
        {
            get
            {
                return _file;
            }
            set
            {
                FileName = Guid.NewGuid() + value.FileName;
                _file = value;
            }
        }


        private HttpPostedFile _file;
        private string _message = null;

        public string Message
        {
            get
            {
                if (string.IsNullOrEmpty(_message))
                {
                    StringBuilder message = new StringBuilder();
                    message.Append("Name: " + Name);
                    message.Append(Environment.NewLine);
                    message.Append("Age: " + Age);
                    message.Append(Environment.NewLine);
                    message.Append("Email: " + Email);
                    message.Append(Environment.NewLine);
                    message.Append("About you: " + AboutYou);
                    message.Append(Environment.NewLine);
                    message.Append("Experience: " + Experience);
                    message.Append(Environment.NewLine);
                    message.Append("Skills and Talents: " + SkillsTalents);
                    message.Append(Environment.NewLine);

                    return message.ToString();
                }

                return _message;
            }
            set => _message = value;
        }
    }
}