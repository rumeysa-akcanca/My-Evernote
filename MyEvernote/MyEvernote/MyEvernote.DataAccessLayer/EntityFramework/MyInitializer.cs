using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MyEvernote.Entities;

namespace MyEvernote.DataAccessLayer.EntityFramework
{
   public  class MyInitializer:CreateDatabaseIfNotExists<DatabaseContext>
    {
        
        //ne zaman çalışacağını belirtmeliyiz//Context'i tip olarak verdik
        protected override void Seed(DatabaseContext context)
        {
            //Database oluştuktan sonra örnek data basımında kullanılan methot seed
            //Adding admin user..
            EvernoteUser admin = new EvernoteUser()
            {
                Name = "Rumeysa",
                Surname = "Akcanca",
                Email = "rumeysacanca@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                Username = "Sauron",
                Password = "1234556",
                CretedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUsername = "rumeysacacnca"
            };
            //Adding standart user..
            EvernoteUser standartUser = new EvernoteUser()
            {
                Name = "Gandalf",
                Surname = "Bladorthin",
                Email = "gandalfwizard@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = false,
                Username = "Greypilgrim",
                Password = "5678932",
                CretedOn = DateTime.Now.AddHours(1),
                ModifiedOn = DateTime.Now.AddMinutes(65),
                ModifiedUsername = "rumeysacanca"
            };
            context.EvernoteUsers.Add(admin);//tabloya kayıt
            context.EvernoteUsers.Add(standartUser);

            for (int i = 0; i < 8; i++)
            {
                EvernoteUser user = new EvernoteUser()
                {
                    Name = FakeData.NameData.GetFirstName(),
                    Surname = FakeData.NameData.GetSurname(),
                    Email = FakeData.NetworkData.GetEmail(),
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = true,
                    IsAdmin = false,
                    Username = $"user{i}",
                    Password = "123",
                    CretedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedUsername = $"user{i}"
                };
                context.EvernoteUsers.Add(user);
            }
            context.SaveChanges();//kayıt
            //User list for using..
            List<EvernoteUser> userlist = context.EvernoteUsers.ToList();
            //Adding Fake Categori: 10 tane örnek kategorimiz var
            for (int i = 0; i < 10; i++)
            {
                Category category = new Category()
                {
                    Title = FakeData.PlaceData.GetStreetName(),
                    Description=FakeData.PlaceData.GetAddress(),
                    CretedOn =DateTime.Now,
                    ModifiedOn=DateTime.Now,
                    ModifiedUsername="rumeysacanca"
                };
                context.Categories.Add(category);
                //Adding  fake notes...
                // kategorilerde notlar var
                for (int k = 0; k < FakeData.NumberData.GetNumber(5,9); k++)
                    //her bir kategori için min 5 max 9 not
                {
                    EvernoteUser owner = userlist[FakeData.NumberData.GetNumber(0, userlist.Count - 1)]; //diziden random bir şekilde kullanıcı çekiyoruz

                    Note note = new Note()
                    {
                        Title=FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(5,25)),
                        Text=FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1,3)),
                       // Category=category,
                        IsDraft=false,
                        LikeCount=FakeData.NumberData.GetNumber(1,9),
                        Owner = owner,
                        CretedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedOn  = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedUsername = owner.Username,
                    };

                    category.Notes.Add(note);//notun da yorumu vardır

                    //Adding fake comments
                    for (int j = 0; j < FakeData.NumberData.GetNumber(3,5); j++)
                    {
                        EvernoteUser comment_owner = userlist[FakeData.NumberData.GetNumber(0, userlist.Count - 1)];
                     
                        Comment comment = new Comment()
                        {
                            Text = FakeData.TextData.GetSentence(),
                            Note =note,
                            Owner = comment_owner,
                            CretedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedUsername = comment_owner.Username

                        };
                        note.Comments.Add(comment);//notun commentlerine ekliyoruz
                    }
                    //Adding fake likes..
                   
                    for (int m = 0; m < note.LikeCount; m++)
                    {
                        Liked liked = new Liked()
                        {
                            LikedUser = userlist[m]
                        };
                        note.Likes.Add(liked);
                    }

                }
            }
            context.SaveChanges();
        }
    }
}
