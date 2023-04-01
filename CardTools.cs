using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Black_Jack
{
    static class CardTools
    {
        public static Stack<Card> Clone(Stack<Card> thisCard)
        {
            Stack<Card> newCard = new Stack<Card>();

            Card[] temp = new Card[thisCard.Count()];

            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = thisCard.Pop();
            }

            for (int i = temp.Length - 1; i >= 0; i--)
            {
                newCard.Push(temp[i]);
                thisCard.Push(temp[i]);
            }

            return newCard;
        }

        public static void PrintStack(Stack<Card> thisCard)
        {
            Stack<Card> tempStack = Clone(thisCard);

            while (tempStack.Count() != 0)
            {
                Console.WriteLine(tempStack.Pop().GetName());
            }
        }

        public static Stack<Card> Shuffle(Stack<Card> thisCard)
        {
            Stack<Card> tempCard = new Stack<Card>();

            return tempCard;
        }

        public static Boolean ContainsAce11(Stack<Card> cards)
        {
            Boolean containsAce11 = false;

            Stack<Card> tempCards = Clone(cards);

            while (tempCards.Count != 0)
            {
                if (tempCards.Pop().GetValue2() == 11)
                {
                    containsAce11 = true;
                }
            }

            return containsAce11;
        }

        public static Stack<Player> ClonePlayer(Stack<Player> players)
        {
            Stack<Player> newPlayers = new Stack<Player>();

            Player[] supplier = new Player[players.Count()];

            for (int i = 0; i < supplier.Length; i++)
            {
                supplier[i] = players.Pop();
            }

            for (int i = supplier.Length - 1; i >= 0; i--)
            {
                players.Push(supplier[i]);
                newPlayers.Push(supplier[i]);
            }


            return newPlayers;
        }

        public static void PrintPlayers(Stack<Player> players)
        {
            Stack<Player> tempPlayers = ClonePlayer(players);

            Console.Write("The Players are: ");
            while (tempPlayers.Count() != 0)
            {
                Console.Write(tempPlayers.Pop().GetName() + ", ");
            }
        }

        public static Stack<Player> SwitchStack(Stack<Player> myStack)
        {
            Stack<Player> newStack = new Stack<Player>();

            while (myStack.Count() != 0)
            {
                newStack.Push(myStack.Pop());
            }

            return newStack;
        }
    }
}
