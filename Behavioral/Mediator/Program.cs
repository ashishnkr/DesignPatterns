using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using CodingExercise;
using NUnit.Framework;

namespace CodingExercise
{
    public class Participant
    {
        private readonly Mediator _mediator;
        public int Value { get; set; }

        public Participant(Mediator mediator)
        {
            _mediator = mediator;
            _mediator.participants.Add(this);
        }

        public void Say(int n)
        {
            _mediator.Broadcast(this, n);
        }
    }

    public class Mediator
    {
        public List<Participant> participants = new List<Participant>();

        public void Broadcast(Participant caller, int n)
        {
            foreach (Participant p in participants)
            {
                if (caller == p)
                {

                }
                else
                {
                    p.Value += n;
                }
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            
        }
    }
}

namespace Coding.Exercise.Tests
{
    [TestFixture]
    public class TestSuite
    {
        [Test]
        public void Test()
        {
            Mediator mediator = new Mediator();
            var p1 = new Participant(mediator);
            var p2 = new Participant(mediator);

            Assert.That(p1.Value, Is.EqualTo(0));
            Assert.That(p2.Value, Is.EqualTo(0));

            p1.Say(2);

            Assert.That(p1.Value, Is.EqualTo(0));
            Assert.That(p2.Value, Is.EqualTo(2));

            p2.Say(4);

            Assert.That(p1.Value, Is.EqualTo(4));
            Assert.That(p2.Value, Is.EqualTo(2));
        }
    }
}
