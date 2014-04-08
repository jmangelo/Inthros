using System.Collections.Generic;

namespace Inthros.AspNet.Views
{
    public interface IActivityViewContainer
    {
        // TODO : P1 : Pass context information in add method (parent property)

        void Add(ActivityView view);

        IReadOnlyCollection<ActivityView> ActivityViews { get; }
    }
}
