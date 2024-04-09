using System;
using System.Numerics;
using HRTheGathering.Cards;
using HRTheGathering.Effects;
using HRTheGathering.Observers;
using HRTheGathering.Players;
using HRTheGathering.Publishers;

namespace HRTheGathering.Board
{
    class Board
    {
        private static Board instance = new Board();
        private Publisher publisher = Publisher.Instance;
        Stack<Card> spellStack = new Stack<Card>();
        private int currentRound;
        private Player? winner = null;
        private Player player1;
        private Player player2;
        private PlayerHealthObserver player1LifeObserver;
        private PlayerHealthObserver player2LifeObserver;
        private PlayerHandObserver player1HandObserver;
        private PlayerHandObserver player2HandObserver;
        private PlayerBoardObserver player1BoardObserver;
        private PlayerBoardObserver player2BoardObserver;
        public CardFactory cardFactory = new CardFactory();

        public Board()
        {
            currentRound = 0;
            player1 = new Player { Id = 1, Name = "Player 1" };
            player2 = new Player { Id = 2, Name = "Player 2" };

            player1LifeObserver = new PlayerHealthObserver();
            player2LifeObserver = new PlayerHealthObserver();

            player1HandObserver = new PlayerHandObserver();
            player2HandObserver = new PlayerHandObserver();

            player1BoardObserver = new PlayerBoardObserver();
            player2BoardObserver =  new PlayerBoardObserver();


            // Attach the observers to the players
            player1.HealthObservable.Attach(player1LifeObserver);
            player1.HandObservable.Attach(player1HandObserver);
            player1.BoardObservable.Attach(player1BoardObserver);

            player2.HealthObservable.Attach(player2LifeObserver);
            player2.HandObservable.Attach(player2HandObserver);
            player2.BoardObservable.Attach(player2BoardObserver);


        }

        public static Board Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Board();
                }
                return instance;
            }
        }

        public void StartGame()
        {
            RunTests();

            PrepareGame();
            // Add whatever is needed

            Console.WriteLine("Done preparing the game - press any key to start the game...");
            Console.ReadKey();
            while(winner == null)
            {
                StartRound();
            }
        }

        public void PrepareGame()
        {
            // Create decks for both players
            player1.Deck = cardFactory.CreateDeck(Card.Color.White, player1, publisher);
            player2.Deck = cardFactory.CreateDeck(Card.Color.Red, player2, publisher);

            // Shuffle decks of each player
            player1.ShuffleDeck();
            player2.ShuffleDeck();

            // Players draw cards up until the MaxCardsInHand (7
            int maxCardsInHand = 7;
            for (int i = 0; i < maxCardsInHand; i++)
            {
                player1.DrawCard();
                player2.DrawCard();
            }
        }

        public void RunTests()
        {
            // Creature Stats Change Test
            //CreatureCard creature1 = new CreatureCard { Name = "Creature 1", Attack = 3, Defense = 10 };
            //CreatureCard creature2 = new CreatureCard { Name = "Creature 2", Attack = 5, Defense = 6 };
            //CreatureCard creature3 = new CreatureCard { Name = "Creature 3", Attack = 4, Defense = 3 };
            //CreatureCard creature4 = new CreatureCard { Name = "Creature 4", Attack = 7, Defense = 8 };

            //Console.WriteLine($"Creature1: ({creature1.Attack}, {creature1.Defense}), Creature2: ({creature2.Attack}, {creature2.Defense}), Creature3: ({creature3.Attack}, {creature3.Defense}), Creature4: ({creature4.Attack}, {creature4.Defense})");

            //publisher.SubscribeChangeStats(creature1, player1);
            //publisher.SubscribeChangeStats(creature2, player1);
            //publisher.SubscribeChangeStats(creature3, player2);
            //publisher.SubscribeChangeStats(creature4, player2);

            //publisher.ChangeStatsCreatures(2, 2, player1);
            //Console.WriteLine($"Creature1: ({creature1.Attack}, {creature1.Defense}), Creature2: ({creature2.Attack}, {creature2.Defense}), Creature3: ({creature3.Attack}, {creature3.Defense}), Creature4: ({creature4.Attack}, {creature4.Defense})");

            //publisher.UnsubscribeChangeStats(creature2, player1);
            //publisher.ChangeStatsCreatures(2, 2, player1);
            //publisher.ChangeStatsCreatures(2, 2, player2);
            //Console.WriteLine($"Creature1: ({creature1.Attack}, {creature1.Defense}), Creature2: ({creature2.Attack}, {creature2.Defense}), Creature3: ({creature3.Attack}, {creature3.Defense}), Creature4: ({creature4.Attack}, {creature4.Defense})");  

            //publisher.UnsubscribeChangeStats(creature3, player2);
            //publisher.ChangeStatsCreatures(2, 2, player2);
            //Console.WriteLine($"Creature1: ({creature1.Attack}, {creature1.Defense}), Creature2: ({creature2.Attack}, {creature2.Defense}), Creature3: ({creature3.Attack}, {creature3.Defense}), Creature4: ({creature4.Attack}, {creature4.Defense})");
            //Console.WriteLine("-----");

            //ChangeStats changeStats = new ChangeStats(2, 2, player1, publisher);
            //SpellCard spellCard2 = new SpellCard { CardEffect = changeStats };
            //spellCard2.CardEffect.ApplyEffect();
            //Console.WriteLine($"Creature1: ({creature1.Attack}, {creature1.Defense}), Creature2: ({creature2.Attack}, {creature2.Defense}), Creature3: ({creature3.Attack}, {creature3.Defense}), Creature4: ({creature4.Attack}, {creature4.Defense})");

            //Console.WriteLine("-----");
            //ChangeStats changeStats2 = new ChangeStats(2, 2, player2, publisher);
            //SpellCard spellCard = new SpellCard { CardEffect = changeStats2 };
            //spellCard.CardEffect.ApplyEffect();
            //Console.WriteLine($"Creature1: ({creature1.Attack}, {creature1.Defense}), Creature2: ({creature2.Attack}, {creature2.Defense}), Creature3: ({creature3.Attack}, {creature3.Defense}), Creature4: ({creature4.Attack}, {creature4.Defense})");
            //Console.ReadKey();

            //// Change CardsInHand Test
            //player1.Hand.Add(creature1);
            //player1.Hand.Add(creature2);
            //player1.Hand.Add(creature3);
            //player1.Hand.Add(creature4);
            //Console.WriteLine($"Cards in hand: { player1.Hand.Count()}");

            //ChangeCardsInHand changeCardsInHand = new ChangeCardsInHand(-2, player1, publisher);
            //SpellCard spellcardCardsInHand = new SpellCard { CardEffect = changeCardsInHand};

            //publisher.SubscribeCardsInHand(player1);

            //player1.UseCard(spellcardCardsInHand, publisher);
            //Console.WriteLine($"Cards in hand: {player1.Hand.Count()}");
            //Console.ReadKey();

            // Health Observer Test
            Console.WriteLine(player1.Health);
            Console.WriteLine(player2.Health);
            Console.WriteLine("Press enter to attack player 1 with 5 damage...");
            Console.ReadKey();

            // Hit player 1 with 5 damage
            int damageAmount = 5; // Let's say the damage amount is 10
            player1.Health -= damageAmount;

            // Hand Observer Test
            Console.WriteLine("Press enter to draw a card for player 1...");
            Console.ReadKey();

            player1.Deck = cardFactory.CreateDeck(Card.Color.White, player1, publisher);
            player1.DrawCard();
            Console.WriteLine("Press enter to continue...");
            Console.ReadKey();

            Console.WriteLine("Press enter to discard card for player 1...");
            player1.DiscardCard(player1.Hand.First(), publisher);
            Console.ReadKey();

        }

        public void StartRound()
        {
            StartTurn(player1);

            // Check if the game has ended after player1's turn
            if (winner != null)
                return;

            StartTurn(player2);

            // Check if the game has ended after player2's turn
            if (winner != null)
                return;

            // End the round and go to the next round
            currentRound++;
        }

        public void StartTurn(Player player)
        {
            // Preparation:
            // Reset temporary effects and reset to original state (example: lands turn back to normal)
            // Reset all Land Cards on the board
            ClearAllTurnedLandCards(player);

            // Drawing:
            // Player draws card from deck and add its to their hand
            bool drawnCard = player.DrawCard();

            // if drawnCard is false, it means the Deck has no more cards left to draw
            if (!drawnCard)
            {
                EndGame(player);
                return;
            }

            // Main:
            // Player can play cards
            // Player can attack
            // Opposing player can assign a defender and/or play an instant spell/card
         
            // If the player has a Land Card, play it
            // Play a land card
            while (player.Hand.Any(card => card is LandCard))
            {
                LandCard? landCard = player.Hand.FirstOrDefault(card => card is LandCard) as LandCard;
                if (landCard != null)
                {
                    player.UseCard(landCard, publisher);
                }
            }

            // If the player has a CreatureCard, try to play it
            CreatureCard? creatureCard = player.Hand.FirstOrDefault(card => card is CreatureCard) as CreatureCard;
            if (creatureCard != null)
            {
                player.UseCardWithCost(creatureCard, publisher);
            }

            // If the player has a SpellCard, try to play it
            SpellCard? spellCard = player.Hand.FirstOrDefault(card => card is SpellCard) as SpellCard;
            if (spellCard != null)
            {
                player.UseCardWithCost(spellCard, publisher, spellStack);
            }

            // TODO: Add attack
            

            // Ending:
            // Player must discard cards from their hand until the cards in hand dont exceed MaxCardsInHand (7)
            while (player.Hand.Count > player.MaxCardsInHand)
            {
                Random random = new Random(); 
                // Get a random card from the hand to discard
                int randomIndex = random.Next(player.Hand.Count);
                Card randomCard = player.Hand[randomIndex];

                player.DiscardCard(randomCard, publisher);
            }

            EndTurn();
            Console.WriteLine($"This is the end of the turn of {player.Name} - press any key to continue...");
            Console.ReadKey();
        }

        public void EndTurn()
        {
            // Log what the status of the players at the end of the turn.
            // 
            // Example: 
            // End situation Player1: 4 cards in hand, 3 used lands on the floor, a permanent 
            // creature played in a state of attack, full life.
            // End situation Player2: 6 cards, 1 land on the floor, 5 life.

            PrintEndSituation(player1);
            PrintEndSituation(player2);
        }

        public void PrintEndSituation(Player player)
        {
            Console.WriteLine($"{player.Name}:");
            Console.WriteLine($"Cards in hand: {player.Hand.Count}");
            int numberOfLands = player.CardsOnBoard.Count(card => card is LandCard);
            Console.WriteLine($"Lands in field: {numberOfLands}");
            int numberOfCreatures = player.CardsOnBoard.Count(card => card is CreatureCard);
            Console.WriteLine($"Creatures in field: {numberOfCreatures}");
            Console.WriteLine($"Lives: {player.Health}\n");
        }

        public void EndGame(Player player)
        {
            // Determine the winning player
            if (player == player1)
            {
                winner = player2;
            }
            else
            {
                winner = player1;
            }

            Console.WriteLine("\n-----------------------------------------------------------");
            Console.WriteLine($"{winner.Name} has won the game!");
            Console.WriteLine("press any key to continue...");
            Console.WriteLine("-----------------------------------------------------------");
            Console.ReadKey();


            // End the game by closing the instance and declaring a winner

            // Game ends by:
            // if player HP <= 0
            // if player Deck.Count <= 0
            // if player forfeits the game

            // Detach all observers
            player1.HealthObservable.Detach(player1LifeObserver);
            player1.HandObservable.Detach(player1HandObserver);
            player1.BoardObservable.Detach(player1BoardObserver);

            player2.HealthObservable.Detach(player2LifeObserver);
            player2.HandObservable.Detach(player2HandObserver);
            player1.BoardObservable.Detach(player2BoardObserver);

        }

        public void ClearAllTurnedLandCards(Player player)
        {
            foreach (Card card in player.CardsOnBoard)
            {
                if (card is LandCard)
                {
                    LandCard landCard = (LandCard)card;
                    landCard.IsTurned = false;
                }
            }
        }

        public void Attack(Player target)
        {
            Player? attacker;
            bool nullifySpell = false;

            if (target == player1)
            {
                attacker = player2;
            }
            else
            {
                attacker = player1;
            }

            while (spellStack.Count > 0)
            {
                Card currentSpell = spellStack.Pop();

                if (currentSpell.CardEffect != null)
                {
                    // If spell is nullified, go to next spell
                    if (nullifySpell)
                    {
                        nullifySpell = false;
                        continue;
                    }

                    // If spell effect nullies next spell, set nullifySpell to true and go to the next spell
                    if (currentSpell.CardEffect is NullifySpell)
                    {
                        nullifySpell = true;
                        continue;
                    }

                    // Apply effect of the spell
                    currentSpell.CardEffect.ApplyEffect();
                }
            }
        }
    }
}