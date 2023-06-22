using dnlib.DotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7.ProxyMethods
{
    internal interface Deob
    {
        string Name { get; }

        void RemoveProtection(ModuleDef module);

        int GetResult();

        void Dispose();
    }
}
