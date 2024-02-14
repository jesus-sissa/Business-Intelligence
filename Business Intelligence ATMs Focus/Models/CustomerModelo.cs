using System.ComponentModel.DataAnnotations;

namespace Business_Intelligence_ATMs_Focus.Models
{
    public class CustomerModelo 
    {


        public int? customer_id { get; set; }
        public int? ownbranche_id { get; set; }
        //[Required]
        public int? social_reason_id { get; set; }
        [Required]
        public string? client_name { get; set; }
        [Required]
        public string? client_address { get; set; }
        [Required]
        public string? siac_key { get; set; }
        public string? status { get; set; }
        //public string? own_client_id { get; set; }



        public string? own_client_id { get; set; }
        public string? business_name { get; set; }
        public string? branch_name { get; set; }
        public string? rfc { get; set; }



        //public string? server_name { get; set; }
        //public string? dba_name { get; set; }
        //public string? usr_name { get; set; }
        //public string? usr_password { get; set; }


    }
}
