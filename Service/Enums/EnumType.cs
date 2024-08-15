using System.ComponentModel;

namespace Service.Enums
{
    public class EnumType
    {
        public enum Role
        {
            ROLE_ADMIN, ROLE_LIBRARIAN, ROLE_MEMBER
        }

        public class Enum<T> where T : Enum
        {
            public static IEnumerable<T> GetAllValuesAsIEnumerable()
            {
                return Enum.GetValues(typeof(T)).Cast<T>();
            }
        }
    }
}
