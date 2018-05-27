using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace Mazes.Wpf.Helpers
{
    public class EnumDescriptionTypeConverter : EnumConverter
    {
        public EnumDescriptionTypeConverter(Type type)
            : base(type)
        { }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (value != null)
                {
                    FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
                    if (fieldInfo != null)
                    {
                        var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                        if ((attributes.Length > 0) && (!String.IsNullOrEmpty(attributes[0].Description)))
                        {
                            return attributes[0].Description;
                        }
                        else
                        {
                            return value.ToString();
                        }
                    }
                }

                return String.Empty;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
