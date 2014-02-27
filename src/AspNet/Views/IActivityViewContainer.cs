using System.Collections.Generic;

namespace Inthros.AspNet.Views
{
    public interface IActivityViewContainer
    {
        void Add(ActivityView view);

        IReadOnlyCollection<ActivityView> ActivityViews { get; }
    }
}
