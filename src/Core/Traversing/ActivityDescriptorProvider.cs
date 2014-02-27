using System;
using System.Collections.Generic;
using System.Linq;

namespace Inthros.Core.Traversing
{
    public static class ActivityDescriptorProvider
    {
        private static readonly List<IActivityDescriptor> descriptors = new List<IActivityDescriptor>();

        static ActivityDescriptorProvider()
        {
            descriptors.Add(new DefaultActivityDescriptor());
        }

        public static IActivityDescriptor Get(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            return descriptors.FirstOrDefault(d => d.Supports(type));
        }

        public static void Clear()
        {
            descriptors.Clear();
        }

        public static void Add(IActivityDescriptor descriptor)
        {
            if (descriptor == null)
            {
                throw new ArgumentNullException("descriptor");
            }

            descriptors.Insert(0, descriptor);
        }

        public static void Remove(IActivityDescriptor descriptor)
        {
            descriptors.Remove(descriptor);
        }
    }
}
