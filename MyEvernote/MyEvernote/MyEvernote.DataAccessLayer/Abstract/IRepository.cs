using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataAccessLayer.Abstract
{
    interface IRepository<T>
    {

        List<T> List();


        IQueryable<T> ListQueryable();// ılist,ıcollection ,ıenuram... bak



        List<T> List(Expression<Func<T, bool>> where);


        int Insert(T obje);


        int Update(T obje);


        int Delete(T obje);

        int Save();

        T Find(Expression<Func<T, bool>> where);
       
    }
}

