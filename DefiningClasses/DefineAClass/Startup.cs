using System;
using System.Reflection;

namespace DefineAClass
{
    public class Startup
    {
        public static void Main()
        {
            Type personType = typeof(Person);
            PropertyInfo[] properties = personType.GetProperties
                (BindingFlags.Public | BindingFlags.Instance);
            Console.WriteLine(properties.Length);
        }
    }
}