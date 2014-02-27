using System;
using System.Collections.Generic;

namespace Inthros.Core.Traversing
{
    public interface IActivityDescriptor
    {
        bool Supports(Type target);

        ActivityInfo[] GetChildren(object activity);

        IReadOnlyCollection<Type> SupportedTypes { get; }
    }
}
