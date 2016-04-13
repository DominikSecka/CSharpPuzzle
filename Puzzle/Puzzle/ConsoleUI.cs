using System;
using System.Text;
using Puzzle.Core;
using System.Text.RegularExpressions;
using System.Threading;

namespace Puzzle.CUI
{
    /// <summary>
    /// Console
    /// </summary>
    class ConsoleUI
    {
        //Playing field
        private Field field;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="field">Field object</param>
        public ConsoleUI(Field field)
        {
            this.field = field;
        }

        /// <summary>
        /// Start new game
        /// </summary>
        public void StartNewGame()
        {
            field.Generate();

            do
            {
                ProcessInput();
                throw new NotImplementedException("This method is not implemented yet.");
            } while (true);
        }

        /// <summary>
        /// Show current game state (delegated method)
        /// </summary>
        public void UpdateUI()
        {
            throw new NotImplementedException("This method is not implemented yet.");
        }

        /// <summary>
        /// Process user input
        /// </summary>
        private void ProcessInput()
        {
            throw new NotImplementedException("This method is not implemented yet.");
        }
    }
}
