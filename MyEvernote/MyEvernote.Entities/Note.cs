using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities
{
    [Table("Notes")]
   public class Note:MyEntityBase
    {
        [Required, StringLength(60)]
        public string Title { get; set; }
        [Required,StringLength(2000)]
        public string Text { get; set; }
        public bool IsDraft { get; set; }//Taslak mı
        public int LikeCount { get; set; }//like sayısı
        public virtual EvernoteUser Owner { get; set; }//kim yaptı bu notu
        public virtual List<Comment> Comments { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }//her notun bir kategorisi vardır
        public virtual List<Liked> Likes { get; set; }//bir notun birden çok like'ı vardır

        public Note()
        {
            Comments = new List<Comment>();
            Likes = new List<Liked>();
        }
    }
}
