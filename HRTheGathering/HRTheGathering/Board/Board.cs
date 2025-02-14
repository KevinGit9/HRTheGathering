﻿using System;
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
            currentRound = 1;
            player1 = new Player { Id = 1, Name = "Player 1" };
            player2 = new Player { Id = 2, Name = "Player 2" };

            player1LifeObserver = new PlayerHealthObserver();
            player2LifeObserver = new PlayerHealthObserver();

            player1HandObserver = new PlayerHandObserver();
            player2HandObserver = new PlayerHandObserver();

            player1BoardObserver = new PlayerBoardObserver();
            player2BoardObserver = new PlayerBoardObserver();


            // Attach the observers to the players
            player1.HealthObservable.Attach(player1LifeObserver);
            player1.HandObservable.Attach(player1HandObserver);
            player1.BoardObservable.Attach(player1BoardObserver);

            player2.HealthObservable.Attach(player2LifeObserver);
            player2.HandObservable.Attach(player2HandObserver);
            player2.BoardObservable.Attach(player2BoardObserver);

            // Subscribe players to publisher channels
            publisher.SubscribeCardsInHand(player1);
            publisher.SubscribeCardsInHand(player2);
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
            PrepareGame();

            Console.WriteLine("Done preparing the game - press any key to start the game...");
            Console.ReadKey();

            RunGame();
        }

        private void PrepareGame()
        {
            // Create decks for both players
            player1.Deck = cardFactory.CreateDeck(Card.Color.White, player1, player2, publisher);
            player2.Deck = cardFactory.CreateDeck(Card.Color.Red, player2, player1, publisher);

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
       
        private void RunGame()
        {
            while (winner == null)
            {
                StartRound();
            }
        }

        private void StartRound()
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

        private void StartTurn(Player player)
        {
            Console.WriteLine("\n\n---------------------------------------------------------------------------------------------");
            Console.WriteLine($"\n\nTurn {currentRound}");
            Console.WriteLine($"Player: {player.Name}\n\n");
            Console.WriteLine("---------------------------------------------------------------------------------------------\n\n");

            // Preparation:
            // Reset temporary effects and reset to original state (example: lands turn back to normal)
            PreparationPhase(player);

            // Drawing:
            // Player draws card from deck and add its to their hand
            bool drewCard = player.DrawCard();

            // if drawnCard is false, it means the Deck has no more cards left to draw
            if (!drewCard)
            {
                EndGame(player);
            }

            // Main:
            // Player can play cards and attack
            MainPhase(player);

            // Apply the spell and/or instant cards that have been played
            ApplySpells();

            // Try to attack the opponent
            Attack(player);

            // Ending:
            EndTurn(player);
            Console.WriteLine($"This is the end of the turn of {player.Name} - press any key to continue...");
            Console.ReadKey();
        }

        private void PreparationPhase(Player player)
        {
            UnturnAllTurnedLandCards(player);
            UnturnAllCreatureCards(player);
            UpdateSpellCards(player);
        }

        private void MainPhase(Player player)
        {
            Player opposingPlayer;
            if (player == player1)
            {
                opposingPlayer = player2;
            }
            else
            {
                opposingPlayer = player1;
            }

            // Play all lands in hand
            PlayLands(player);

            // Try to play a creature
            PlayCreature(player);

            // Try to play a spell, save if played
            bool spellCardPlayed = PlaySpell(player);

            // If spell card has been played, check if the opponent has an instant card to play
            if (spellCardPlayed)
            {
                InstantCard? instantCardOpponent = opposingPlayer.Hand.FirstOrDefault(card => card is InstantCard) as InstantCard;

                if (instantCardOpponent != null)
                {
                    bool opponentNullifies = opposingPlayer.UseCardWithCost(instantCardOpponent, publisher, spellStack);

                    // If opponent plays an instant card, check if the current player can play an instant card
                    if (opponentNullifies)
                    {
                        InstantCard? instantCard = player.Hand.FirstOrDefault(card => card is InstantCard) as InstantCard;
                        if (instantCard != null)
                        {
                            player.UseCardWithCost(instantCard, publisher, spellStack);
                        }
                    }
                }
            }
        }

        private void PlayLands(Player player)
        {
            // Play every land card the player owns
            while (player.Hand.Any(card => card is LandCard))
            {
                LandCard? landCard = player.Hand.FirstOrDefault(card => card is LandCard) as LandCard;
                if (landCard != null)
                {
                    player.UseCard(landCard, publisher);
                }
            }
        }

        private void PlayCreature(Player player)
        {
            // If the player has a CreatureCard, try to play it
            CreatureCard? creatureCard = player.Hand.FirstOrDefault(card => card is CreatureCard) as CreatureCard;
            if (creatureCard != null)
            {
                player.UseCardWithCost(creatureCard, publisher);
            }
        }

        private bool PlaySpell(Player player)
        {
            bool spellCardPlayed = false;
            // If the player has a SpellCard, try to play it
            SpellCard? spellCard = player.Hand.FirstOrDefault(card => card is SpellCard) as SpellCard;
            if (spellCard != null)
            {
                spellCardPlayed = player.UseCardWithCost(spellCard, publisher, spellStack);
            }

            return spellCardPlayed;
        }

        private void EndTurn(Player player)
        {
            // Player must discard cards from their hand until the cards in hand dont exceed MaxCardsInHand (7)
            while (player.Hand.Count > player.MaxCardsInHand)
            {
                Console.WriteLine($"\n{player.Name} hand exceeds max cards\n");

                Random random = new Random();
                // Get a random card from the hand to discard
                int randomIndex = random.Next(player.Hand.Count);
                Card randomCard = player.Hand[randomIndex];

                player.DiscardCard(randomCard, publisher, true);
            }

            // Log what the status of the players at the end of the turn.
            PrintEndSituation(player1);
            PrintEndSituation(player2);
        }

        private void PrintEndSituation(Player player)
        {
            Console.WriteLine($"\n\n{player.Name}:");
            Console.WriteLine($"Cards in hand: {player.Hand.Count}");
            int numberOfLands = player.CardsOnBoard.Count(card => card is LandCard);
            Console.WriteLine($"Lands in field: {numberOfLands}");
            int numberOfCreatures = player.CardsOnBoard.Count(card => card is CreatureCard);
            Console.WriteLine($"Creatures in field: {numberOfCreatures}");
            Console.WriteLine($"Cards in discard pile: {player.DiscardPile.Count}");
            Console.WriteLine($"Lives: {player.Health}\n");
        }

        private void EndGame(Player loser)
        {
            // Determine the winning player
            if (loser == player1)
            {
                winner = player2;
            }
            else
            {
                winner = player1;
            }

            Console.WriteLine("\n\n---------------------------------------------------------------------------------------------");
            Console.WriteLine($"\n\n{winner.Name} has won the game!\n\n");
            Console.WriteLine("press any key to exit the application...\n\n");
            Console.WriteLine("---------------------------------------------------------------------------------------------\n\n");
            Console.ReadKey();

            // Detach all observers
            player1.HealthObservable.Detach(player1LifeObserver);
            player1.HandObservable.Detach(player1HandObserver);
            player1.BoardObservable.Detach(player1BoardObserver);

            player2.HealthObservable.Detach(player2LifeObserver);
            player2.HandObservable.Detach(player2HandObserver);
            player1.BoardObservable.Detach(player2BoardObserver);

            // End the game by closing the instance
            Environment.Exit(0);
        }

        private void UnturnAllTurnedLandCards(Player player)
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

        private void UnturnAllCreatureCards(Player player)
        {
            foreach (Card card in player.CardsOnBoard)
            {
                if (card is CreatureCard)
                {
                    CreatureCard creature = (CreatureCard)card;
                    if (creature.IsTurned)
                    {
                        creature.IsTurned = false;
                    }
                }
            }
        }

        private void UpdateSpellCards(Player player)
        {
            for (int i = player.CardsOnBoard.Count - 1; i >= 0; i--)
            {
                Card card = player.CardsOnBoard[i];

                if (card is not SpellCard || card.CardEffect == null)
                {
                    continue;
                }

                // If card effect doesn't have a duration, its permanent, return out
                if (card.CardEffect.Duration ==  null)
                {
                    continue;
                }

                // Decrement duration by 1 turn
                card.CardEffect.DecrementDuration();

                // Check if the card is expired, if so discard it
                if (card.CardEffect.IsExpired())
                {
                    Console.WriteLine($"[{card.CardColor} Spell][{card.Cost}] {card.Name} ({card.CardEffect.Description}) has expired\n");
                    player.DiscardCard(card, publisher);
                }
            }
        }

        private void ApplySpells()
        {
            bool nullifySpell = false;

            while (spellStack.Count > 0)
            {
                Card currentSpell = spellStack.Pop();

                if (currentSpell.CardEffect != null)
                {
                    // If spell is nullified, go to next spell
                    if (nullifySpell)
                    {
                        nullifySpell = false;
                        if (currentSpell is SpellCard)
                        {
                            Console.WriteLine($"[{currentSpell.CardColor} Spell][{currentSpell.Cost}] {currentSpell.Name} ({currentSpell.CardEffect.Description}) has been nullified");
                        }
                        else
                        {
                            Console.WriteLine($"[{currentSpell.CardColor} Instant][{currentSpell.Cost}] {currentSpell.Name} ({currentSpell.CardEffect.Description}) has been nullified");
                        }

                        // Remove the spell from the board and add it to the discard pile
                        if (player1.CardsOnBoard.Contains(currentSpell))
                        {
                            player1.DiscardCard(currentSpell, publisher);
                        }
                        else
                        {
                            player2.DiscardCard(currentSpell, publisher);
                        }

                        continue;
                    }

                    // If spell effect nullies next spell, set nullifySpell to true and go to the next spell
                    if (currentSpell.CardEffect is NullifySpell)
                    {
                        nullifySpell = true;
                        Console.WriteLine($"[{currentSpell.CardColor} Instant][{currentSpell.Cost}] {currentSpell.Name} ({currentSpell.CardEffect.Description}) has applied it's effect");

                        // Remove the spell from the board and add it to the discard pile
                        if (player1.CardsOnBoard.Contains(currentSpell))
                        {
                            player1.DiscardCard(currentSpell, publisher);
                        }
                        else
                        {
                            player2.DiscardCard(currentSpell, publisher);
                        }

                        continue;
                    }

                    // Apply effect of the spell
                    if (currentSpell is SpellCard)
                    {
                        Console.WriteLine($"[{currentSpell.CardColor} Spell][{currentSpell.Cost}] {currentSpell.Name} ({currentSpell.CardEffect.Description}) has applied it's effect\n");
                    }
                    else
                    {
                        Console.WriteLine($"[{currentSpell.CardColor} Instant][{currentSpell.Cost}] {currentSpell.Name} ({currentSpell.CardEffect.Description}) has applied it's effect\n");

                        // Discard card because it's an InstantCard
                        if (player1.CardsOnBoard.Contains(currentSpell))
                        {
                            player1.DiscardCard(currentSpell, publisher);
                        }
                        else
                        {
                            player2.DiscardCard(currentSpell, publisher);
                        }
                    }
                    currentSpell.CardEffect.ApplyEffect();
                }
            }
        }

        private void Attack(Player attacker)
        {
            Player? defender;
            CreatureCard? attackingCreature = null;
            CreatureCard? defendingCreature = null;

            if (attacker == player1)
            {
                defender = player2;
            }
            else
            {
                defender = player1;
            }

            // Assign the creature with the highest attack as the attacker
            foreach (Card card in attacker.CardsOnBoard)
            {
                if (card is CreatureCard)
                {
                    CreatureCard creature = (CreatureCard)card;
                    if (creature.IsTurned == false)
                    {
                        if (attackingCreature == null)
                        {
                            attackingCreature = creature;
                        }
                        else if (creature.Attack > attackingCreature.Attack)
                        {
                            attackingCreature = creature;
                        }
                    }
                }
            }

            // Assign the creature with the highest defense as the defender
            foreach (Card card in defender.CardsOnBoard)
            {
                if (card is CreatureCard)
                {
                    CreatureCard creature = (CreatureCard)card;
                    if (creature.IsTurned == false)
                    {
                        if (defendingCreature == null)
                        {
                            defendingCreature = creature;
                        }
                        else if (creature.Defense > defendingCreature.Defense)
                        {
                            defendingCreature = creature;
                        }
                    }
                }
            }

            // Attack player if there is an attacking creature but no defending creature
            if (attackingCreature != null && defendingCreature == null)
            {
                Console.WriteLine($"\n[{attackingCreature.CardColor} Creature] {attackingCreature.Name} ({attackingCreature.Attack}, {attackingCreature.Defense}) Attacked {defender.Name} with {attackingCreature.Attack}");
                defender.Health -= attackingCreature.Attack;
                attackingCreature.IsTurned = true;
                
                // If defender health is lower or equal to 0, defending player loses
                if (defender.Health <= 0)
                {
                    EndGame(defender);
                    
                }
            }
            // Attack defending creature if there is an attacking creature and defending creature
            else if (attackingCreature != null && defendingCreature != null)
            {
                Console.WriteLine($"\n[{attackingCreature.CardColor} Creature] {attackingCreature.Name} ({attackingCreature.Attack}, {attackingCreature.Defense}) is attacking");
                Console.WriteLine($"[{defendingCreature.CardColor} Creature] {defendingCreature.Name} ({defendingCreature.Attack}, {defendingCreature.Defense}) is defending\n");
                defendingCreature.Defense -= attackingCreature.Attack;
                attackingCreature.Defense -= defendingCreature.Attack;
                attackingCreature.IsTurned = true;
                defendingCreature.IsTurned = true;

                if (defendingCreature.Defense <= 0)
                {
                    defender.DiscardCard(defendingCreature, publisher);
                    Console.WriteLine($"[{defendingCreature.CardColor} Creature] {defendingCreature.Name} ({defendingCreature.Attack}, {defendingCreature.Defense}) was slain in battle");
                }
                if (attackingCreature.Defense <= 0)
                {
                    attacker.DiscardCard(attackingCreature, publisher);
                    Console.WriteLine($"[{attackingCreature.CardColor} Creature] {attackingCreature.Name} ({attackingCreature.Attack}, {attackingCreature.Defense}) was slain in battle");
                }
            }
        }

        // Methods to replicate the small play of the document
        public void SmallPlay()
        {
            // player 1 deck
            player1.Deck.Add(cardFactory.CreateLandCard("Sunlit Meadows", Card.Color.White));
            player1.Deck.Add(cardFactory.CreateLandCard("Radiant Glade", Card.Color.White));
            player1.Deck.Add(cardFactory.CreateLandCard("Luminous Plains", Card.Color.White));
            player1.Deck.Add(cardFactory.CreateLandCard("Celestial Grove", Card.Color.White));
            player1.Deck.Add(cardFactory.CreateLandCard("Sunlit Meadows", Card.Color.White));

            // player 2 deck
            player2.Deck.Add(cardFactory.CreateLandCard("Molten Peak", Card.Color.Red));
            player2.Deck.Add(cardFactory.CreateLandCard("Ember Highlands", Card.Color.Red));
            player2.Deck.Add(cardFactory.CreateLandCard("Volcanic Crater", Card.Color.Red));
            player2.Deck.Add(cardFactory.CreateLandCard("Blaze Ridge", Card.Color.Red));
            player2.Deck.Add(cardFactory.CreateLandCard("Volcanic Crater", Card.Color.Red));

            // player 1 hand
            player1.Hand.Add(cardFactory.CreateLandCard("Sunlit Meadows", Card.Color.White));
            player1.Hand.Add(cardFactory.CreateLandCard("Radiant Glade", Card.Color.White));
            player1.Hand.Add(cardFactory.CreateLandCard("Luminous Plains", Card.Color.White));
            player1.Hand.Add(cardFactory.CreateLandCard("Celestial Grove", Card.Color.White));

            ChangeCardsInHand discardCard1 = new ChangeCardsInHand(-1, player2, publisher, "Discard 1 random cards of the opponents hand.");
            player1.Hand.Add(cardFactory.CreateCreatureCard("Griffon", 2, Card.Color.White, 2, 2, discardCard1));

            NullifySpell nullifySpellWhite = new NullifySpell("Nullify the opponents spell.");
            player1.Hand.Add(cardFactory.CreateInstantCard("Divine Reprieve", 1, Card.Color.White, nullifySpellWhite));
            ChangeStats changeStats3 = new ChangeStats(3, 3, player1, publisher, "Increases all your creatures stats by +3/+3.");
            player1.Hand.Add(cardFactory.CreateSpellCard("Radiant Blessing", 1, Card.Color.White, changeStats3));

            // player 2 hand
            player2.Hand.Add(cardFactory.CreateLandCard("Molten Peak", Card.Color.Red));
            player2.Hand.Add(cardFactory.CreateLandCard("Ember Highlands", Card.Color.Red));
            player2.Hand.Add(cardFactory.CreateLandCard("Volcanic Crater", Card.Color.Red));
            player2.Hand.Add(cardFactory.CreateLandCard("Blaze Ridge", Card.Color.Red));
            player2.Hand.Add(cardFactory.CreateLandCard("Volcanic Crater", Card.Color.Red));

            NullifySpell nullifySpellRed = new NullifySpell("Nullify the opponents spell.");
            player2.Hand.Add(cardFactory.CreateInstantCard("Flame Burst", 1, Card.Color.Red, nullifySpellRed));

            NullifySpell nullifySpellRed2 = new NullifySpell("Nullify the opponents spell.");
            player2.Hand.Add(cardFactory.CreateInstantCard("Flame Burst", 1, Card.Color.Red, nullifySpellRed2));

            // player 1, turn 1
            Console.WriteLine("\n\n---------------------------------------------------------------------------------------------");
            Console.WriteLine($"\n\nTurn {currentRound}");
            Console.WriteLine($"Player: {player1.Name}\n\n");
            Console.WriteLine("---------------------------------------------------------------------------------------------\n\n");

            // Draw card
            player1.DrawCard();

            // Use landcard
            Card landCard1Player1 = GetCard(player1, "Sunlit Meadows");
            player1.UseCard(landCard1Player1, publisher);

            // Use landcard
            Card landCard2Player1 = GetCard(player1, "Radiant Glade");
            player1.UseCard(landCard2Player1, publisher);
            EndTurn(player1);

            // player 2, turn 1
            Console.WriteLine("\n\n---------------------------------------------------------------------------------------------");
            Console.WriteLine($"\n\nTurn {currentRound}");
            Console.WriteLine($"Player: {player2.Name}\n\n");
            Console.WriteLine("---------------------------------------------------------------------------------------------\n\n");
            currentRound++;

            // Draw card
            player2.DrawCard();

            // Use landcard
            Card landCard1Player2 = GetCard(player2, "Molten Peak");
            player2.UseCard(landCard1Player2, publisher);
            EndTurn(player2);

            // player 1, turn 2
            Console.WriteLine("\n\n---------------------------------------------------------------------------------------------");
            Console.WriteLine($"\n\nTurn {currentRound}");
            Console.WriteLine($"Player: {player1.Name}\n\n");
            Console.WriteLine("---------------------------------------------------------------------------------------------\n\n");

            // Unturn landcards
            UnturnAllTurnedLandCards(player1);

            // Draw card
            player1.DrawCard();

            // Use landcard
            Card landCard3Player1 = GetCard(player1, "Luminous Plains");
            player1.UseCard(landCard3Player1, publisher);

            // Use creature card with discard effect
            Card creatureCard1Player1 = GetCard(player1, "Griffon");
            CreatureCard creatureCard1Player1Creature = (CreatureCard)creatureCard1Player1;
            player1.UseCardWithCost(creatureCard1Player1Creature, publisher);
            EndTurn(player1);

            // player 2, turn 2
            Console.WriteLine("\n\n---------------------------------------------------------------------------------------------");
            Console.WriteLine($"\n\nTurn {currentRound}");
            Console.WriteLine($"Player: {player2.Name}\n\n");
            Console.WriteLine("---------------------------------------------------------------------------------------------\n\n");
            currentRound++;

            // Unturn landcards
            UnturnAllTurnedLandCards(player2);

            // Draw card
            player2.DrawCard();
            EndTurn(player2);

            // player 1, turn 3
            Console.WriteLine("\n\n---------------------------------------------------------------------------------------------");
            Console.WriteLine($"\n\nTurn {currentRound}");
            Console.WriteLine($"Player: {player1.Name}\n\n");
            Console.WriteLine("---------------------------------------------------------------------------------------------\n\n");

            // Unturn landcards
            UnturnAllTurnedLandCards(player1);

            // Draw card
            player1.DrawCard();

            // Use spells
            Card spellCard1 = GetCard(player1, "Radiant Blessing");
            player1.UseCardWithCost(spellCard1, publisher, spellStack);
            Card instantCard1 = GetCard(player2, "Flame Burst");
            player2.UseCardWithCost(instantCard1, publisher, spellStack);
            Card instantCard2 = GetCard(player1, "Divine Reprieve");
            player1.UseCardWithCost(instantCard2, publisher, spellStack);
            ApplySpells();

            // Attack
            Attack(player1);
            EndTurn(player1);
        }

        private Card GetCard(Player player, string cardName)
        {
            return player.Hand.FirstOrDefault(card => card.Name == cardName);
        }
    }
}