using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Client.Commands
{
    public class CommandInvoker
    {
        private static CommandInvoker _instance;
        public static CommandInvoker Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CommandInvoker();
                }
                return _instance;
            }
        }

        private CommandInvoker() { }

        private List<ICommand> _allCommands = new List<ICommand>();
        private Stack<IUndoableCommand> _undoableCommands = new Stack<IUndoableCommand>();

        public void Run(ICommand command)
        {
            bool wasSuccessful = command.Execute();
            if(wasSuccessful)
            {
                _allCommands.Add(command);

                if (command is IUndoableCommand undoableCommand)
                {
                    _undoableCommands.Push(undoableCommand);
                }
            }
        }

        /// <returns>if successful</returns>
        public bool TryUndo()
        {
            if(_undoableCommands.Count <= 0)
            {
                return false;
            }

            _undoableCommands.Pop().Undo();
            return true;
        }

        public void ClearUndoCommands()
        {
            _undoableCommands.Clear();
        }
    }
}
