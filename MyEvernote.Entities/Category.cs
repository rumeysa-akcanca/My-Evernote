using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities
{
    [Table("Categories")]
  public   class Category: MyEntityBase
        //Category sınıfının  tipi MyEntityBase olarak da tanımlanabilir
    {
        [Required,StringLength(50)]  
        public string  Title { get; set; }
        [StringLength(150)]
        public string Description { get; set; }
        public virtual List<Note> Notes { get; set; }//Bir categorinin birden çok notu var
        //Başka bir class'la ilişkili olduğu için bunları virtual olarak tanımladık
        public Category()
        {
            //category newlendiği zaman,ctor oluştuktan sonra notların otomatik olarak listesinin oluşmasını
            //sağlıyoruz not eklemk istediğimizda null hatası almayalım

            Notes = new List<Note>();
        }

    }
}
