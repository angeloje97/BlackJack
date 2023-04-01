using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Black_Jack
{
    class Player
    {
        private string name;
        private int handValue;
        private Stack<Card> hand;
        private int wins;
        private int chips;

        private static int playerCount = 0;

        public Player(string name = "Bot")
        {
            this.name = name;
            handValue = 0;
            hand = new Stack<Card>();
            wins = 0;

            chips = 1000;

            playerCount++;
        }

        public string GetName() { return name; }
        public int GetHandValue() { return handValue; }
        public int GetChips() { return chips; }
        public int GetWins() { return wins; }
        public int GetHandSize() { return hand.Count(); }


        public Stack<Card> Hand { get { return hand; } }

        public Card TopHand()
        {
            handValue = 0;
            return hand.Pop();
        }

        public int Bet(int betAmount)
        {
            if(betAmount > chips)
            {
                return 0;
            }
            else
            {
                chips -= betAmount;
                return betAmount;
            }
        }

        public void ShowHand()
        {
            string handString = name + " has ";

            Console.Write(handString);

            CardTools.PrintStack(hand);
        }

        public void Take(Card card)
        {
            hand.Push(card);

            CountHand(card);

            ChangeAceValue();
        }

        private void CountHand(Card card)
        {

            Card tempCard = card;

            if (card.GetSpecialName() == "Ace")
            {
                handValue += tempCard.GetValue2();
            }
            else
            {
                handValue += tempCard.GetValue1();
            }
        }
        private void ChangeAceValue()
        {
            Boolean handOver = handValue > 21;

            Boolean containsAce11 = CardTools.ContainsAce11(hand);

            if (handOver && containsAce11)
            {

                Stack<Card> tempHand = new Stack<Card>();

                while (hand.Count() != 0)
                {
                    if (hand.Peek().GetValue2() == 11)
                    {
                        handValue -= 10;
                        hand.Peek().ChangeValue2();
                    }

                    tempHand.Push(hand.Pop());
                }

                while (tempHand.Count() != 0)
                {
                    hand.Push(tempHand.Pop());
                }


            }
        }

        public void Win(int amount)
        {
            chips += 2 * amount;
            wins++;
        }

        public Card PeekTop()
        {
            return hand.Peek();
        }

        public void returnChips(int returnValue)
        {
            chips += returnValue;
        }
    }
}
