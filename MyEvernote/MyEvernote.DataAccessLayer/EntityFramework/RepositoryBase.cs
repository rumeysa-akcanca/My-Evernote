using MyEvernote.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataAccessLayer.EntityFramework
{
  public  class RepositoryBase// bu class newlenerek çağrılamaz
    {
        protected static DatabaseContext context;//static methotlar static değişkenlere erişebilir
        private static object _lockSync = new object();
        protected RepositoryBase()
        {
            //contstructor protected ise artık bu class newlenemez miras alan bunu sadece newleyebilir
            //bu classın newlenmemesini istiyoruz
           CreateContext();

        }
        private static void CreateContext()
        {
            //kontrollü bir şekilde databsecontex nesnesinin oluşumunu sağlıyoruz
            //nullsa newleyecek null değilse hep var olanı kullanacak
            if (context==null)
            {
                //bazen if'in içerisine aynı anda 2 tane iş parçacığı girip iki kere newlenebiliyo
                //bu gibi durumların kontrolü için: lock yapısı:kitleme yapılır
                lock (_lockSync)
                {
                    //aynı anda 2 iş parçacığı çalıştırılmaz
                    if (context== null)
                    {
                      //ekstra işi garantiye alma
                      context = new DatabaseContext();
                    }
                }
            }
         
        }
    }
}
