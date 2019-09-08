using System;
using NUnit.Framework;

namespace CodingExercise
{
    public class Game
    {
        public event EventHandler RatEnters, RatDies;

        public event EventHandler<Rat> NotifyRat;

        public void OnRatEnters(object sender)
        {
            RatEnters?.Invoke(sender, EventArgs.Empty);
        }

        public void OnRatDies(object sender)
        {
            RatDies?.Invoke(sender, EventArgs.Empty);
        }

        public void OnNotifyRat(object sender, Rat whichRat)
        {
            NotifyRat?.Invoke(sender, whichRat);
        }
        // remember - no fields or properties!
    }

    public class Rat : IDisposable
    {
        private readonly Game game;
        public int Attack = 1;

        public Rat(Game game)
        {
            this.game = game;
            game.RatEnters += (sender, args) =>
            {
                if (sender != this)
                {
                    ++Attack;
                    game.OnNotifyRat(this, (Rat) sender);
                }
            };
            game.NotifyRat += (sender, rat) =>
            {
                if (rat == this)
                {
                    ++Attack;
                }
            };
            game.RatDies += (sender, args) => --Attack;
            game.OnRatEnters(this);
        }

        public void Dispose()
        {
            game.OnRatDies(this);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            
        }
    }

    namespace CodingExercise.Tests
    {
        [TestFixture]
        public class Tests
        {
            [Test]
            public void PlayingByTheRules()
            {
                Assert.That(typeof(Game).GetFields(), Is.Empty);
                Assert.That(typeof(Game).GetProperties(), Is.Empty);
            }

            [Test]
            public void SingleRatTest()
            {
                var game = new Game();
                var rat = new Rat(game);
                Assert.That(rat.Attack, Is.EqualTo(1));
            }

            [Test]
            public void TwoRatTest()
            {
                var game = new Game();
                var rat = new Rat(game);
                var rat2 = new Rat(game);
                Assert.That(rat.Attack, Is.EqualTo(2));
                Assert.That(rat2.Attack, Is.EqualTo(2));
            }

            [Test]
            public void ThreeRatsOneDies()
            {
                var game = new Game();

                var rat = new Rat(game);
                Assert.That(rat.Attack, Is.EqualTo(1));

                var rat2 = new Rat(game);
                Assert.That(rat.Attack, Is.EqualTo(2));
                Assert.That(rat2.Attack, Is.EqualTo(2));

                using (var rat3 = new Rat(game))
                {
                    Assert.That(rat.Attack, Is.EqualTo(3));
                    Assert.That(rat2.Attack, Is.EqualTo(3));
                    Assert.That(rat3.Attack, Is.EqualTo(3));
                }

                Assert.That(rat.Attack, Is.EqualTo(2));
                Assert.That(rat2.Attack, Is.EqualTo(2));
            }
        }
    }
}

