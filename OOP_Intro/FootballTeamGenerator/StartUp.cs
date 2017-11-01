namespace FootballTeamGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StartUp
    {
        public static void Main()
        {
            string input = string.Empty;
            Dictionary<string, Team> teams = new Dictionary<string, Team>();

            while ((input = Console.ReadLine()) != "END")
            {
                try
                {
                    string[] inputCmds = input.Split(new char[] { ';' }, StringSplitOptions.None);

                    switch (inputCmds[0])
                    {
                        case ("Team"):
                            string teamName;
                            try
                            {
                                teamName = inputCmds[1];
                            }
                            catch
                            {
                                throw new Exception("A name should not be empty.");
                            }
                            Team newTeam = new Team(inputCmds[1]);
                            teams.Add(inputCmds[1], newTeam);
                            break;

                        case ("Add"):

                            if (!teams.ContainsKey(inputCmds[1]))
                            {
                                throw new InvalidOperationException($"Team {inputCmds[1]} does not exist.");
                            }

                            Player newPlayer = new Player(inputCmds[2], inputCmds[3], inputCmds[4], inputCmds[5], inputCmds[6], inputCmds[7]);
                            teams[inputCmds[1]].AddPlayer(newPlayer);
                            break;

                        case ("Remove"):
                            if (!teams.ContainsKey(inputCmds[1]))
                            {
                                throw new InvalidOperationException($"Team {inputCmds[1]} does not exist.");
                            }

                            teams[inputCmds[1]].RemovePlayer(inputCmds[2]);
                            break;

                        case ("Rating"):
                            if (!teams.ContainsKey(inputCmds[1]))
                            {
                                throw new InvalidOperationException($"Team {inputCmds[1]} does not exist.");
                            }

                            Console.WriteLine($"{inputCmds[1]} - {teams[inputCmds[1]].Rating}");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}