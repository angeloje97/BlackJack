using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Black_Jack
{
    class Table
    {
        private Stack<Card> playedCards;

        public Table()
        {
            playedCards = new Stack<Card>();
        }

        public int Stack() { return playedCards.Count(); }

        public void Put(Card card)
        {
            playedCards.Push(card);
        }

        public Card ReturnCard()
        {
            return playedCards.Pop();
        }

        public int GetSize() { return playedCards.Count(); }
    }
}

