using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace LabApp
{
    /// <summary>
    /// Расширения для перечислений
    /// </summary>
    internal static class EnumExtentions
    {

        /// <summary>
        /// Возвращает строку описания поля перечисления
        /// </summary>
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

    /// <summary>
    /// Обертка для элемента перечисления TypesOfProduct. Содержит орписание (Description) элемента.
    /// </summary>
    internal class TypeDisplay
    {
        internal TypeDisplay(TypesOfProduct value, string text)
        {
            Value = value;
            Text = text;
        }

        public TypesOfProduct Value { get; private set; }

        public string Text { get; private set; }

        public override string? ToString() => Text;
    }


    /// <summary>
    /// Обертка для элемента перечисления ThreadPriority. Содержит орписание (Description) элемента.
    /// </summary>
    internal class PriorityItem
    {
        public ThreadPriority Value { get; set; }
        public string? Text { get; set; }

        public override string? ToString() => Text;
    }
}
