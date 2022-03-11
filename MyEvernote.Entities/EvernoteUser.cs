using MyEvernote.Entities.ValueObject;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities
{
    [Table("EvernoteUsers")]
    public class EvernoteUser: MyEntityBase
    {
        [StringLength(25)]
        public string Name { get; set; }
        [StringLength(25)]
        public string  Surname { get; set; }
        [Required,StringLength(25)]
        public string Username { get; set; }
        [Required,StringLength(70)]
        public string Email { get; set; }
        [Required,StringLength(25)]
        public string Password { get; set; }
        //Profil Sayfası için
        [StringLength(30)]// images klasörünün altında/user_12.jpg
        public string ProfileImageFilename { get; set; }//dosyanın adı ve uzantısı??
        public bool IsActive { get; set; }//maile gelen linke tıklayınca aktive edecez
        public bool IsAdmin { get; set; }//Kullanıcı admin (yönetici) mi?
        [Required]
        public Guid ActivateGuid { get; set; }//activate kodu eşleşmasi yapmalıyız
        //linkten aldığımız ıd'ye göre kullanıcıyıdb'den bulup activate etmeliyiz
        //guid eşsiz oluyo
        
                                         //adminin biri girip kullnıcıda düzeeltme yapabilir mi yad a kullanıcını kendisi.. bunun için MyBlogUser metotlarını  alıyoruz
        public virtual List<Note> Notes { get; set; }
        //bir kullanıcının birden çok notu var kendi oluşturduğu likeladığı değil
        public virtual List<Comment> Comments { get; set; }
        //bir kullanıcının birden çok yorumu var
        public virtual List<Liked> Likes { get; set; }//bir kullanıcının da birden çok like'ı vardır

       
    }
}
