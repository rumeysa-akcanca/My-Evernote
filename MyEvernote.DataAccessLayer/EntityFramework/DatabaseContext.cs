
using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataAccessLayer.EntityFramework
{
   
    
        public class DatabaseContext : DbContext
        {
            //veritabanımızda tablolarımıza karşılık gelen dbsetlerimizi tanımlıyoruz
            public DbSet<EvernoteUser> EvernoteUsers { get; set; }
            public DbSet<Note> Notes { get; set; }
            public DbSet<Comment> Comments { get; set; }
            public DbSet<Category> Categories { get; set; }
            public DbSet<Liked> Likes { get; set; }
        //buradan oluşturacağı tabloları da biliyor etf
        //Databasecontext için bir initializer classı vercez

           public DatabaseContext()
           {
              Database.SetInitializer(new MyInitializer());

           }


        }
    
}
