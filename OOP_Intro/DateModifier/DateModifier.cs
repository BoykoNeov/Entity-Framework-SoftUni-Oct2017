namespace DateModifier
{
    using System;
    using System.Linq;

    public class DateModifier
    {
        public static void Main()
        {
            int[] firstDateParams = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            int[] secondDateParams = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            DateTime firstDate = new DateTime(firstDateParams[0], firstDateParams[1], firstDateParams[2]);
            DateTime secondDate = new DateTime(secondDateParams[0], secondDateParams[1], secondDateParams[2]);

            TimeSpan difference = firstDate.Subtract(secondDate);
            Console.WriteLine(Math.Abs(difference.Days));
        }
    }
}