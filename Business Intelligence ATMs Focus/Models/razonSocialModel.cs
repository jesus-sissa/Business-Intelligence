using System.ComponentModel.DataAnnotations;

namespace Business_Intelligence_ATMs_Focus.Models
{
    public class razonSocialModel
    {
        public int? social_reason_id { get; set; }
        [Required (ErrorMessage ="Requerido {0}")]
        [StringLength(12,ErrorMessage ="")]
        public string? rfc { get; set; }
        [Required]
        public string? business_name { get; set; }
        [Required]
        public string? businnes_address { get; set; }
  
        public string? status { get; set; }
        
    }
}
