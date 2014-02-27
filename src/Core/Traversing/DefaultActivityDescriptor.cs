using System;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Inthros.Core.Utilities;

namespace Inthros.Core.Traversing
{
    public sealed class DefaultActivityDescriptor : IActivityDescriptor
    {
        private static readonly IDictionary<Type, Func<object, ActivityInfo[]>> activityTypeToGetChildrenFunctionMap;

        private static readonly ReadOnlyCollection<Type> supportedActivityTypes;

        static DefaultActivityDescriptor()
        {
            var typeComparer = new InheritanceTypeComparer();

            activityTypeToGetChildrenFunctionMap = new SortedDictionary<Type, Func<object, ActivityInfo[]>>(typeComparer)
            {
                { typeof (Sequence), a => GetSequenceChildren((Sequence) a) },
                { typeof (If), a => GetIfChildren((If) a) },
                { typeof (Parallel), a => GetParallelChildren((Parallel) a) },
                { typeof (DoWhile), a => GetDoWhileChildren((DoWhile) a) },
                { typeof (While), a => GetWhileChildren((While) a) },
                { typeof (Pick), a => GetPickChildren((Pick) a) },
                { typeof (PickBranch), a => GetPickBranchChildren((PickBranch) a) },
                { typeof (Assign), a => new ActivityInfo[0] },
                { typeof (WriteLine), a => new ActivityInfo[0] },
                { typeof (Delay), a => new ActivityInfo[0] },
                { typeof (ForEach<>), a => GetForEachChildren((dynamic) a) },
                { typeof (ParallelForEach<>), a => GetParallelForEachChildren((dynamic) a) },
                { typeof (Persist), a => new ActivityInfo[0] },
                { typeof (Assign<>), a => new ActivityInfo[0] },
                { typeof (Activity), a => GetActivityChildren((Activity) a) },
            };

            supportedActivityTypes = new ReadOnlyCollection<Type>(activityTypeToGetChildrenFunctionMap.Keys.ToList());
        }

        public bool Supports(Type target)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            return this.SupportedTypes.Any(t => IsCompatibleType(target, t));
        }

        public IReadOnlyCollection<Type> SupportedTypes
        {
            get { return supportedActivityTypes; }
        }

        public ActivityInfo[] GetChildren(object activity)
        {
            if (activity == null)
            {
                throw new ArgumentNullException("activity");
            }

            var activityType = activity.GetType();

            var keyType = FindCompatibleSuppportedType(activityType);

            if (keyType == null)
            {
                string message = string.Format(
                    CultureInfo.InvariantCulture,
                    "The activity type ({0}) is not supported.",
                    activityType.FullName);

                throw new ArgumentException(message, "activity")
                {
                    Data = { { "SupportedTypes", this.SupportedTypes } },
                };
            }

            return activityTypeToGetChildrenFunctionMap[keyType](activity);
        }

        private static ActivityInfo[] GetSequenceChildren(Sequence activity)
        {
            var children = new List<ActivityInfo>(activity.Activities.Count);

            foreach (var child in activity.Activities)
            {
                children.Add(new ActivityInfo(child, activity, "Activities"));
            }

            return children.ToArray();
        }

        private static ActivityInfo[] GetIfChildren(If activity)
        {
            var children = new List<ActivityInfo>();

            if (activity.Then != null)
            {
                children.Add(new ActivityInfo(activity.Then, activity, "Then"));
            }

            if (activity.Else != null)
            {
                children.Add(new ActivityInfo(activity.Else, activity, "Else"));
            }

            return children.ToArray();
        }

        private static ActivityInfo[] GetParallelChildren(Parallel activity)
        {
            var children = new List<ActivityInfo>(activity.Branches.Count);

            foreach (var child in activity.Branches)
            {
                children.Add(new ActivityInfo(child, activity, "Branches"));
            }

            return children.ToArray();
        }

        private static ActivityInfo[] GetDoWhileChildren(DoWhile activity)
        {
            var children = new List<ActivityInfo>();

            if (activity.Body != null)
            {
                children.Add(new ActivityInfo(activity.Body, activity, "Body"));
            }

            return children.ToArray();
        }

        private static ActivityInfo[] GetWhileChildren(While activity)
        {
            var children = new List<ActivityInfo>();

            if (activity.Body != null)
            {
                children.Add(new ActivityInfo(activity.Body, activity, "Body"));
            }

            return children.ToArray();
        }

        private static ActivityInfo[] GetPickChildren(Pick activity)
        {
            var children = new List<ActivityInfo>(activity.Branches.Count);

            foreach (var child in activity.Branches)
            {
                children.Add(new ActivityInfo(child, activity, "Branches"));
            }

            return children.ToArray();
        }

        private static ActivityInfo[] GetPickBranchChildren(PickBranch activity)
        {
            var children = new List<ActivityInfo>();

            if (activity.Trigger != null)
            {
                children.Add(new ActivityInfo(activity.Trigger, activity, "Trigger"));
            }

            if (activity.Action != null)
            {
                children.Add(new ActivityInfo(activity.Action, activity, "Action"));
            }

            return children.ToArray();
        }

        private static ActivityInfo[] GetForEachChildren<T>(ForEach<T> activity)
        {
            var forEach = (dynamic) activity;

            var children = new List<ActivityInfo>();

            if (forEach.Body != null && forEach.Body.Handler != null)
            {
                children.Add(new ActivityInfo(forEach.Body.Handler, activity, "Body"));
            }

            return children.ToArray();
        }

        private static ActivityInfo[] GetParallelForEachChildren<T>(ParallelForEach<T> activity)
        {
            var parallelForEach = (dynamic) activity;

            var children = new List<ActivityInfo>();

            if (parallelForEach.Body != null && parallelForEach.Body.Handler != null)
            {
                children.Add(new ActivityInfo(parallelForEach.Body.Handler, activity, "Body"));
            }

            return children.ToArray();
        }

        private static ActivityInfo[] GetActivityChildren(Activity activity)
        {
            var children = new List<ActivityInfo>();

            foreach (var child in WorkflowInspectionServices.GetActivities(activity).Where(a => a != null))
            {
                children.Add(new ActivityInfo(child, activity, string.Empty));
            }

            return children.ToArray();
        }

        private bool IsCompatibleType(Type source, Type target)
        {
            return target.IsAssignableFrom(source) || source.IsConstructedGenericTypeOf(target);
        }

        private Type FindCompatibleSuppportedType(Type source)
        {
            foreach (Type supportedType in activityTypeToGetChildrenFunctionMap.Keys)
            {
                if (IsCompatibleType(source, supportedType))
                {
                    return supportedType;
                }
            }

            return null;
        }
    }
}