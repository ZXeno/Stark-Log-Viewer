namespace Stark
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.ComponentModel.DataAnnotations;

    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum GenericEnum)
        {
            Type genericEnumType = GenericEnum.GetType();
            MemberInfo[] memberInfo = genericEnumType.GetMember(GenericEnum.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                var _Attribs = memberInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);
                if (_Attribs != null && _Attribs.Count() > 0)
                {
                    return (_Attribs.ElementAt(0) as DisplayAttribute).GetName();
                }
            }

            return GenericEnum.ToString();
        }
    }
}
