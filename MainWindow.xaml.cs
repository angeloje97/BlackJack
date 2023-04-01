using Black_Jack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Black_Jack_Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int gameCount = 0;
        private Player player;
        private Player dealer;
        private Deck deck;
        private Black_Jack.Table table;

        Stack<Image> dealerCards = new Stack<Image>();
        Stack<Image> playerCards = new Stack<Image>();
        Stack<Image> tempDealerCards = new Stack<Image>();
        Stack<Image> tempPlayerCards = new Stack<Image>();

        Boolean roundPlayed = false;
        Boolean isBetting = false;
        Boolean onHittingPhase = false;
        Boolean canHold = false;
        Boolean gameStarted = false;
        Boolean hasBetted = false;

        private string bettingEntry;
        private int bettingChips;

        public MainWindow()
        {
            InitializeComponent();

        }



        //Functions ---------------------------------------------------

        private string DisplayGameCount()
        {
            string display = "Game " + gameCount;

            return display;
        }

        private void Wait(int i)
        {
            System.Threading.Thread.Sleep(i * 1000);
        }

        private void Update()
        {
            gameTextBox.Text = DisplayGameCount();
            winTextBox.Text = "Wins: " + player.GetWins();
            tableTextBox.Text = "Cards played: " + table.GetSize();
            deckTextBox.Text = "Cards in Deck: " + deck.GetSize();
            chipsTextBox.Text = "Chips: " + player.GetChips();

            handValueText.Text = "Player Hand Value: " + player.GetHandValue();
        }

        private void PlayerPlays()
        {
            player.Take(deck.TopDeck());

            playerCards.Peek().Source = player.PeekTop().CardImage();
            tempPlayerCards.Push(playerCards.Pop());

            Update();
        }

        private void DealerPlays()
        {
            dealer.Take(deck.TopDeck());

            dealerCards.Peek().Source = dealer.PeekTop().CardBack();
            tempDealerCards.Push(dealerCards.Pop());

            Update();
        }



        private void FillStacks()
        {
            dealerCards.Push(dcard1);
            dealerCards.Push(dcard2);
            dealerCards.Push(dcard3);
            dealerCards.Push(dcard4);
            dealerCards.Push(dcard5);
            dealerCards.Push(dcard6);
            dealerCards.Push(dcard7);

            playerCards.Push(pcard1);
            playerCards.Push(pcard2);
            playerCards.Push(pcard3);
            playerCards.Push(pcard4);
            playerCards.Push(pcard5);
            playerCards.Push(pcard6);
            playerCards.Push(pcard7);

            while (tempDealerCards.Count() != 0)
            {
                tempDealerCards.Pop();
            }

            while (tempPlayerCards.Count() != 0)
            {
                tempPlayerCards.Pop();
            }
        }

        private void ClearHands()
        {
            while (tempPlayerCards.Count() != 0)
            {
                tempPlayerCards.Peek().Source = null;

                playerCards.Push(tempPlayerCards.Pop());
            }

            while (tempDealerCards.Count() != 0)
            {
                tempDealerCards.Peek().Source = null;

                dealerCards.Push(tempDealerCards.Pop());
            }
        }

        private void ClearHands2()
        {
            while(player.GetHandSize() !=0)
            {
                table.Put(player.TopHand());
            }

            while(dealer.GetHandSize() != 0)
            {
                table.Put(dealer.TopHand());
            }
        }


        private void PlayRound()
        {
            if (!roundPlayed)
            {
                roundPlayed = true;

                BetPhase();


            }
            else
            {
                MessageBox.Show("The Round is already in play");
            }


            Update();
        }

        private void BetPhase()
        {
            statusBox.Text = "Enter Betting Amount";
            isBetting = true;

        }

        private void DistributeCards()
        {

            DealerPlays();
            PlayerPlays();
            DealerPlays();
            PlayerPlays();

            RevealTop();

            statusBox.Text = "Distributing Cards";
        }

        private void DealerPhase()
        {
            while (dealer.GetHandValue() <= 16)
            {
                DealerPlays();
            }

            if(dealer.GetHandValue() > 21)
            {
                statusBox.Text = "Dealer Drew Over 21 and Broke";
            }
            else
            {
                statusBox.Text = "The dealer's hand is " + dealer.GetHandValue();
            }

            Update();
        }

        private void DealerReveal()
        {
            Stack<Card> tempCards = CardTools.Clone(dealer.Hand);

            while(tempDealerCards.Count() != 0)
            {
                tempDealerCards.Peek().Source = null;
                dealerCards.Push(tempDealerCards.Pop());
            }

            while(tempCards.Count != 0)
            {
                dealerCards.Peek().Source = tempCards.Pop().CardImage();

                tempDealerCards.Push(dealerCards.Pop());
            }
        }

        private void EndRound()
        {
            if(player.GetHandValue() <= 21)
            {
                if(player.GetHandValue() > dealer.GetHandValue() || dealer.GetHandValue() > 21)
                {


                    statusBox.Text = "You Win!";
                    player.Win(bettingChips);
                    Update();
                    roundPlayed = false;
                }
                else if(player.GetHandValue() == dealer.GetHandValue())
                {
                    statusBox.Text = "You tied!";
                    player.returnChips(bettingChips);
                    bettingChips = 0;
                    Update();
                    roundPlayed = false;
                }
                else
                {
                    statusBox.Text = "You Lost";
                    bettingChips = 0;
                    Update();
                    roundPlayed = false;
                }
            }
            else
            {
                statusBox.Text = "You Lost";
                bettingChips = 0;
                Update();
                roundPlayed = false;
            }

            hasBetted = false;

            Update();

        }

        private void RevealTop()
        {
            tempDealerCards.Peek().Source = dealer.PeekTop().CardImage();
        }

        private void ReShuffle()
        {
            while(table.GetSize()!= 0)
            {
                deck.Put(table.ReturnCard());
            }

            deck.Shuffle(5);
        }

        //Butons -------------------------------------------------------------------------------------------------------------------------------------

        private void playButton_Click(object sender, RoutedEventArgs e)
        {


            if(!roundPlayed && gameStarted)
            {
                if (player.GetHandSize() != 0 || dealer.GetHandSize() != 0)
                {
                    ClearHands2();
                    ClearHands();
                }

                if(deck.GetSize() < 20)
                {
                    ReShuffle();
                }

                gameCount++;

                PlayRound();
            }
            else
            {
                MessageBox.Show("Can't play a new round at the moment");
            }
            
        }

        

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            ClearHands();
            FillStacks();
            roundPlayed = false;
            gameStarted = true;

            player = new Player("Angelo");
            dealer = new Player("Dealer");
            deck = new Deck();
            deck.SetCards();
            table = new Black_Jack.Table();
            deck.Shuffle(5);
            gameCount = 0;

            resetButton.Content = "Reset";

            statusBox.Text = "Idle Game";

            Update();
        }

        private void betButton_Click(object sender, RoutedEventArgs e)
        {
            Boolean canBet = true;

            if(roundPlayed == true)
            {
                if(player.GetChips() == 0)
                {
                    MessageBox.Show("You don't have anything to bet with.");
                    canBet = false;
                }
            }


            if (isBetting)
            {
                bettingEntry = ($"{betTextBox.Text}");

                if (bettingEntry == null)
                {
                    MessageBox.Show("Betting is empty");
                }
                else
                {
                    bettingChips = Convert.ToInt32($"{ betTextBox.Text }");



                    if (bettingChips > player.GetChips() && player.GetChips() != 0)
                    {
                        MessageBox.Show("You Can't Bet That Amount");
                    }
                    else
                    {
                        if (canBet)
                        {
                            player.Bet(bettingChips);
                        }
                        else
                        {
                            bettingChips = 0;
                        }

                        isBetting = false;
                        hasBetted = true;
                        DistributeCards();

                        Update();

                        onHittingPhase = true;
                        canHold = true;
                        statusBox.Text = "Hit or Hold?";

                        if(player.GetHandValue() == 21)
                        {
                            statusBox.Text = "You got 21!";
                        }
                    }
                }
                
            }
            else
            {
                MessageBox.Show("You can't place a bet at the moment");
            }
        }

        private void hitButton_Click(object sender, RoutedEventArgs e)
        {
            Boolean broke = player.GetHandValue() > 21;
            Boolean has21 = player.GetHandValue() == 21;

            if(onHittingPhase && !broke && !has21)
            {
                PlayerPlays();
                broke = player.GetHandValue() > 21;
                has21 = player.GetHandValue() == 21;

            }
            else
            {
                MessageBox.Show("You can't Hit at the moment");
            }



            if (broke)
            {
                statusBox.Text = "You Went Over 21";
            }

            if (has21)
            {
                statusBox.Text = "You got 21!";
            }

            Update();
        }

        private void holdButton_Click(object sender, RoutedEventArgs e)
        {
            if(canHold && hasBetted)
            {
                statusBox.Text = "Dealer Phase";

                DealerPhase();
                DealerReveal();
                EndRound();
            }
            else
            {
                MessageBox.Show("You can't hold at the moment");
            }
        }
    }
}
