using System;

namespace Inthros.Core.Utilities
{
    public static class TypeExtensions
    {
        public static bool IsConstructedGenericTypeOf(this Type type, Type openGenericType)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (openGenericType == null)
            {
                throw new ArgumentNullException("openGenericType");
            }

            return type.IsGenericType && type.GetGenericTypeDefinition() == openGenericType;
        }
    }
}