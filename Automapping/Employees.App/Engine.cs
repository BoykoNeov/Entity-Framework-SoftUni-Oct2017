using System;
using System.Linq;
using Employees.App.Commands;

namespace Employees.App
{
    internal class Engine
    {
        private readonly IServiceProvider serviceProvider;

        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        internal void Run()
        {
            while (true)
            {
                string input = Console.ReadLine();
                string[] commandTokens = input.Trim().Split(' ');

                string commandName = commandTokens[0];

                string[] commandArgs = commandTokens.Skip(1).ToArray();

                ICommand command = CommandParser.ParseCommand(serviceProvider, commandName);

                string result = command.Execute(commandArgs);

                Console.WriteLine(result);
            }
        }
    }
}