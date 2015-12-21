using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace BusinessLayerLibrary.Infrastructure
{
    /// <summary>
    /// Реализация с хранением конфигурации в web.config
    /// </summary>
    public class Config : IDbConfig
    {
        /// <summary>
        /// Строка подключения с именем PrimaryDatabase в секции connectionStrings
        /// </summary>
        public string SqlConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["PrimaryDatabase"].ConnectionString; }
        }

       
   
    }
}
