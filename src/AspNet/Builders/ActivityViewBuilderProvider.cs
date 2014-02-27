using System;
using System.Collections.Generic;
using System.Linq;

namespace Inthros.AspNet.Builders
{
    public static class ActivityViewBuilderProvider
    {
        private static readonly List<IActivityViewBuilder> builders = new List<IActivityViewBuilder>();

        static ActivityViewBuilderProvider()
        {
            builders.Add(new DefaultActivityViewBuilder());
        }

        public static IActivityViewBuilder Get(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            return builders.FirstOrDefault(b => b.Supports(type));
        }

        public static void Clear()
        {
            builders.Clear();
        }

        public static void Add(IActivityViewBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            builders.Insert(0, builder);
        }

        public static void Remove(IActivityViewBuilder builder)
        {
            builders.Remove(builder);
        }
    }
}
