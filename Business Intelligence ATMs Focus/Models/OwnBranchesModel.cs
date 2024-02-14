using System.ComponentModel.DataAnnotations;

namespace Business_Intelligence_ATMs_Focus.Models
{
    public class OwnBranchesModel
    {
        public int ownbranche_id { get; set; }
        [Required]
        public string branch_name { get; set; }
        [Required]
        public string server_name { get; set; }
        [Required]
        public string dba_name { get; set; }
        [Required]
        public string usr_name { get; set; }
        [Required]
        public string usr_password { get; set; }

        public string status { get; set; }


        public string rs_id { get; set; }
        public string rfc { get; set; }


    }
}
