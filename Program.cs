using System;
using System.Collections;
using System.Collections.Generic;

namespace SD725
{
    class MainApp
    {
        static void Main()

        {
            // Create receiver, command, and invoker

            Receiver commandDispatcher = new Receiver();
            Command command = new SendEmail(commandDispatcher);
            Invoker invoker = new Invoker();

            // Set and execute single command
            Console.WriteLine("------ Executing Single Command ----------");
            invoker.SetCommand(command);
            invoker.ExecuteCommand();

            Console.ReadKey();
            Console.WriteLine("------ Executing Multiple Commands ----------");

            invoker.macro.Add(commandDispatcher.SendEmail);
            invoker.macro.Add(commandDispatcher.PrintName);

            invoker.ExecuteMultipleCommands();
            Console.ReadKey();

        }
    }

    abstract class Command
    {
        protected Receiver receiver;

        public Command(Receiver receiver)
        {
            this.receiver = receiver;
        }

        public abstract void Execute();
    }

    class SendEmail : Command
    {
        public SendEmail(Receiver receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            receiver.SendEmail();
        }
    }

    class Receiver
    {
        public void SendEmail()
        {
            Console.WriteLine("Email Sent");
        }

        public void PrintName()
        {
            Console.WriteLine("Print Name Action");
        }
    }

    class Invoker
    {
        private Command _command;
        public List<Action> macro = new List<Action>();

        public void SetCommand(Command command)
        {
            this._command = command;
        }

        public void ExecuteCommand()
        {
            _command.Execute();
        }

        public void ExecuteMultipleCommands()
        {
            foreach (Action cmd in macro)
            {
                cmd.Invoke();

            }
        }
    }
}