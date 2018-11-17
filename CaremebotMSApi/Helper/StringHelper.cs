using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaremebotMSApi.Helper
{
    public static class StringHelper
    {
        public static List<T> getEnums <T>(this List<string> stringList)
        { 
        return stringList.Select(x => Enum.Parse(typeof(T), x))
                             .Cast<T>()
                             .ToList();
        }
        public static TEnum? ParseEnum<TEnum>(this string sEnumValue) where TEnum : struct
        {
            TEnum eTemp;
            TEnum? eReturn = null;
            if (Enum.TryParse<TEnum>(sEnumValue, out eTemp) == true)
                eReturn = eTemp;
            return eReturn;
        }
    }

}