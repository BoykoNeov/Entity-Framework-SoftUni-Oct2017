// Mnogo mazah... :(
// 87/100 for now
namespace FootballTeamGenerator
{
    using System;

    public class Player
    {
        private string name;
        private int endurance;
        private int sprint;
        private int dribble;
        private int passing;
        private int shooting;

        public Player() { }

        public Player(string playerName, string playerEndurance, string playerSprint, string playerDribble, string playerPassing, string playerShooting)
        {
            this.Name = playerName;
            this.Endurance = int.Parse(playerEndurance);
            this.Sprint = int.Parse(playerSprint);
            this.Dribble = int.Parse(playerDribble);
            this.Passing = int.Parse(playerPassing);
            this.Shooting = int.Parse(playerShooting);
        }

        public int Shooting
        {
            get
            {
                return shooting;
            }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException("Shooting should be between 0 and 100.");
                }
                shooting = value;
            }
        }

        public int Passing
        {
            get
            {
                return passing;
            }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException("Passing should be between 0 and 100.");
                }
                passing = value;
            }
        }

        public int Dribble
        {
            get
            {
                return dribble;
            }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException("Dribble should be between 0 and 100.");
                }
                dribble = value;
            }
        }

        public int Sprint
        {
            get
            {
                return sprint;
            }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException("Sprint should be between 0 and 100.");
                }
                sprint = value;
            }
        }

        public int Endurance
        {
            get
            {
                return endurance;
            }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException("Endurance should be between 0 and 100.");
                }
                endurance = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value == null || value == string.Empty || value.TrimStart().TrimEnd().Length == 0)
                {
                    throw new ArgumentException("A name should not be empty.");
                }
                name = value;
            }
        }

        public override bool Equals(object obj)
        {
            var item = obj as Player;

            if (item == null)
            {
                return false;
            }

            return this.Name.Equals(item.Name);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}