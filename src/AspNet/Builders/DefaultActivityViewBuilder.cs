using System;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.ServiceModel.Activities;
using Inthros.AspNet.Views;
using Inthros.Core.Utilities;

namespace Inthros.AspNet.Builders
{
    public sealed class DefaultActivityViewBuilder : ActivityViewBuilder
    {
        private static readonly IDictionary<Type, Func<object, ActivityView>> activityTypeToBuildFunctionMap;

        private static readonly ReadOnlyCollection<Type> supportedActivityTypes;

        static DefaultActivityViewBuilder()
        {
            // TODO : P2 : Consider sorting the supported types collections instead of the dictionary

            // The comparer is used so that the keys are sorted and the compatible type
            // is found in the correct order but it's probably better to sort the
            // supported types collections instead of the dictionary
            var typeComparer = new InheritanceTypeComparer();

            activityTypeToBuildFunctionMap = new SortedDictionary<Type, Func<object, ActivityView>>(typeComparer)
            {
                { typeof (Sequence), a => BuildSequenceView((Sequence) a) },
                { typeof (Persist), a => BuildHeaderOnlyView((Activity) a) },
                { typeof (If), a => BuildIfView((If) a) },
                { typeof (Parallel), a => BuildParallelView((Parallel) a) },
                { typeof (DoWhile), a => BuildDoWhileView((DoWhile) a) },
                { typeof (While), a => BuildWhileView((While) a) },
                { typeof (Pick), a => BuildPickView((Pick) a) },
                { typeof (PickBranch), a => BuildPickBranchView((PickBranch) a) },
                { typeof (Assign), a => BuildAssignView((Assign) a) },
                { typeof (WriteLine), a => BuildWriteLineView((WriteLine) a) },
                { typeof (Delay), a => BuildDelayView((Delay) a) },
                { typeof (ForEach<>), a => BuildForEachView((dynamic) a) },
                { typeof (ParallelForEach<>), a => BuildParallelForEachView((dynamic) a) },
                { typeof (Assign<>), a => BuildAssignView((dynamic) a) },
                { typeof (AddToCollection<>), a => BuildHeaderOnlyView((Activity) a) },
                { typeof (ClearCollection<>), a => BuildHeaderOnlyView((Activity) a) },
                { typeof (ExistsInCollection<>), a => BuildHeaderOnlyView((Activity) a) },
                { typeof (RemoveFromCollection<>), a => BuildHeaderOnlyView((Activity) a) },
                { typeof (Compensate), a => BuildHeaderOnlyView((Activity) a) },
                { typeof (Confirm), a => BuildHeaderOnlyView((Activity) a) },
                { typeof (Rethrow), a => BuildHeaderOnlyView((Activity) a) },
                { typeof (Throw), a => BuildHeaderOnlyView((Activity) a) },
                { typeof (TerminateWorkflow), a => BuildHeaderOnlyView((Activity) a) },
                { typeof (CorrelationScope), a => BuildCorrelationScopeView((CorrelationScope) a) },
                { typeof (CancellationScope), a => BuildCancellationScopeView((CancellationScope) a) },
                { typeof (CompensableActivity), a => BuildCompensableActivityView((CompensableActivity) a) },
                { typeof (TransactionScope), a => BuildTransactionScopeView((TransactionScope) a) },
                { typeof (NoPersistScope), a => BuildNoPersistScopeView((NoPersistScope) a) },
                //{ typeof (Activity), a => BuildHeaderOnlyView((Activity) a) },
            };

            // TODO : P1 : TryCatch
            // TODO : P1 : InvokeDelegate
            // TODO : P1 : InvokeMethod
            // TODO : P1 : InitializeCorrelation
            // TODO : P1 : Receive
            // TODO : P1 : ReceiveReply
            // TODO : P1 : Send
            // TODO : P1 : SendReply
            // TODO : P1 : TransactedReceiveScope
            // TODO : P1 : Switch<T>
            // TODO : P1 : Interop
            // TODO : P1 : Flow Chart Activities
            // TODO : P1 : State Machine Activities

            supportedActivityTypes = new ReadOnlyCollection<Type>(activityTypeToBuildFunctionMap.Keys.ToList());
        }

        public override IReadOnlyCollection<Type> SupportedTypes
        {
            get { return supportedActivityTypes; }
        }

        public override ActivityView Build(object activity)
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

            return activityTypeToBuildFunctionMap[keyType](activity);
        }

        private static ActivityView BuildNoPersistScopeView(NoPersistScope source)
        {
            string activityId = ObjectIdManager.GetId(source);

            var view = new NoPersistScopeView(activityId)
            {
                ActivityName = source.DisplayName,
            };

            return view;
        }

        private static ActivityView BuildTransactionScopeView(TransactionScope source)
        {
            string activityId = ObjectIdManager.GetId(source);

            var view = new TransactionScopeView(activityId)
            {
                ActivityName = source.DisplayName,
            };

            return view;
        }

        private static ActivityView BuildCompensableActivityView(CompensableActivity source)
        {
            string activityId = ObjectIdManager.GetId(source);

            var view = new CompensableActivityView(activityId)
            {
                ActivityName = source.DisplayName,
            };

            return view;
        }

        private static ActivityView BuildCancellationScopeView(CancellationScope source)
        {
            string activityId = ObjectIdManager.GetId(source);

            var view = new CancellationScopeView(activityId)
            {
                ActivityName = source.DisplayName,
            };

            return view;
        }

        private static ActivityView BuildCorrelationScopeView(CorrelationScope source)
        {
            string activityId = ObjectIdManager.GetId(source);

            string correlatesWidth = ExpressionConvert.ToString(source.CorrelatesWith);

            var view = new CorrelationScopeView(activityId)
            {
                ActivityName = source.DisplayName,
                CorrelatesWith = correlatesWidth,
            };

            return view;
        }

        private static ActivityView BuildHeaderOnlyView(Activity source)
        {
            string activityId = ObjectIdManager.GetId(source);

            return new HeaderOnlyActivityView(activityId) { ActivityName = source.DisplayName };
        }

        private static ActivityView BuildSequenceView(Sequence source)
        {
            string activityId = ObjectIdManager.GetId(source);

            return new SequenceView(activityId) { ActivityName = source.DisplayName };
        }

        private static ActivityView BuildIfView(If source)
        {
            string activityId = ObjectIdManager.GetId(source);

            string condition = ExpressionConvert.ToString(source.Condition);

            var view = new IfView(activityId)
            {
                ActivityName = source.DisplayName,
                Condition = condition,
            };

            return view;
        }

        private static ActivityView BuildParallelView(Parallel source)
        {
            string activityId = ObjectIdManager.GetId(source);

            return new ParallelView(activityId) { ActivityName = source.DisplayName };
        }

        private static ActivityView BuildDoWhileView(DoWhile source)
        {
            string activityId = ObjectIdManager.GetId(source);

            string condition = ExpressionConvert.ToString(source.Condition);

            var view = new DoWhileView(activityId)
            {
                ActivityName = source.DisplayName,
                Condition = condition,
            };

            return view;
        }

        private static ActivityView BuildWhileView(While source)
        {
            string activityId = ObjectIdManager.GetId(source);

            string condition = ExpressionConvert.ToString(source.Condition);

            var view = new WhileView(activityId)
            {
                ActivityName = source.DisplayName,
                Condition = condition,
            };

            return view;
        }

        private static ActivityView BuildPickView(Pick source)
        {
            string activityId = ObjectIdManager.GetId(source);

            var view = new PickView(activityId)
            {
                ActivityName = source.DisplayName,
            };

            return view;
        }

        private static ActivityView BuildPickBranchView(PickBranch source)
        {
            string activityId = ObjectIdManager.GetId(source);

            var view = new PickBranchView(activityId)
            {
                ActivityName = source.DisplayName,
            };

            return view;
        }

        private static ActivityView BuildAssignView(Assign source)
        {
            string activityId = ObjectIdManager.GetId(source);

            string to = ExpressionConvert.ToString(source.To);
            string value = ExpressionConvert.ToString(source.Value);

            var view = new AssignView(activityId)
            {
                ActivityName = source.DisplayName,
                To = to,
                Value = value,
            };

            return view;
        }

        private static ActivityView BuildWriteLineView(WriteLine source)
        {
            string activityId = ObjectIdManager.GetId(source);

            string text = ExpressionConvert.ToString(source.Text);

            var view = new WriteLineView(activityId)
            {
                ActivityName = source.DisplayName,
                Text = text,
            };

            return view;
        }

        private static ActivityView BuildDelayView(Delay source)
        {
            string activityId = ObjectIdManager.GetId(source);

            string duration = ExpressionConvert.ToString(source.Duration);

            var view = new DelayView(activityId)
            {
                ActivityName = source.DisplayName,
                Duration = duration,
            };

            return view;
        }

        private static ActivityView BuildForEachView<T>(ForEach<T> source)
        {
            string activityId = ObjectIdManager.GetId(source);

            string values = ExpressionConvert.ToString(source.Values);
            string argument = string.Empty;

            if (source.Body != null)
            {
                argument = source.Body.Argument.Name;
            }

            var view = new ForEachView(activityId)
            {
                ActivityName = source.DisplayName,
                Values = values,
                Argument = argument,
            };

            return view;
        }

        private static ActivityView BuildParallelForEachView<T>(ParallelForEach<T> source)
        {
            string activityId = ObjectIdManager.GetId(source);

            string values = ExpressionConvert.ToString(source.Values);
            string argument = string.Empty;

            if (source.Body != null)
            {
                argument = source.Body.Argument.Name;
            }

            var view = new ForEachView(activityId)
            {
                ActivityName = source.DisplayName,
                Values = values,
                Argument = argument,
            };

            return view;
        }

        private static ActivityView BuildAssignView<T>(Assign<T> source)
        {
            string activityId = ObjectIdManager.GetId(source);

            string to = ExpressionConvert.ToString(source.To);
            string value = ExpressionConvert.ToString(source.Value);

            var view = new AssignView(activityId)
            {
                ActivityName = source.DisplayName,
                To = to,
                Value = value,
            };

            return view;
        }

        private Type FindCompatibleSuppportedType(Type source)
        {
            foreach (Type supportedType in activityTypeToBuildFunctionMap.Keys)
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