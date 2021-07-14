using System;
using System.ComponentModel;

namespace JM.Integration.Methanol.Common.Extensions
{
    /// <summary>
    /// Enum extension class for getting enum description
    /// </summary>
    public static class EnumExtensionMethods
    {
        /// <summary>
        /// Provides enum description for the enum 
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns>Returns enum description</returns>
        public static string GetEnumDescription(this Enum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
        }
    }
}
