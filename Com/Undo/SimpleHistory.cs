///<summary>
///<para name = "Module">Name</para>
///<para name = "Describe">Words</para>
///<para name = "Author">htoo</para>
///<para name = "Date">20130902_1639</para>
///</summary>

using System;
using System.Collections;

namespace DecorationSystem.Undo {

    /// <summary>
    /// This class implements a usual, linear command sequence. You can move back and forth
    /// changing the state of the respective document. When you move forward, you execute
    /// a respective command, when you move backward, you Undo it (UnExecute).
    /// Implemented through a double linked-list of SimpleHistoryNode objects.
    /// </summary>
    public class SimpleHistory {

        public SimpleHistory() {
            Init();
        }

        /// <summary>
        /// Adds a new command to the tail after current state. If 
        /// there exist more command after this, they're lost (Garbage Collected).
        /// This is the only method of this class that actually modifies the linked-list.
        /// </summary>
        /// <param name="newCommand">Action to be added.</param>
        public void AppendCommand(ICommand newCommand) {

            CurrentState.NextCommand = newCommand;
            CurrentState.NextNode = new SimpleHistoryNode(newCommand, CurrentState);

            CurrentState = CurrentState.NextNode;
            LastNode = CurrentState;

            RaiseUndoBufferChanged();

            Length += 1;
        }

        /// <summary>
        /// All existing Nodes and Actions are garbage collected.
        /// </summary>
        public void Clear() {
            Init();
            RaiseUndoBufferChanged();
        }

        public void MoveBack() {
            if (!CanMoveBack) return;
            if (CurrentState.PreviousCommand.CanUnExecute) CurrentState.PreviousCommand.UnExecute(); 

            CurrentState = CurrentState.PreviousNode;

            RaiseUndoBufferChanged();
            Length -= 1;
        }

        public void MoveForward() {
            if (!CanMoveForward) return;
            if (CurrentState.NextCommand.CanExecute) CurrentState.NextCommand.Execute();

            CurrentState = CurrentState.NextNode;

            RaiseUndoBufferChanged();
            Length += 1;
        }

        public bool CanMoveBack {
            get { return CurrentState.PreviousCommand != null && CurrentState.PreviousNode != null; }
        }

        public bool CanMoveForward {
            get { return CurrentState.NextCommand != null && CurrentState.NextNode != null; }
        }

        public int Length { get; set; }

        private SimpleHistoryNode _currentState = new SimpleHistoryNode();

        public SimpleHistoryNode CurrentState {
            get { return _currentState; }
            set {
                if (value != null) {
                    _currentState = value;
                }
                else {
                    throw new ArgumentNullException("CurrentState");
                }
            }
        }

        public SimpleHistoryNode HeadNode { get; set; }
        public SimpleHistoryNode LastNode { get; set; }

        public event EventHandler CollectionChanged;
        protected void RaiseUndoBufferChanged() {
            if (CollectionChanged != null) {
                CollectionChanged(this, new EventArgs());
            }
        }

        void Init() {
            CurrentState = new SimpleHistoryNode();
            HeadNode = CurrentState;
            LastNode = CurrentState;
            Length = 0;
        }

        public IEnumerable Pop() {
            SimpleHistoryNode current = HeadNode;
            while (current != null && current != CurrentState && current.NextCommand != null) {
                yield return current.NextCommand;
                current = current.NextNode;
            }
        }
    }

    /// <summary>
    /// Represents a node of the doubly linked-list SimpleHistory
    /// (StateX in the following diagram:)

    /// (State0) --- [Command0] --- (State1) --- [Command1] --- (State2)

    /// StateX (e.g. State1) has a link to the previous State, previous Command,
    /// next State and next Command.
    /// As you move from State1 to State2, an Command1 is executed (Redo).
    /// As you move from State1 to State0, an Command0 is un-executed (Undo).
    /// </summary>
    public class SimpleHistoryNode {

        public SimpleHistoryNode(ICommand lastExisingCommand, SimpleHistoryNode lastExisingState) {
            PreviousCommand = lastExisingCommand;
            PreviousNode = lastExisingState;
        }

        public SimpleHistoryNode() {
        }

        public ICommand PreviousCommand { get; set; }
        public ICommand NextCommand { get; set; }
        public SimpleHistoryNode PreviousNode { get; set; }
        public SimpleHistoryNode NextNode { get; set; }

    }
}