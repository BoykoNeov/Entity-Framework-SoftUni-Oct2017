namespace Employees.App
{
    using System;
    using System.Reflection;
    using System.Linq;
    using Employees.App.Commands;
    using System.Collections.Generic;

    internal class CommandParser
    {
        public static ICommand ParseCommand(IServiceProvider serviceProvider, string commandName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            // Gets only the classes from the assembly that implement the interface 'ICommand'
            IEnumerable<Type> commandTypes = assembly
                .GetTypes()
                .Where(t => t.GetInterfaces()
                .Contains(typeof(ICommand)));

            // Gets only the command that has a name similar to input
            Type commandType = commandTypes
                .SingleOrDefault(c => c.Name.ToLower() == $"{commandName}Command".ToLower());

            if (commandType == null)
            {
                throw new InvalidOperationException("Invalid command!");
            }

            // Gets the first constructor (what if there are more?)
            ConstructorInfo constructor = commandType
                .GetConstructors()
                .FirstOrDefault();

            // Gets the parameters of the constructor
            IEnumerable<Type> construcotrParams = constructor
                .GetParameters()
                .Select(p => p.ParameterType);

            // Get constructor arguments
            object[] constructorArgs = construcotrParams
            //  .Select(p => serviceProvider.GetService(p))
                .Select(serviceProvider.GetService)
                .ToArray();

            //Ivoke requres object
            ICommand command = (ICommand)constructor.Invoke(constructorArgs);

            return command;
        }
    }
}