using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Black_Jack
{
    class Card
    {
        private string name;
        private string specialName;
        private string suit;
        private int value1;
        private int value2 = 0;

        private string l1;
        private string l2;

        public Card(int value, string suit)
        {
            this.value1 = value;
            this.suit = suit;
            specialName = "None";

            name = value + " of " + suit;

            l1 += value;
            l2 += suit[0];
        }
        public Card() { }

        public string GetName() { return name; }

        public string GetSuit() { return suit; }

        public int GetValue1() { return value1; }

        public int GetValue2() { return value2; }
        public void SpecialName(string newName)
        {
            specialName = newName;
            name = newName + " of " + suit;

            if (newName == "Ace") { value2 = 11; }

            l1 = "";

            l1 += newName[0];
        }

        public string GetSpecialName() { return specialName; }

        public void ChangeValue2() { value2 = 1; }

        public string CardPicture()
        {
            // string fileLocation = "A:\\Python Projects\\Random Projects\\Black Jack\\Card Files/";
            string fileLocation = "./Cards/"
            fileLocation += l1 + l2;
            fileLocation += ".png";

            return fileLocation;
        }

        public System.Windows.Media.ImageSource CardImage()
        {
            Uri uri = new Uri(CardPicture(), UriKind.Absolute);

            ImageSource myImage = new BitmapImage(uri);

            return myImage;
        }

        public ImageSource CardBack()
        {
            string url = "A:\\Python Projects\\Random Projects\\Black Jack\\Card Files/GB.png";

            Uri uri = new Uri(url, UriKind.Absolute);
            ImageSource myImage = new BitmapImage(uri);
            return myImage;
        }

        public void ResetValue2()
        {
            value2 = 11;
        }
    }
}
