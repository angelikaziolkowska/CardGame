using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Card game where cards are dealt out to a group of players so that each player 
/// has the same number of cards, then displays each player’s hand and the total 
/// value of their hand in ranked order. 
/// </summary>
namespace ConsoleApp
{

    enum CardName
    {
        Ace = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13
    }

    enum CardSuit
    {
        Hearts = 1,
        Spades = 2,
        Clubs = 3,
        Diamonds = 4
    }
    

    class CardGame
    {
        /// <summary>
        /// Empty Cards Array.
        /// </summary>
        public static List<Card> cards = new List<Card>();


        /// <summary>
        /// Main.
        /// </summary>
        public static void Main(string[] args)
        {
            while (true)
            {
                CardGame cardGame = new CardGame();
                int players = cardGame.Players();
                cards = DeckOfCards();
                cards = cardGame.Shuffle(cards);
                string[] playerNames = cardGame.PlayerNames(players);

                var list = cardGame.PlayerHands(players, cards, playerNames);


                var grouped = list.GroupBy(m => m.PlayerName)
                                    .Select(g => new
                                    {
                                        playerName = g.Key,
                                        Value = g.Sum(s => s.Value),
                                        Cards = g
                                    }).OrderByDescending(n => n.Value);


                foreach (var playerGroup in grouped)
                {
                    Console.Write(playerGroup.playerName + ", Points: " + playerGroup.Value + ", Player cards:");


                    var playerCardDisplayNames = playerGroup.Cards.Select(c => c.CardNum.DisplayName);
                    var aggregatedNames = playerCardDisplayNames.Aggregate((aggr, next) => aggr + ", " + next);
                    Console.Write(" " + aggregatedNames);

                    Console.WriteLine();
                }

            }
        }

        /// <summary>
        /// Get User Input.
        /// </summary>
        public int Players()
        {
            int p = 0;
            do
            {
                Console.WriteLine("Please Enter an a number of players 2-10 or q,quit to exit application:");
                string input = Console.ReadLine();

                bool result = int.TryParse(input, out p);

                if (result && p >= 2 && p <= 10)
                {

                    return p;

                }
                else if (input == "quit" || input == "q")
                {
                    Environment.Exit(0);
                }
                
            } while (p == 0);
            
            return 0;
        }

        /// <summary>
        /// Create the Card Deck.
        /// </summary>
        public static List<Card> DeckOfCards()
        {

            List<Card> cards = new List<Card>();

            foreach (object suit in Enum.GetValues(typeof(CardSuit)))
            {
                foreach (object cardName in Enum.GetValues(typeof(CardName)))
                {
                    cards.Add(new Card()
                    {
                        Suit = (CardSuit)suit,
                        Name = (CardName)cardName
                    });
                }
            }
            
            return cards;
        }

        /// <summary>
        /// Shuffle the array.
        /// </summary>     
        public T[] Shuffle<T>(T[] array)
        {
            Random _random = new Random();
            var random = _random;            
            for (int i = array.Length; i > 1; i--)
            {
                // Pick random element to swap.
                int j = random.Next(i); // 0 <= j <= i-1
                                        // Swap.
                T tmp = array[j];
                array[j] = array[i - 1];
                array[i - 1] = tmp;
            }
            return array;
        }

        public List<T> Shuffle<T>(List<T> list)
        {
            Random _random = new Random();
            var random = _random;
            for (int i = list.Count; i > 1; i--)
            {
                // Pick random element to swap.
                int j = random.Next(i); // 0 <= j <= i-1
                                        // Swap.
                T tmp = list[j];
                list[j] = list[i - 1];
                list[i - 1] = tmp;
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary> 
        public class PlayerCard
        {
            public string PlayerName { get; set; }
            public Card CardNum { get; set; }
            public int Value { get; set; }
        }


        /// <summary>
        /// Create Array of Player Names.
        /// </summary> 
        public string[] PlayerNames(int players)
        {
            string[] playerNames = new string[players];

            for(int i = 0; i < players; i++)
            {
                playerNames[i] = String.Concat("Player ", (i+1).ToString());
            }

            return playerNames;
        }

        /// <summary>
        /// Player Cards saved in List.
        /// </summary> 
        public List<PlayerCard> PlayerHands (int players, List<Card> cards, string[] playerNames)
        {

            List<PlayerCard> PlayerCardsList = new List<PlayerCard>();
            int cardsPerPlayer = cards.Count / players;

            // Go through every player
            for (int i = 0; i < players; i++)
            {
                // Go through every card for the player
                int startVal = i * cardsPerPlayer;
                for (int j = startVal; j < startVal + cardsPerPlayer; j++)
                {
                    // Add Card To List
                    PlayerCardsList.Add(new PlayerCard()
                    {
                        PlayerName = playerNames[i],
                        CardNum = cards[j],
                        Value = cards[j].Value// CalculateValue(cards[j])
                    });
                }                
            }
            return PlayerCardsList;
        }
    }
}

