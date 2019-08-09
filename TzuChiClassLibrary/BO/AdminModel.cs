using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TzuChiClassLibrary.BO
{
    public class AdminModel
    {
        public string AdminID { get; set; }
        [Required(ErrorMessage = "請輸入帳號")]
        public string Account { get; set; }
        [Required(ErrorMessage = "請輸入密碼")]
        public string Password { get; set; }
        [Required(ErrorMessage = "請輸入名稱")]
        public string Name { get; set; }
        public string Tel { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string LastLogonIP { get; set; }
        public DateTime LastLogonTime { get; set; }
        public Boolean Enable { get; set; }
        public Int32 TotalNum { get; set; }
    }
}
