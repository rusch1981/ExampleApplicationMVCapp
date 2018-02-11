using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExampleApplicationMVC.Models;

namespace ExampleApplicationMVC.Utilities
{
    public interface ISaveApplicant
    {
        void Save(Applicant applicant);
    }
}
