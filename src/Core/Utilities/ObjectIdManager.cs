using System;
using System.Runtime.CompilerServices;

namespace Inthros.Core.Utilities
{
    public static class ObjectIdManager
    {
        private static readonly ConditionalWeakTable<object, string> identifiers = new ConditionalWeakTable<object, string>();

        public static string GetId(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            string id;
            if (!identifiers.TryGetValue(value, out id))
            {
                id = Guid.NewGuid().ToString();
                identifiers.Add(value, id);
            }

            return id;
        }
    }
}
