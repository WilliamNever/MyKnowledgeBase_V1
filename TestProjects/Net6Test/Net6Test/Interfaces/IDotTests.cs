﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6Test.Interfaces
{
    public interface IDotTests : IBase
    {
        int GetId(string num);
        string GetIdx(int num);
    }
    public interface IBase
    { }
}