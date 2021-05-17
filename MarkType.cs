using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// The type of value a cell in the game is currently at
/// </summary>

namespace TicTacToe
{
    public enum MarkType
    {
        /// <summary>
        /// Unclicked
        /// </summary>
        Free,
        /// <summary>
        /// The cell is an O
        /// </summary>
        Nought,
        /// <summary>
        /// The cell is an X
        /// </summary>
        Cross
    }
}
