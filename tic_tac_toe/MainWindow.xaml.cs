using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System;

namespace tic_tac_toe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members

        private MarkType[] mResults;        //current results of cells
        private bool mPlayer1Turn;          //true player 1 (X) false player 2 (O)
        private bool mGameEnded;            //true ended, false not

        #endregion

        #region Constructor 
        /// <summary>
        /// defoult constructor 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            NewGame();

        }
        #endregion

        private void NewGame()
        {
            mResults= new MarkType[9];

            for(var i = 0; i < mResults.Length; i++)
            {
                mResults[i] = MarkType.Free;
            }

            mPlayer1Turn = true;
            //iterate everybutton on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                //change colors to default
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground= Brushes.Plum;
            });
            mGameEnded = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(mGameEnded)
            {
                NewGame();
                return;
            }

            var button = (Button)sender;
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);
            var index = column + (row * 3);

            if(mResults[index] != MarkType.Free)
                return;

            mResults[index] = mPlayer1Turn ? MarkType.Cross :MarkType.Nought;
            button.Content = mPlayer1Turn ? "X" : "O";

            if (!mPlayer1Turn)
                button.Foreground = Brushes.LightBlue;

            mPlayer1Turn ^= true;
            CheckForWinner();
        }

        private void CheckForWinner()
        {
            #region horizontal wins
            //check for horizontal wins
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                mGameEnded = true;
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.PaleGreen;
            }
            else if(mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                mGameEnded = true;
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.PaleGreen;
            }
            else if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                mGameEnded = true;
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.PaleGreen;
            }
            #endregion

            #region vertical wins
            //check for vertical wins
            else if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                mGameEnded = true;
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.PaleGreen;
            }
            else if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                mGameEnded = true;
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.PaleGreen;
            }
            else if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                mGameEnded = true;
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.PaleGreen;
            }
            #endregion

            #region diagonal wins
            //check for diagonal wins
            else if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                mGameEnded = true;
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.PaleGreen;
            }
            else if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                mGameEnded = true;
                Button0_2.Background = Button1_1.Background = Button2_0.Background = Brushes.PaleGreen;
            }
            #endregion

            #region tie
            //no move
            else if (!mResults.Any(result => result == MarkType.Free))
            {
                mGameEnded = true;
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.MistyRose;
                });
            }
            #endregion
        }
    }
}
