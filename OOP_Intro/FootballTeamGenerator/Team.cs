namespace FootballTeamGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Team
    {
        private string name;
        private int numberOfPlayers;
        private double rating;
        private Dictionary<Player, double> playersByOverallSkill;
        private HashSet<Player> teamPlayers;

        public Team()
        {
            this.playersByOverallSkill = new Dictionary<Player, double>();
            this.teamPlayers = new HashSet<Player>();
        }

        public Team(string teamName) : this()
        {
            this.Name = teamName;
        }

        public double Rating
        {
            get
            {
                return Math.Round(rating);
            }
            private set
            {
                rating = value;
            }
        }

        private int NumberOfPlayers
        {
            get
            {
                return numberOfPlayers;
            }
            set
            {
                numberOfPlayers = value;
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

        public void AddPlayer(Player playerToAdd)
        {
            this.teamPlayers.Add(playerToAdd);
            this.NumberOfPlayers++;

            double sumOfPlayerStats = playerToAdd.Dribble + playerToAdd.Endurance + playerToAdd.Passing + playerToAdd.Shooting + playerToAdd.Sprint;

            this.playersByOverallSkill.Add(playerToAdd, (sumOfPlayerStats / 5));
            CalculateTeamRating();
        }

        public void RemovePlayer(string NameOfplayerToRemove)
        {
            Player playerToRemove = new Player
            {
                Name = NameOfplayerToRemove
            };

            if (!this.teamPlayers.Contains(playerToRemove))
            {
                string exceptionmessage = $"Player {playerToRemove.Name} is not in {this.Name} team.";
                throw new ArgumentException(exceptionmessage);
            }

            this.teamPlayers.Remove(playerToRemove);
            this.playersByOverallSkill.Remove(playerToRemove);
            this.numberOfPlayers--;
            CalculateTeamRating();
        }

        private void CalculateTeamRating()
        {
            this.Rating = this.playersByOverallSkill.Select(x => x.Value).Sum() / this.NumberOfPlayers;
        }
    }
}