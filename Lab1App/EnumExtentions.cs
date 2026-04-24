using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace LabApp
{
    internal static class EnumExtentions
    {
        internal static string GetDescription<T>(this T enumType)
            where T : Enum
        {
            Type type = enumType.GetType();
            string name = enumType.ToString();

            FieldInfo? field = type.GetField(name);

            DescriptionAttribute? attribute;
            if (field != null)
            {
                attribute = field.GetCustomAttribute<DescriptionAttribute>();
            }
            else
                attribute = null;


            if (attribute != null)
                return attribute.Description;
            else
                return name;

        }
    }


    internal class TypeDisplay
    {
        TypesOfProduct type;
        internal TypeDisplay(TypesOfProduct type)
        {
            this.type = type;
        }

        public TypesOfProduct Value
        {
            get { return type; }
        }

        public string Text
        {
            get
            {
                return type.GetDescription();
            }
        }
    }
}
