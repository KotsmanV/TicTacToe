using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members

        /// <summary>
        /// Holds the current results of cells in the active game
        /// </summary>
        private MarkType[] mResults;

        /// <summary>
        /// True = player 1, False = player 2
        /// </summary>
        private bool mIsPlayerOne;

        /// <summary>
        /// True = game ended
        /// /// </summary>
        private bool mGameEnded;

        #endregion


        #region Constructor

        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        #endregion

        /// <summary>
        /// Starts a new game and resets all values to default
        /// </summary>
        private void NewGame()
        {
            //New blank array of free cells
            mResults = new MarkType[9];

            for (int i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;

            //Make sure it's Player 1
            mIsPlayerOne = true;

            //Iterate every button on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                //Change background, foreground and content to default values
                button.Content = string.Empty;
                button.Background = Brushes.SeaShell;
                button.Foreground = Brushes.Green;
            });

            //Make sure game has not ended
            mGameEnded = false;
        }


        /// <summary>
        /// Handles a button click event
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">Click events</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            //Cast the sender to a button
            var button = (Button) sender;

            //Find the buttons position in the array
            int column = Grid.GetColumn(button);
            int row = Grid.GetRow(button);

            int index = column + (row * 3);

            //Don't do anything if the cell is already filled
            if (mResults[index] != MarkType.Free)
                return;

            //Set the cell value based on which player's turn it is
            mResults[index] = mIsPlayerOne ? MarkType.Cross : MarkType.Nought;

            button.Content = mIsPlayerOne ? "X" : "O";

            //Change noughts to red
            if (!mIsPlayerOne)
                button.Foreground = Brushes.OrangeRed;

            //Toggle player turns
            mIsPlayerOne = !mIsPlayerOne;

            //Check for a winner
            WinningCondition();
        }


        
        private void WinningCondition()
        {
            //Winning combinations
            MarkType[][] combinations =
            {
                new[] {mResults[0], mResults[1], mResults[2]},
                new[] {mResults[3], mResults[4], mResults[5]},
                new[] {mResults[6], mResults[7], mResults[8]},
                new[] {mResults[0], mResults[3], mResults[6]},
                new[] {mResults[1], mResults[4], mResults[7]},
                new[] {mResults[2], mResults[5], mResults[8]},
                new[] {mResults[0], mResults[4], mResults[8]},
                new[] {mResults[6], mResults[4], mResults[2]}
            };


            //Buttons by name
            Dictionary<int, Button> buttons = new Dictionary<int, Button>()
            {
                [0] = Zero,
                [1] = One,
                [2] = Two,
                [3] = Three,
                [4] = Four,
                [5] = Five,
                [6] = Six,
                [7] = Seven,
                [8] = Eight
            };

            //Checks all combinations if there's a winning one           
            foreach (var combination in combinations)
            {
                if ((combination[0] & combination[1] & combination[2]) != MarkType.Free)
                    if ((combination[0] & combination[1] & combination[2]) == combination[0])
                    {
                        mGameEnded = true;
                        var cmb = combination;
 
                        
                        //TODO: Fix cell painting
                        //Θέλουμε να βρούμε στο combination που κέρδισε το index που έχει κάθε σύμβολο στη mResults,
                        //οπότε μετά, με βάση αυτό το index, θα βρούμε τα buttons από το Dictionary και θα τους αλλάξουμε το χρώμα.
                        //Κάτι λάθος παίζει όμως με τον κώδικα της foreach και μου βρίσκει μόνο ένα απ' τα 3 κουμπιά. Για βοηθήστε!!!
                        foreach (var mt in cmb)
                        {
                            var index = Array.IndexOf(mResults, mt);
                            if (buttons.ContainsKey(index))
                                buttons[index].Background = Brushes.Indigo;
                            
                            //buttons.First(x => x.Key == index).Value.Background = Brushes.Indigo;

                        }

                        break;

                    }

                if (!mResults.Any(result => result == MarkType.Free))
                {
                    //Game ended
                    mGameEnded = true;

                    //Turn all cells gray
                    Container.Children.Cast<Button>().ToList().ForEach(button =>
                    {
                        button.Background = Brushes.DimGray;
                    });
                }
            }
        }
    }
}
