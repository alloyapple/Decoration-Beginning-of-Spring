///<summary>
///<para name = "Module">CommandManager</para>
///<para name = "Describe">Words</para>
///<para name = "Author">YS</para>
///<para name = "Date">20130902_1639</para>
///</summary>

using System;
using System.Collections;

namespace DecorationSystem.Undo {

    /// <summary>
    /// CommandManager is a central class for the Undo Framework.
    /// 
    /// Here's how it works:
	/// <para> 1. You declare a class that implements ICommand </para>
    /// 2. You create an instance of it and give it all necessary info that 
	/// 	it needs to know to apply or rollback a change
    /// 3. You call Commandmanager.RecordCommand(yourCommand)
    /// 
    /// Then you can also call Commandmanager.Undo() or Commandmanager.Redo()
    /// </summary>
    public class CommandManager {

        static CommandManager _commandmanager;
        public static CommandManager Instance() {
            if (_commandmanager ==null) {
                _commandmanager = new CommandManager();
            }
            return _commandmanager;
        }

        SimpleHistory _history;
        SimpleHistory History {
            get {
                return _history;
            }
            set {
                if (_history != null) {
                    _history.CollectionChanged -= RaiseUndoBufferChanged;
                }
                _history = value;
                if (_history != null) {
                    _history.CollectionChanged += RaiseUndoBufferChanged;
                }
            }
        }

        CommandManager() {
            History = new SimpleHistory();
            History.CollectionChanged +=RaiseUndoBufferChanged;
        }

        public CommandManager RecordCommand(ICommand existingCommand) {
            History.AppendCommand(existingCommand);
            return this;
        }

        public CommandManager Undo() {
            if (!CanUndo) return null;
            History.MoveBack();
            return this;
        }

        public CommandManager Redo() {
            if (!CanRedo) return null;
            History.MoveForward();
            return this;
        }

        public void Clear() {
            History.Clear();
        }

        public int Length {
            get { return History.Length; }
        }

        public bool CanUndo {
            get {
                return History.CanMoveBack;
            }
        }

        public bool CanRedo {
            get {
                return History.CanMoveForward;
            }
        }

        public event EventHandler CollectionChanged;
        protected void RaiseUndoBufferChanged(object sender, EventArgs e) {
            EventHandler handle = CollectionChanged;
            if (handle != null) {
                handle(this, e);
            }
        }

        public IEnumerable Pop() {
            return History.Pop();
        } 

    }
}

