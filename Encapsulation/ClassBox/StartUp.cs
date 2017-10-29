namespace ClassBox
{
    using System;
    using System.Linq;
    using System.Reflection;

    public class StartUp
    {
        public static void Main()
        {
            decimal inputLength = decimal.Parse(Console.ReadLine());
            decimal inputWidth = decimal.Parse(Console.ReadLine());
            decimal inputHeight = decimal.Parse(Console.ReadLine());

            Box box = new Box();


            Type boxType = typeof(Box);
            FieldInfo[] fields = boxType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            Console.WriteLine(fields.Count());

            try
            {
                box = new Box(inputLength, inputWidth, inputHeight);

                Console.WriteLine($"Surface Area - {box.CalculateSurfaceArea():f2}");
                Console.WriteLine($"Lateral Surface Area - {box.CalculateLateralSurfaceArea():f2}");
                Console.WriteLine($"Volume - {box.CalculateVolume():f2}");
            }
            catch (Exception e)
            {
              //  Console.WriteLine("ERROR!");
                Console.WriteLine(e.Message);
            }
        }
    }
}