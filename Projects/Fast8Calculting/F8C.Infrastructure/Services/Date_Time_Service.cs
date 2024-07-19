using F8C.Core.Consts;
using F8C.Core.Enums;
using F8C.Core.Interfaces;
using F8C.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F8C.Infrastructure.Services
{
    public class Date_Time_Service : Base8MethodService<Date_Time_Model>, ICreate8Method
    {
        public override Result8SymbolModel Get8Symbol(Date_Time_Model model)
        {
            model.Identity = Identity;
            return base.Get8Symbol(model);
        }
        public Create8Method Identity => Create8Method.Date_Time;

        public string Name => "Created by Date[year/12, month, day] & Time/12";

        public TService? GetService<TService>() where TService : class => this as TService;
    }
}
