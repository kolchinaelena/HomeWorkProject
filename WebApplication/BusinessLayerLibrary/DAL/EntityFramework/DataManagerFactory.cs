using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLayerLibrary.DAL;
using BusinessLayerLibrary.DAL.EntityFramework;
using BusinessLayerLibrary.Infrastructure;


namespace BusinessLayerLibrary.DAL
{
   public class DataManagerFactory: IDataManagerFactory
    {
        private readonly IDbConfig config;
        public DataManagerFactory(IDbConfig Config)
        {
            config = Config;
        }

        public IDataManager GetDataManager()
        {
            return new DataManager(config); 
        }

        public IDataManager GetDataManager(ConcurrencyLock lockType)
        {
            return new DataManager(config, lockType);
        }

    }

}
