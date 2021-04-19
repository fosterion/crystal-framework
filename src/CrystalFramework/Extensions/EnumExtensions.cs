using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalFramework.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Get value of attribute description
        /// </summary>
        /// <param name="enumValue">Enum value</param>
        /// <returns></returns>
        public static string GetDescription(this Enum enumValue)
        {
            var attribute = enumValue.GetAttributeValue<DescriptionAttribute>();

            return attribute == null ? enumValue.ToString() : attribute.Description;
        }

        /// <summary>
        /// Get attribute value
        /// </summary>
        /// <typeparam name="T">Attribute datatype</typeparam>
        /// <param name="enumValue">Enum value</param>
        /// <returns>Атрибут | null</returns>
        private static T GetAttributeValue<T>(this Enum enumValue) where T : Attribute
        {
            var type = enumValue.GetType();
            var memberInfo = type.GetMember(enumValue.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);

            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }
    }
}
