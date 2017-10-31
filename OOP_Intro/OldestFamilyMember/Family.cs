namespace OldestFamilyMember
{
    using System.Collections.Generic;
    using System.Linq;

    internal class Family
    {
        private List<Person> familyMembers;

       public Family()
        {
            this.FamilyMembers = new List<Person>();
        }

        private List<Person> FamilyMembers
        {
            get { return familyMembers; }
            set { familyMembers = value; }
        }

       public void AddMember(Person member)
        {
            this.FamilyMembers.Add(member);
        }

        public Person GetOldestMember()
        {
            Person oldestPerson = this.FamilyMembers.OrderByDescending(x => x.Age).First();
            return oldestPerson;
        }
    }
}