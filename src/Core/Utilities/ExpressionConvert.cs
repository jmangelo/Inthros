using System.Activities;
using System.Activities.Expressions;
using System.Activities.XamlIntegration;

namespace Inthros.Core.Utilities
{
    public static class ExpressionConvert
    {
        public static string ToString<T>(Activity<T> activity)
        {
            var literal = activity as Literal<T>;

            if (literal != null && CSharpConvert.IsCodePrimitive(literal.Value))
            {
                // TODO : P2 : Handle literals according to the expression language

                return CSharpConvert.ToString(literal.Value);
            }

            var serializableExpression = activity as IValueSerializableExpression;

            if (serializableExpression != null && serializableExpression.CanConvertToString(null))
            {
                return serializableExpression.ConvertToString(null);
            }

            var textExpression = activity as ITextExpression;

            if (textExpression != null)
            {
                return textExpression.ExpressionText;
            }

            return null;
        }

        public static string ToString(ActivityWithResult activity)
        {
            if (activity == null)
            {
                return null;
            }

            if (activity.GetType().IsConstructedGenericTypeOf(typeof(Literal<>)) && CSharpConvert.IsCodePrimitive(activity.ResultType))
            {
                return CSharpConvert.ToString(((dynamic) activity).Value);
            }

            var serializableExpression = activity as IValueSerializableExpression;

            if (serializableExpression != null && serializableExpression.CanConvertToString(null))
            {
                return serializableExpression.ConvertToString(null);
            }

            var textExpression = activity as ITextExpression;

            if (textExpression != null)
            {
                return textExpression.ExpressionText;
            }

            return null;
        }

        public static string ToString<T>(OutArgument<T> argument)
        {
            if (argument != null)
            {
                return ToString(argument.Expression);
            }

            return null;
        }

        public static string ToString(Argument argument)
        {
            if (argument != null)
            {
                return ToString(argument.Expression);
            }

            return null;
        }

        public static string ToString<T>(InArgument<T> argument)
        {
            if (argument != null)
            {
                return ToString(argument.Expression);
            }

            return null;
        }
    }
}