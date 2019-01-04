using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Card
    {
        public CardSuit Suit { get; set; }

        public CardName Name { get; set; }

        public int Value
        {
            get
            {
                int cardVal = ((int)Name) / Enum.GetValues(typeof(CardSuit)).Length;
                if (Name > CardName.Ten)
                {
                    cardVal = 10;
                }
                return cardVal;
            }
        }

        /// <summary>
        /// Displays Card Name.
        /// </summary>
        public string DisplayName
        {
            get
            {
                return this.Name.ToString() + " of " + this.Suit.ToString();
            }
        }
    }
}
