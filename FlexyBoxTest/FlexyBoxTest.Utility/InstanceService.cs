using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FlexyBoxTest.Utility.Extensions;

namespace FlexyBoxTest.Utility
{
    //Task 2.1
    public class InstanceService
    {
        // Got help with fixing a test specific issue via this site:
        // https://haacked.com/archive/2012/07/23/get-all-types-in-an-assembly.aspx/

        //Task 2.1
        public IEnumerable<T> GetInstances<T>()
        {
            var output = new List<T>();

            var type = typeof(T);

            if (type.IsClass && !type.IsAbstract)
            {
                output.Add(Activator.CreateInstance<T>());
            }

            else if (type.IsInterface)
            {
                var types = GetAssemblyTypes().Where(x => x.IsClass && !x.IsAbstract && type.IsAssignableFrom(x)).ToList();

                output = AddImplementations(output, types);
            }

            else if (type.IsAbstract)
            {
                var types = GetAssemblyTypes().Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(type)).ToList();
                
                output = AddImplementations(output, types);
            }

            return output;
        }
        
        private List<T> AddImplementations<T>(List<T> output, IEnumerable<Type> types)
        {
            output.AddRange(types.Select(implementations => (T) Activator.CreateInstance(implementations)));
            return output;
        }

        private IEnumerable<Type> GetAssemblyTypes()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetLoadableTypes());
        }
    }
}