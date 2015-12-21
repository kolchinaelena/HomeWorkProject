using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.IO;
using  System.Reflection;
using BusinessLayerLibrary.Domain.Model;



namespace BusinessLayerLibrary.DAL.EntityFramework.Repositories
{
   public class RepositoryBase
    {
        public ContextModel mContext { get; private set; }

        public  DbContextTransaction mTransaction { get; private set; }

       public ConcurrencyLock? Lock { get; set; }

        public RepositoryBase(ContextModel context, DbContextTransaction transaction)
        {
            mContext = context;
            mTransaction = transaction;
        }
       

      
    }
}
