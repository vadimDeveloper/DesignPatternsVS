﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorRealWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create chatroom
            var chatroom = new Chatroom();

            // Create participants and register them
            var George = new Beatle("George");
            var Paul = new Beatle("Paul");
            var Ringo = new Beatle("Ringo");
            var John = new Beatle("John");
            var Yoko = new NonBeatle("Yoko");

            chatroom.Register(George);
            chatroom.Register(Paul);
            chatroom.Register(Ringo);
            chatroom.Register(John);
            chatroom.Register(Yoko);

            // Chatting participants
            Yoko.Send("John", "Hi John!");
            Paul.Send("Ringo", "All you need is love");
            Ringo.Send("George", "My sweet Lord");
            Paul.Send("John", "Can't buy me love");
            John.Send("Yoko", "My sweet love");

            // Wait for user
            Console.ReadKey();
        }
    }
    /// <summary>
    /// The 'Mediator' abstract class
    /// </summary>
    abstract class AbstractChatroom
    {
        public abstract void Register(Participant participant);
        public abstract void Send(
          string from, string to, string message);
    }

    /// <summary>
    /// The 'ConcreteMediator' class
    /// </summary>
    class Chatroom : AbstractChatroom
    {
        private Dictionary<string, Participant> _participants =
          new Dictionary<string, Participant>();

        public override void Register(Participant participant)
        {
            if (!_participants.ContainsValue(participant))
            {
                _participants[participant.Name] = participant;
            }

            participant.Chatroom = this;
        }

        public override void Send(
          string from, string to, string message)
        {
            var participant = _participants[to];

            participant?.Receive(@from, message);
        }
    }

    /// <summary>
    /// The 'AbstractColleague' class
    /// </summary>
    class Participant
    {
        private Chatroom _chatroom;
        private string _name;

        // Constructor
        public Participant(string name)
        {
            this._name = name;
        }

        // Gets participant name
        public string Name
        {
            get { return _name; }
        }

        // Gets chatroom
        public Chatroom Chatroom
        {
            set { _chatroom = value; }
            get { return _chatroom; }
        }

        // Sends message to given participant
        public void Send(string to, string message)
        {
            _chatroom.Send(_name, to, message);
        }

        // Receives message from given participant
        public virtual void Receive(
          string from, string message)
        {
            Console.WriteLine("{0} to {1}: '{2}'",
              from, Name, message);
        }
    }

    /// <summary>
    /// A 'ConcreteColleague' class
    /// </summary>
    class Beatle : Participant
    {
        // Constructor
        public Beatle(string name)
          : base(name)
        {
        }

        public override void Receive(string from, string message)
        {
            Console.Write("To a Beatle: ");
            base.Receive(from, message);
        }
    }

    /// <summary>
    /// A 'ConcreteColleague' class
    /// </summary>
    class NonBeatle : Participant
    {
        // Constructor
        public NonBeatle(string name)
          : base(name)
        {
        }

        public override void Receive(string from, string message)
        {
            Console.Write("To a non-Beatle: ");
            base.Receive(from, message);
        }
    }
}