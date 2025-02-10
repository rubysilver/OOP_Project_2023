using Data_Layer.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Data_Layer.DAL
{
    public static class DataFactory
    {        
        public static IData GetData()
        {
            CreateConfiguration();
            return LoadInstance(); 
        }
        public static void CreateConfiguration()
        {
            ConfigUtils.CreateConfigurationFile();
        }
        private static IData LoadInstance()
        {
            try
            {
                string configuration = ConfigUtils.GetStandardSetting("Repository");

                if (string.IsNullOrEmpty(configuration))
                {
                    throw new Exception();
                }

                string fullClassName = $"{typeof(IData).Namespace}.{configuration}";
                Type type = Type.GetType(fullClassName);

                if (type == null || !type.IsClass || type.IsAbstract)
                {
                    throw new Exception();
                }

                return Activator.CreateInstance(type) as IData;
            }
            catch 
            {
                return new API();
            }
        }
    }
}
