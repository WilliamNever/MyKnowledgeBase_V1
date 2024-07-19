using F8C.Core.Enums;
using F8C.Core.Interfaces;
using F8C.Core.Models;

namespace F8C.Infrastructure.Services
{
    public class Obj_Num_Service : Base8MethodService<Obj_Num_Model>, ICreate8Method
    {
        public override Result8SymbolModel Get8Symbol(Obj_Num_Model model)
        {
            model.Identity = Identity;
            return base.Get8Symbol(model);
        }
        public Create8Method Identity => Create8Method.Obj_Num;
        public string Name => "Created by Object Number";

        public TService? GetService<TService>() where TService : class => this as TService;

    }
}
