using System;
using System.Collections.Generic;
using System.Text;

namespace Core31TestProject.Models.AutoMapperTestModels
{
    public class DestCompany : BaseCompany
    {
        public string Des_Name { get; set; }
        public List<DDepart> Departs { get; set; }
    }
    public class DDepart : SDepart
    {
        public string Total_Numbers { get; set; }
    }
}
