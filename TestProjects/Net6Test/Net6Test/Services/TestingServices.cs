using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6Test.Services
{
    public class TestingServiceMain
    {
        public Guid SID { get; private set; }
        public TestingServiceInjected injection;
        public TestingServiceMain(TestingServiceInjected inject)
        {
            SID = Guid.NewGuid();
            injection = inject;
        }
    }
    public class TestingServiceInjected
    {
        public Guid SID { get; private set; }
        public TestingServiceInjected()
        {
            SID = Guid.NewGuid();
        }
    }
}
