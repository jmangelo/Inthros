using System;
using System.Collections.Generic;

namespace Inthros.Core.Utilities
{
    public sealed class InheritanceTypeComparer : IComparer<Type>
    {
        public int Compare(Type x, Type y)
        {
            const int XIsEqualToY = 0;
            const int XIsLessThanY = -1;
            const int XIsGreaterThanY = 1;

            if (x == y)
            {
                return XIsEqualToY;
            }

            if (x == null)
            {
                return XIsLessThanY;
            }

            if (y == null)
            {
                return XIsGreaterThanY;
            }

            if (x.IsSubclassOf(y))
            {
                return XIsLessThanY;
            }

            if (y.IsSubclassOf(x))
            {
                return XIsGreaterThanY;
            }

            return StringComparer.Ordinal.Compare(x.AssemblyQualifiedName, y.AssemblyQualifiedName);
        }
    }
}