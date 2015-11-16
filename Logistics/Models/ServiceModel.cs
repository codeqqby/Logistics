using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logistics.Models
{
    public class ServiceModel
    {
        private static ServiceModel uniqueInstance;
        private static readonly object locker = new object();
        public string UserName { get; set; }
        public string FirstLetter { get; set; }
        private LogisticsService.Service1Client client;
        /// <summary>
        /// WCF对象
        /// </summary>
        public LogisticsService.Service1Client Client
        {
            get { return client; }
        }

        private ServiceModel()
        {
            this.client = new LogisticsService.Service1Client();
        }

        public static ServiceModel CreateInstance()
        {
            if (uniqueInstance == null)
            {
                lock (locker)
                {
                    if (uniqueInstance == null)
                    {
                        uniqueInstance = new ServiceModel();
                    }
                }
            }
            return uniqueInstance;
        }
    }
}