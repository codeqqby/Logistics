using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logistics.Models
{
    public class ProjectModel
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string ProjectUses { get; set; }
        public string Area1 { get; set; }
        public string Area2 { get; set; }
        public string Area3 { get; set; }
        public string Area4 { get; set; }
        public string Area5 { get; set; }
        public string Area6 { get; set; }
        public string MachineType { get; set; }
        public string ProHouseType { get; set; }
        public string Address0 { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Address5 { get; set; }
        public string Address6 { get; set; }
        public string ProjectAddress { get; set; }
        public string CustomerName { get; set; }
        public string CustomerTel { get; set; }
        public string Price { get; set; }
        public string ProjectStatus { get; set; }
        public string ProjectType { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}