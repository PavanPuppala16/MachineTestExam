using System.Data.SqlClient;
using System.Data;
using MachineTestExam.Models;

namespace MachineTestExam.Logic
{
    public class GetID
    {

        public static List<Registration> GETProductListbyID(string? UserId)
        {
            var dbconfig = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];
            using (SqlConnection con = new SqlConnection(dbconnectionstr))
            {
                SqlCommand cmd = new SqlCommand("select * from Register where UserId=@UserId", con);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                con.Open();
                SqlDataReader pdr = cmd.ExecuteReader();
                List<Registration> customers = new List<Registration>();
                if (pdr.HasRows)
                {
                    while (pdr.Read())
                    {
                        customers.Add(new Registration
                        {
                            UserId = Convert.ToInt32(pdr["UserId"]),
                            UserName = Convert.ToString(pdr["UserName"]),
                            EmailId = Convert.ToString(pdr["EmailId"]),
                            Password = Convert.ToString(pdr["Password"]),
                            Branch = Convert.ToString(pdr["Branch"]),
                            PhoneNo = Convert.ToString(pdr["PhoneNo"]),
                            IDProof =Convert.ToString(pdr["IDProof"]),
                            IDno = Convert.ToString(pdr["IDno"]),
                            NoID = Convert.ToString(pdr["NoID"]),
                            JoiningDate = Convert.ToDateTime(pdr["JoiningDate"]),
                            CalculateDate = Convert.ToDateTime(pdr["CalculateDate"]),
                        });
                    }
                    con.Close();
                }
                return customers;
            }

        }

        public static List<DdlModel> PopulateData()
        {
            var dbconfig = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];

            List<DdlModel> customers = new List<DdlModel>();
            using (SqlConnection con = new SqlConnection(dbconnectionstr))
            {
                string query = "SP_GETDISTCUSTOMER";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(new DdlModel
                            {
                                UserId = Convert.ToString(sdr["UserId"])
                            });
                        }
                    }
                    con.Close();
                }
                return customers;
            }
        }

    }
}
