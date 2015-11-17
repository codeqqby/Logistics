using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logistics.Models
{
    public class BuildModel
    {
        public List<string> FirstLetter { get; set; }
        public List<BuildInfo> Build { get; set; }

        public BuildModel()
        {
            this.FirstLetter = new List<string>();
            this.Build = new List<BuildInfo>();
        }
    }

    public class BuildInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}