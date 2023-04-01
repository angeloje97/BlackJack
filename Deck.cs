using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Black_Jack
{
    class Deck
    {
        Stack<Card> deck = new Stack<Card>();

        private string[] suit = { "Hearts", "Spades", "Clubs", "Diamonds" };
        private int[] values = { 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        private string[] specialNames = { "Ace", "Jack", "Queen", "King" };

        public void SetCards()
        {
            for (int i = 0; i < values.Length; i++)
            {
                for (int j = 0; j < suit.Length; j++)
                {
                    Card newCard = new Card(values[i], suit[j]);
                    deck.Push(newCard);
                }
            }

            for (int i = 0; i < specialNames.Length; i++)
            {
                for (int j = 0; j < suit.Length; j++)
                {
                    switch (specialNames[i])
                    {
                        case "Ace":
                            Card newCard = new Card(1, suit[j]);
                            deck.Push(newCard);
                            newCard.SpecialName("Ace");
                            break;


                        case "Jack":
                            Card newCard1 = new Card(10, suit[j]);
                            deck.Push(newCard1);
                            newCard1.SpecialName("Jack");
                            break;

                        case "Queen":
                            Card newCard2 = new Card(10, suit[j]);
                            deck.Push(newCard2);
                            newCard2.SpecialName("Queen");
                            break;

                        case "King":
                            Card newCard3 = new Card(10, suit[j]);
                            newCard3.SpecialName("King");
                            deck.Push(newCard3);
                            break;
                    }
                }

            }
        } // ------------------------------------------------------------------

        public int GetSize() { return deck.Count(); }

        public Card TopDeck() { return deck.Pop(); }

        public void Put(Card card) { deck.Push(card); }

        public string PeekTop()
        {
            if (deck.Count() != 0)
            {
                return deck.Peek().GetName();
            }
            else
            {
                return "The Deck is Empty";
            }
        }


        public void GetRandomCard(int value)
        {



        }

        public void Shuffle(int rep)
        {

            Card[] temp = new Card[deck.Count()];
            int index = 0;

            Random random = new Random();


            while (deck.Count() != 0)
            {
                int num = random.Next(1, 3);

                if (index == 52)
                {
                    index = 0;
                }
                else if (num == 2 && temp[index] == null)
                {
                    temp[index] = deck.Pop();
                    index++;
                }
                else
                {
                    index++;
                }

            }

            for (int i = temp.Length - 1; i >= 0; i--)
            {
                deck.Push(temp[i]);
            }

            rep--;

            ResetAce();

            if (rep != 0)
            {
                Shuffle(rep);
            }

        }

        public void ResetAce()
        {
            Stack<Card> tempCards = new Stack<Card>();

            while(deck.Count() != 0)
            {
                if(deck.Peek().GetSpecialName() == "Ace")
                {
                    deck.Peek().ResetValue2();
                }

                tempCards.Push(deck.Pop());
            }

            while(tempCards.Count() != 0)
            {
                deck.Push(tempCards.Pop());
            }
        }

        public void PrintDeck()
        {
            CardTools.PrintStack(deck);
        }

    }
}
