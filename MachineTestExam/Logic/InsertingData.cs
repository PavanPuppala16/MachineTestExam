using MachineTestExam.Models;
using System.Data;
using System.Data.SqlClient;
namespace MachineTestExam.Logic
{
    public class InsertingData
    {
        public static bool Insertdata(Registration obj)
        {
            bool res = false;
            var dbconfig = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];
            using (SqlConnection con = new SqlConnection(dbconnectionstr))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_insert_Register", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserName", obj.UserName);
                    cmd.Parameters.AddWithValue("@EmailId", obj.EmailId);
                    cmd.Parameters.AddWithValue("@Password", obj.Password);
                    cmd.Parameters.AddWithValue("@Branch", obj.Branch);
                    cmd.Parameters.AddWithValue("@PhoneNo",obj.PhoneNo);
                    cmd.Parameters.AddWithValue("@IDProof", obj.IDProof);
                    cmd.Parameters.AddWithValue("@IDno", obj.IDno);
                    cmd.Parameters.AddWithValue("@NoID", obj.NoID);
                    cmd.Parameters.AddWithValue("@JoiningDate", Convert.ToDateTime(obj.JoiningDate));
                    cmd.Parameters.AddWithValue("@CalculateDate", Convert.ToDateTime(obj.CalculateDate));
                  
                    int x = cmd.ExecuteNonQuery();
                    if (x > 0)
                    {
                        return res = true;
                    }
                    else
                    {
                        return res = false;
                    }
                }
                catch (Exception)
                {

                }
                finally
                {
                    con.Close();
                }
                return res = true;
            }

        }
        public static bool paymentInserting(PaymentMode obj)
        {
            bool res = false;
            var dbconfig = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];
            using (SqlConnection con = new SqlConnection(dbconnectionstr))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Sp_Payment", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", obj.UserId);
                    cmd.Parameters.AddWithValue("@InversementAmout", obj.InversementAmout);
                    cmd.Parameters.AddWithValue("@PaymentMethod", obj.PaymentMethod);
                    cmd.Parameters.AddWithValue("@AccoutNo", obj.AccoutNo);
            
                    cmd.Parameters.AddWithValue("@PaymentDate", Convert.ToDateTime(obj.PaymentDate));


                    int x = cmd.ExecuteNonQuery();
                    if (x > 0)
                    {
                        return res = true;
                    }
                    else
                    {
                        return res = false;
                    }
                }
                catch (Exception)
                {

                }
                finally
                {
                    con.Close();
                }
                return res = true;
            }

        }
        public static DataTable login(LoginModel obj)
        {
            var dbconfig = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];
            using (SqlConnection con = new SqlConnection(dbconnectionstr))
            {
                SqlCommand cmd = new SqlCommand("SP_Login_Register", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", obj.UserName);
                cmd.Parameters.AddWithValue("@EmailId", obj.EmailId);
                cmd.Parameters.AddWithValue("@Password", obj.Password);
                cmd.Parameters.AddWithValue("@Branch", obj.Branch);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        public static List<Registration> GetALLData()
        {
            List<Registration> obj = new List<Registration>();
            var dbconfig = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];
            using (SqlConnection con = new SqlConnection(dbconnectionstr))
            {
                SqlDataAdapter da = new SqlDataAdapter("SP_display", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    obj.Add(
                        new Registration
                        {
                            UserId = Convert.ToInt32(dr["UserId"].ToString()),
                            UserName = dr["UserName"].ToString(),
                            EmailId = dr["EmailId"].ToString(),
                            Password = dr["PASSWORD"].ToString(),
                            Branch = dr["Branch"].ToString(),
                            PhoneNo = dr["PhoneNo"].ToString(),
                            IDProof = dr["IDProof"].ToString(),
                            IDno = dr["IDno"].ToString(),
                            NoID = dr["NoID"].ToString(),
                            JoiningDate = Convert.ToDateTime(dr["JoiningDate"].ToString()),
                             CalculateDate = Convert.ToDateTime(dr["CalculateDate"].ToString())
                        }
                        );
                }
                return obj;
            }
        }
        public static List<PaymentMode> GetALLDataByPayment()
        {
            List<PaymentMode> obj = new List<PaymentMode>();
            var dbconfig = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];
            using (SqlConnection con = new SqlConnection(dbconnectionstr))
            {
                SqlDataAdapter da = new SqlDataAdapter("SP_DisplayPayment", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    obj.Add(
                        new PaymentMode
                        {
                            PaymentID = Convert.ToInt32(dr["PaymentID"].ToString()),
                            UserId = Convert.ToInt32(dr["UserId"].ToString()),
                            InversementAmout = Convert.ToInt32(dr["InversementAmout"].ToString()),


                            PaymentMethod = dr["PaymentMethod"].ToString(),
                            AccoutNo = dr["AccoutNo"].ToString(),
                            PaymentDate = Convert.ToDateTime(dr["PaymentDate"].ToString()),

                        }
                        );
                }
                return obj;
            }
        }


        public static List<Registration> PopulateDataMachine()
        {
            var dbconfig = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];
            List<Registration> fruits = new List<Registration>();
            using (SqlConnection con = new SqlConnection(dbconnectionstr))
            {
                string query = "sp_getCust";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            fruits.Add(new Registration
                            {
                                UserId = Convert.ToInt32(Convert.ToString(sdr["UserId"]))
                            });
                        }
                    }
                    con.Close();
                }
            }
            return fruits;
        }
        public static List<Registration> GetOrdersByCustMachine(int? UserId)
        {
            var dbconfig = new ConfigurationBuilder()
                         .SetBasePath(Directory.GetCurrentDirectory())
                         .AddJsonFile("appsettings.json").Build();



            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];
            using (SqlConnection con = new SqlConnection(dbconnectionstr))
            {
                SqlCommand cmd = new SqlCommand("select * from OrdersData where CustomerID=@CustomerID", con);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                con.Open();
                SqlDataReader idr = cmd.ExecuteReader();
                List<Registration> customers = new List<Registration>();
                if (idr.HasRows)
                {
                    while (idr.Read())
                    {
                        customers.Add(new Registration
                        {
                            UserId = Convert.ToInt32(idr["UserId"]),
                            UserName = Convert.ToString(idr["UserName"]),
                            EmailId = Convert.ToString(idr["EmailId"]),
                            Password = Convert.ToString(idr["Password"]),
                            Branch = Convert.ToString(idr["Branch"]),

                            PhoneNo = Convert.ToString(idr["PhoneNo"]),
                            IDProof = Convert.ToString(idr["IDProof"]),
                            IDno = Convert.ToString(idr["IDno"]),

                            NoID = Convert.ToString(idr["NoID"]),
                            JoiningDate = Convert.ToDateTime(idr["JoiningDate"]),
                            CalculateDate = Convert.ToDateTime(idr["CalculateDate"]),


                        });
                    }
                }
                return customers;
            }
        }

        public static Registration GetDataByID(int UserId)
        {
            Registration obj = null;
            var dbconfig = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];
            using (SqlConnection con = new SqlConnection(dbconnectionstr))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllDataByID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    obj = new Registration();
                    obj.UserId = Convert.ToInt32(ds.Tables[0].Rows[i]["UserId"].ToString());
                    obj.UserName = ds.Tables[0].Rows[i]["UserName"].ToString();
                    obj.EmailId = ds.Tables[0].Rows[i]["EmailId"].ToString();
                    obj.Password = ds.Tables[0].Rows[i]["Password"].ToString();
                    obj.Branch = ds.Tables[0].Rows[i]["Branch"].ToString();
                    obj.PhoneNo = ds.Tables[0].Rows[i]["PhoneNo"].ToString();
                    obj.IDProof = ds.Tables[0].Rows[i]["IDProof"].ToString();
                    obj.IDno = ds.Tables[0].Rows[i]["IDno"].ToString();
                    obj.NoID = ds.Tables[0].Rows[i]["NoID"].ToString();
                    obj.JoiningDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["JoiningDate"].ToString());
                    obj.CalculateDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["CalculateDate"].ToString());
                }
                return obj;
            }
        }


        public static bool UpdateData(Registration obj)
        {
            bool res = false;
            var dbconfig = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];
            using (SqlConnection con = new SqlConnection(dbconnectionstr))
                try
                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_Update_Register", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", obj.UserId);
                    cmd.Parameters.AddWithValue("@UserName", obj.UserName);
                    cmd.Parameters.AddWithValue("@EmailId", obj.EmailId);

                    cmd.Parameters.AddWithValue("@Password", obj.Password);
                    cmd.Parameters.AddWithValue("@Branch", obj.Branch);
                    cmd.Parameters.AddWithValue("@PhoneNo", obj.PhoneNo);
                    cmd.Parameters.AddWithValue("@IDProof", obj.IDProof);
                  cmd.Parameters.AddWithValue("@IDno", obj.IDno);
                    cmd.Parameters.AddWithValue("@NoID", obj.NoID);
                    cmd.Parameters.AddWithValue("@JoiningDate", Convert.ToDateTime(obj.JoiningDate));
                    cmd.Parameters.AddWithValue("@CalculateDate", Convert.ToDateTime(obj.CalculateDate));

                   
                    int x = cmd.ExecuteNonQuery();
                    if (x > 0)
                    {
                        return res = true;
                    }
                    else
                    {
                        return res = false;
                    }

                }
                catch (Exception)
                {
                    return res = true;
                }

        }

        public static bool DeleteData(int UserId)
        {
            bool res = false;
            var dbconfig = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];
            using (SqlConnection con = new SqlConnection(dbconnectionstr))
                try
                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_DELETE_DATA", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    int x = cmd.ExecuteNonQuery();
                    if (x > 0)
                    {
                        return res = true;
                    }
                    else
                    {
                        return res = false;
                    }

                }
                catch (Exception)
                {
                    return res = true;
                }

        }


        public static bool UPDATEDATABYEMAILID(ForgetPasswordModel OBJ)
        {
            bool res = false;
            var dbconfig = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];
            using (SqlConnection con = new SqlConnection(dbconnectionstr))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_ResetPassword_Register", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmailId", OBJ.EmailId);
                    cmd.Parameters.AddWithValue("@Password", OBJ.Password);
                    int x = cmd.ExecuteNonQuery();
                    if (x > 0)
                    {
                        return res = true;
                    }
                    else
                    {
                        return res = false;
                    }
                }
                catch (Exception)
                {

                }
                finally
                {
                    con.Close();
                }
                return res = true;
            }

        }
        public static bool DeleteDataFile(int id)
        {
            bool res = false;
            var dbconfig = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];
            using (SqlConnection con = new SqlConnection(dbconnectionstr))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_delete_UploadFile", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    int x = cmd.ExecuteNonQuery();
                    if (x > 0)
                    {
                        return res = true;
                    }
                    else
                    {
                        return res = false;
                    }
                }
                catch (Exception)
                {

                }
                finally
                {
                    con.Close();
                }
                return res = true;
            }
        }


    }


}
