using System.ComponentModel.DataAnnotations;

namespace MachineTestExam.Models
{
    public class LoginModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "What's ur Name*")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required*")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string EmailId { get; set; }
        [Required(ErrorMessage = "Please enter password*")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Password \"{0}\" must have {2} character", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Password must contain: Minimum 8 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character")]
        public string Password { get ; set; }

        [Required(ErrorMessage = "Select role*")]
        public string Branch { get; set; }
    }
    public class Registration
    {
        [Key]
        public int UserId { get; set; }
    
        [Required(ErrorMessage = "What's ur Name*")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required*")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string EmailId { get; set; }
        [Required(ErrorMessage = "Please enter password*")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Password \"{0}\" must have {2} character", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Password must contain: Minimum 8 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character")]

        public string Password { get; set; }

        [Required(ErrorMessage = "Select role*")]
        public string Branch { get; set; }

        [Required(ErrorMessage = "Mobile is required*")]
        [RegularExpression(@"\d{10}", ErrorMessage = "Please enter 10 digit Mobile No.")]
        public string PhoneNo { get; set; }
        [Required(ErrorMessage = " IDProof is required*")]
        public string IDProof { get; set; }

        [Required(ErrorMessage = "Mobile is required*")]
        [RegularExpression(@"\d{12}", ErrorMessage = "Please enter 10 digit Mobile No.")]
        public string IDno { get; set; }
        
        public string NoID { get; set; }
        public  DateTime JoiningDate { get; set; }

        public DateTime CalculateDate { get; set; }



    }


    public class ForgetPasswordModel
    {

        [Required(ErrorMessage = "Email is required*")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string EmailId { get; set; }


        [Required(ErrorMessage = "Please enter password*")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Password \"{0}\" must have {2} character", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Password must contain: Minimum 8 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character")]
        public string Password { get; set; }
    }

    public class OtpModel
    {
        public string Otp { get; set; }
    }
    public class UploadModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }


    public class PaymentMode
    {
        [Key]
        public int PaymentID { get; set; }

        public int UserId { get; set; }
        public int InversementAmout { get; set; }

        public string PaymentMethod { get; set; }
        public string AccoutNo { get; set; }
        public DateTime PaymentDate { get; set; }



    }
    public class DdlModel
    {
        public string UserId { get; set; }
    }
}
