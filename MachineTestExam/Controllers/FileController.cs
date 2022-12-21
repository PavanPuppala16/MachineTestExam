using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Data;
using MachineTestExam.Models;
using MachineTestExam.Logic;

namespace MachineTestExam.Controllers
{
    public class FileController : Controller
    {
        [HttpGet]
        public IActionResult Upload()
        {
            return View(PopulateFiles());
        }

        [HttpPost]
        public IActionResult Upload(List<IFormFile> PostedFiles)
        {
            foreach (IFormFile PostedFile in PostedFiles)
            {
                string fileName = Path.GetFileName(PostedFile.FileName);
                string type = PostedFile.ContentType;
                byte[] bytes = null;
                using (MemoryStream ms = new MemoryStream())
                {
                    PostedFile.CopyTo(ms);
                    bytes = ms.ToArray();
                }
                var dbconfig = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
                string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];
                using (SqlConnection con = new SqlConnection(dbconnectionstr))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "insert into tbl_FileUploadDetails values (@Name,@ContentType,@Data)";

                        cmd.Parameters.AddWithValue("@Name", fileName);
                        cmd.Parameters.AddWithValue("@ContentType", type);
                        cmd.Parameters.AddWithValue("@Data", bytes);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

            }
            return View(PopulateFiles());
        }

        public FileResult DownloadFile(int fileId)
        {
            UploadModel model = PopulateFiles().Find(model => model.ID == Convert.ToInt32(fileId));
            string fileName = model.Name;
            string contentType = model.ContentType;
            byte[] bytes = model.Data;
            return File(bytes, contentType, fileName);

        }

        public static List<UploadModel> PopulateFiles()
        {
            List<UploadModel> Files = new List<UploadModel>();
            var dbconfig = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();
            string dbconnectionstr = dbconfig["ConnectionStrings:DefaultConnection"];
            using (SqlConnection con = new SqlConnection(dbconnectionstr))
            {
                string query = "select * from tbl_FileUploadDetails";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            Files.Add(new UploadModel
                            {
                                ID = Convert.ToInt32(sdr["ID"].ToString()),
                                Name = sdr["Name"].ToString(),
                                ContentType = sdr["ContentType"].ToString(),
                                Data = (byte[])sdr["Data"]
                            });
                        }
                    }
                    con.Close();
                }
            }

            return Files;
        }
        public IActionResult Delete(int fileId)
        {
            bool res = InsertingData.DeleteDataFile((int)fileId);
            //if (res == true)
            {
                return RedirectToAction("Upload");
            }
            // else
            //{
            //     return View();
            //}
            //return View();
        }
        public IActionResult View(int fileId)
        {

            return View();
        }

        public IActionResult Display()
        {
            return View();
        }
    }
}
