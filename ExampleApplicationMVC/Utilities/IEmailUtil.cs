using System;

namespace ExampleApplicationMVC.Utilities
{
    public interface IEmailUtil
    {
        void SendEmail(String message);
        void SendEmail(Exception exception, String message);
        void SendEmail(string subject, string message, string filePath);
    }
}
