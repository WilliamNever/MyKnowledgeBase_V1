using System;
using System.Collections.Generic;
using System.Text;

namespace Core31TestProject.Models.AutoMapperTestModels
{
    public class SourceCompany : BaseCompany
    {
        public string Ss_Name { get; set; }
        public List<SDepart> Departs { get; set; }
    }
    public class SDepart
    { 
        public string Dep_Name { get; set; }
        public int? Numbers { get; set; }
        public int Serv_Numbers { get; set; }
        public void GetHH(string ss)
        { }
    }
}
