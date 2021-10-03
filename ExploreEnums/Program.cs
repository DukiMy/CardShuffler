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

        public static List<string> ShuffleDeck(List<string> list, int times)
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

        static void Main(string[] args)
        {
            CultureInfo.CurrentCulture = new CultureInfo("sv-SE");

            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            PrintCards(CreateDeck());

            PrintCards(ShuffleDeck(CreateDeck(), 1000000));

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