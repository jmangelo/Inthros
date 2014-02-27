using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.CSharp;

namespace Inthros.Core.Utilities
{
    public static class CSharpConvert
    {
        private static readonly CodeGeneratorOptions DefaultCodeGeneratorOptions = new CodeGeneratorOptions();

        public static string ToString(object value)
        {
            var text = new StringBuilder();

            using (var provider = new CSharpCodeProvider())
            {
                using (var writer = new StringWriter(text, CultureInfo.InvariantCulture))
                {
                    provider.GenerateCodeFromExpression(new CodePrimitiveExpression(value), writer, DefaultCodeGeneratorOptions);
                }
            }

            return text.ToString();
        }

        public static bool IsCodePrimitive(object value)
        {
            if (value == null)
            {
                return true;
            }

            var type = value.GetType();

            return type.IsPrimitive || type == typeof (string);
        }

        public static bool IsCodePrimitive(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            return type.IsPrimitive || type == typeof(string);
        }
    }
}
