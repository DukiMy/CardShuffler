using System;
using System.Globalization;
using System.Collections.Generic;
using System.IO;

namespace ExploreEnums
{
    enum CardNumbers
    {
        Ace = 1,
        Jack = 11,
        Queen = 12,
        King = 13
    }

    enum Suits
    {
        Clubs,
        Diamonds,
        Hearts,
        Spades
    }

    class Program
    {
        private static List<string> cardDeck = new List<string>();

        public static void PrintCards(List<string> list)
        {
            foreach (string card in list)
            {
                Console.WriteLine(card);
            }
        }

        public static List<string> ReverseDeck(List<string> list)
        {
            string temp = null;

            for (int i = 0; i < list.Count / 2; i++)
            {
                temp = list[i];
                list[i] = list[list.Count - i - 1];
                list[list.Count - i - 1] = temp;
            }

            return list;
        }

        public static List<string> CreateDeck()
        {
            List<string> result = new List<string>();

            for (int i = 0, j = 1, k = 0; i < 52; i++, j++)
            {
                result.Add($"Card: {(CardNumbers)j} of {(Suits)k}");
                if (j == 13) { j = 0; k++; }
            }

            return result;
        }

        public static List<string> RiffleShuffle(List<string> list, int times)
        {
            List<string> firstHalf = new List<string>();
            List<string> secondHalf = new List<string>();

            Console.WriteLine($"\n___After {times:N0} shuffles___\n");

            while (times != 0)
            {
                for (int i = 0; i < list.Count / 2; i++)
                {
                    firstHalf.Add(list[i]);
                    secondHalf.Add(list[(list.Count / 2) + i]);
                }

                list.Clear();

                for (int i = 0, j = 0, k = 0; i < 52; i++)
                {
                    if(i % 2 == 1)
                    {
                        list.Add(firstHalf[j]);
                        j++;
                    }
                    else
                    {
                        list.Add(secondHalf[k]);
                        k++;
                    }
                }

                times--;
                firstHalf.Clear();
                secondHalf.Clear();
            }

            return list;
        }

        public static List<string> FisherYatesShuffle(List<string> list)
        {
            string temp;
            Random rand = new Random();

            for (int i = list.Count - 1; i >= 0; i--)
            {
                int randNr = rand.Next(0, list.Count);

                temp = list[i];
                list[i] = list[randNr];
                list[randNr] = temp;
            }

            return list;
        }

        public static void CompareDecks(int timesShuffled)
        {
            int timesMatched = 0;
            List<string> sortedDeck = CreateDeck();

            while (timesShuffled != 0)
            {
                List<string> shuffledDeck = RiffleShuffle(CreateDeck(), timesShuffled);

                for (int i = 0; i < sortedDeck.Count; i++)
                {
                    if (sortedDeck[i] == shuffledDeck[i])
                    {
                        timesMatched++;
                    }
                }

                if (timesMatched == sortedDeck.Count)
                {
                    Console.SetCursorPosition(0, 0);

                    Console.WriteLine($"When the cards are shuffled {timesShuffled} times, then they are the same.");
                    timesShuffled = 0;
                }
                else
                {
                    Console.SetCursorPosition(0, 5);
                    Console.WriteLine($"There where no matching decks from 0 times shuffled to {timesShuffled} times shuffled.");
                }

                timesShuffled--;
            }
        }

        static void Main(string[] args)
        {
            CultureInfo.CurrentCulture = new CultureInfo("sv-SE");

            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            Console.WriteLine($"\n___After 0 shuffles___\n");

            PrintCards(CreateDeck());

            Console.WriteLine($"\n___After 1 Fisher-Yates shuffle___\n");


            PrintCards(FisherYatesShuffle(RiffleShuffle(CreateDeck(), 247)));

            //PrintCards(ShuffleDeck(CreateDeck(), 247));

            //CompareDecks(1000);

            //using (StreamWriter output = new StreamWriter(Path.Combine(docPath, "Cards.csv")))
            //{
            //    foreach (string card in ShuffleDeck(CreateDeck(), 1))
            //    {
            //        output.WriteLine(card);
            //    }
            //}
        }
    }
}