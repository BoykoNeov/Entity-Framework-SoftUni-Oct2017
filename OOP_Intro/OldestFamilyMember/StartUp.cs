namespace OldestFamilyMember
{
    using System;
    using System.Reflection;

    public class StartUp
    {
        public static void Main()
        {
            MethodInfo oldestMemberMethod = typeof(Family).GetMethod("GetOldestMember");
            MethodInfo addMemberMethod = typeof(Family).GetMethod("AddMember");
            if (oldestMemberMethod == null || addMemberMethod == null)
            {
                throw new Exception();
            }

            Family currentFamily = new Family();

            int personsToAdd = int.Parse(Console.ReadLine());
            for (int i = 0; i < personsToAdd; i++)
            {
                string[] inputs = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                Person newFamilyMember = new Person
                {
                    Name = inputs[0],
                    Age = int.Parse(inputs[1])
                };

                currentFamily.AddMember(newFamilyMember);
            }

            Person oldestMember = currentFamily.GetOldestMember();
            Console.WriteLine(oldestMember);
        }
    }
}