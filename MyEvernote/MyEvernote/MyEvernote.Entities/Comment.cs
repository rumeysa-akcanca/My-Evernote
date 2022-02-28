using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities
{
    [Table("Comments")]
    public  class Comment:MyEntityBase
    {
        [Required,StringLength(250)]
        public string Text { get; set; }
        //Arasındaki ilişkileri oluşturmamız lazım
        public virtual Note Note { get; set; }//Hangi note yorum
        public virtual EvernoteUser Owner { get; set; }//Hangi kullanıcı yazdı bu yorumu

    }
}
