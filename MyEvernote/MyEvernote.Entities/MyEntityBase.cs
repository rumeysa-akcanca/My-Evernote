using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities
{
  public  class MyEntityBase
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]//prinmary key ve oto artan
        public int Id { get; set; }
        [Required]
        public DateTime CretedOn { get; set; }//oluşturulduğu zaman
                                              //kimin tarafından değiştirildiği en son ne zaman güncellendiği saklamak istiyor muyuz
        [Required]//boş geçilemez
        public DateTime ModifiedOn { get; set; }//Düzenlendiği tarih
        [Required,StringLength(30)]
        public string ModifiedUsername { get; set; }//Kimin tarafından
    }
}
