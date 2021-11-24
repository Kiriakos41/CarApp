using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CarApp.Helpers
{
    public static class Extensions
    {
        public static List<BindingEnum> BindEnumToSelectListItem<T>(this BindingEnum select) where T : Enum
        {
            var list = Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(value => new BindingEnum()
                {
                    Text = (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute)?.Description ?? value.ToString(),
                    Value = value
                })
                .OrderBy(item => item.Text.ToString())
                .ToList();
            return list;
        }

        public class BindingEnum
        {
            public Enum Value { get; set; }
            public string Text { get; set; }
        }

    }

    
}
