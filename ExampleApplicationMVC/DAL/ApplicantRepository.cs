using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using Dapper;
using ExampleApplicationMVC.DAL;
using ExampleApplicationMVC.Models;
using ExampleApplicationMVC.Utilities;

namespace ExampleApplicationMVC.DAL
{
    public class ApplicantRepository
    {
        private const string GetIncompleteApplicantIdsSqlFile = @"Sql/GetIncompleteApplicantIds.sql";
        private const string SetApplicantToCompleteSqlFile = @"Sql/SetApplicantToComplete.sql";
        private const string GetApplicantSqlFile = @"Sql/GetApplicant.sql";


        public void CreateApplicant(Applicant applicant)
        {
            using (IDbConnection connection = new SqlConnection(ConfigManager.GetConnectionString("Application")))
            {
                //called SP on DB
                connection.Execute("dbo.Add_Applicant @Name, @Age, @Email, @AboutYou, @Experience, @SkillsTalents, @FileName", applicant);
            }
        }

        public List<int> GetIncompleteApplicants()
        {
            using (IDbConnection connection = new SqlConnection(ConfigManager.GetConnectionString("Application")))
            {
                //get sql string from embedded resource
                var getIncompleteApplicantIdsStatement = ReadEmbeddedResource(GetIncompleteApplicantIdsSqlFile);                
                return connection.Query<int>(getIncompleteApplicantIdsStatement).ToList();
            }
        }

        public List<int> SetApplicantToComplete(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConfigManager.GetConnectionString("Application")))
            {
                var getSetApplicantToCompleteSqlFileStatement = ReadEmbeddedResource(SetApplicantToCompleteSqlFile) + id;
                return connection.Query<int>(getSetApplicantToCompleteSqlFileStatement).ToList();
            }
        }

        public Applicant GetApplicant(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConfigManager.GetConnectionString("Application")))
            {
                var getGetApplicantSqlFileStatement = ReadEmbeddedResource(GetApplicantSqlFile) + id;
                return connection.Query<Applicant>(getGetApplicantSqlFileStatement).First();
            }
        }

        private static string ReadEmbeddedResource(string resourceName)
        {
            string content;
            var assembly = typeof(ApplicantRepository).Assembly;
            resourceName = $"{assembly.GetName().Name}.{resourceName.Replace(" ", "_").Replace("\\", ".").Replace("/", ".")}";
            using (var resourceStream = assembly.GetManifestResourceStream(resourceName))
            {
                using (var reader = new StreamReader(resourceStream))
                {
                    content = reader.ReadToEnd();
                }
            }
            return content;
        }
    }
}