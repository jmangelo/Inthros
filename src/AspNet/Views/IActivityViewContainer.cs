using System.Collections.Generic;

namespace Inthros.AspNet.Views
{
    public interface IActivityViewContainer
    {
        void Add(ActivityView view, string scope);

        IReadOnlyCollection<ActivityView> ActivityViews { get; }
    }
}
