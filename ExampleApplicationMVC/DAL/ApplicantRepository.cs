using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using ExampleApplicationMVC.Models;
using ExampleApplicationMVC.Utilities;

namespace ExampleApplicationMVC.DAL
{
    public class ApplicantRepository
    {
        public void CreateApplicant(Applicant applicant)
        {
            using(IDbConnection connection = new SqlConnection(ConfigManager.GetConnectionString("Application")))
            {
                connection.Execute("dbo.Add_Applicant @Name, @Age, @Email, @AboutYou, @Experience, @SkillsTalents, @FileName", applicant);
            }
        }

        public List<int> GetIncompleteApplicants()
        {
            using (IDbConnection connection = new SqlConnection(ConfigManager.GetConnectionString("Application")))
            {
                //todo remove to sql file
                string sqlQuery = "SELECT Id FROM Application.dbo.Applicants WHERE Complete = 'false'";
                return connection.Query<int>(sqlQuery).ToList();
            }
        }

        public List<int> SetApplicantToComplete(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConfigManager.GetConnectionString("Application")))
            {
                //todo remove to sql file
                string sqlQuery = $"update dbo.Applicants set Complete = 'true' where Id = {id}";
                return connection.Query<int>(sqlQuery).ToList();
            }
        }

        public Applicant GetApplicant(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConfigManager.GetConnectionString("Application")))
            {
                //todo remove to sql file
                string sqlQuery = $"SELECT * FROM Application.dbo.Applicants WHERE Id = {id}";
                return connection.Query<Applicant>(sqlQuery).First();
            }
        }
    }
}