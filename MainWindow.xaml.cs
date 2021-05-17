using System;
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
        private bool mIsPlayer1;
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
            mIsPlayer1 = true;

            //Iterate every button on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button => 
            {
                //Change background, foreground and content to default values
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Green;
            });

            //Make sure game has not ended
            mGameEnded = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
