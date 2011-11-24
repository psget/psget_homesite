using System;
using Homesite.App.Providers.DirectoryProvider;

namespace Homesite.Views.Directory
{
    public static class ModuleExtensions
    {
        public static bool IsResent(this Module module) 
        {
            return module.Updated > DateTimeOffset.Now.AddDays(-7);
        }        
    }
}