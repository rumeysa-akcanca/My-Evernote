using MyEvernote.DataAccessLayer;
using MyEvernote.DataAccessLayer.Abstract;
using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace MyEvernote.DataAccessLayer.EntityFramework
{
  public  class Repository <T>:RepositoryBase,IRepository<T> where T:class
    {
       // private DatabaseContext db;

        //süreklli set'i bulmaya çalışıyoruz:
        private DbSet<T> _objectSet;
        public Repository()
        {
            //db = RepositoryBase.CreateContext();
            _objectSet = context.Set<T>();
        }
        public List<T> List()
        {
           return  _objectSet.ToList();//t'nin instanca alınabilir bir nesne olması gerekiyor
        }
        //istediğimiz kritere göre liste döndüren

        public IQueryable<T> ListQueryable()
            
        { //ıqueryable döncek ne zaman :orderbyladığımızda 
            //ne zaman tolist methodunu çağırdığımızda o zaman sql'e gidiyo olacak
            //bize bir sorguyu çalıştırmadan önceki halini versin ,biz bir işlem yapalım öyle sorgu çalışssın
            return _objectSet.AsQueryable<T>();
        }

        public List<T> List(Expression<Func<T,bool>> where)
        {
            return _objectSet.Where(where).ToList();
            //buna ifadeler eklemek için örneğin order by , ilk 10 kayıt adlı sonraki 3 kaydı ver diyebilsin
            //bu ne zaman tamamlarsa sorgu o zaman çalışsın diyeceksek ıqueryable döndürsün

        }
        public  int Insert(T obje)
        {
            _objectSet.Add(obje);//tabloyu buluyoruz,sonra içine objeyi veriyoruz
            return Save();
        }
        public int Update(T obje)
        {
            //entityframeworkte bir nesneyi elde ederiz find' la ya da sorgulayarak db'den
            //sorguladıktan sonra propertylerde değişiklik yaparız ve savechange cağırırız ve o  update eder
            return Save();
        }
        public int Delete(T obje)
        {
            _objectSet.Remove(obje);
            return Save();
        }
        public int Save() //kaç kayıt etkilennmişse onun adeti döner
        {
          return context.SaveChanges();
        }
        public T Find(Expression<Func<T,bool>> where )
        {
            return _objectSet.FirstOrDefault(where);
        }
    }
}
