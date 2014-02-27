using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inthros.AspNet.Views;
using Inthros.Core.Utilities;

namespace Inthros.AspNet.Builders
{
    public abstract class ActivityViewBuilder<T> : ActivityViewBuilder, IActivityViewBuilder<T>
    {
        private static readonly IReadOnlyCollection<Type> supportedTypes = new ReadOnlyCollection<Type>(new[] { typeof (T) });

        public abstract ActivityView Build(T activity);

        public sealed override ActivityView Build(object activity)
        {
            if (activity == null)
            {
                throw new ArgumentNullException("activity");
            }

            if (!this.Supports(activity.GetType()))
            {
                throw new ArgumentException("", "activity");
            }

            return this.Build((T) activity);
        }

        public sealed override IReadOnlyCollection<Type> SupportedTypes {
            get { return supportedTypes; }
        }
    }

    public abstract class ActivityViewBuilder : IActivityViewBuilder
    {
        protected bool IsCompatibleType(Type source, Type target)
        {
            return target.IsAssignableFrom(source) || source.IsConstructedGenericTypeOf(target);
        }

        public bool Supports(Type target)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            return this.SupportedTypes.Any(t => IsCompatibleType(target, t));
        }

        public abstract ActivityView Build(object activity);

        public abstract IReadOnlyCollection<Type> SupportedTypes { get; }
    }
}
