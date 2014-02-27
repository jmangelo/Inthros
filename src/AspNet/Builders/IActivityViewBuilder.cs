using System;
using System.Collections.Generic;
using Inthros.AspNet.Views;

namespace Inthros.AspNet.Builders
{
    public interface IActivityViewBuilder<T> : IActivityViewBuilder
    {
        ActivityView Build(T activity);
    }

    public interface IActivityViewBuilder
    {
        bool Supports(Type target);

        ActivityView Build(object activity);

        IReadOnlyCollection<Type> SupportedTypes { get; }
    }
}
