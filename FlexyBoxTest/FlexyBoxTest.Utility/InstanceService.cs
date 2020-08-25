using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FlexyBoxTest.Utility
{
    public class InstanceService
    {
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
                var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                    .Where(x => x.IsClass && !x.IsAbstract && type.IsAssignableFrom(x)).ToList();
                
                output.AddRange(types.Select(implementations => (T) Activator.CreateInstance(implementations)));
            }

            else if (type.IsAbstract)
            {

                

                var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                    .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(type)).ToList();

                output.AddRange(types.Select(implementations => (T)Activator.CreateInstance(implementations)));
            }

            return output;
        }
    }
}