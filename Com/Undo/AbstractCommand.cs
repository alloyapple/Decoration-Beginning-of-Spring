///<summary>
///<para name = "Module">Name</para>
///<para name = "Describe">Words</para>
///<para name = "Author">htoo</para>
///<para name = "Date">20130902_1639</para>
///</summary>

using UnityEngine;
using System;
using System.Collections;
using DecorationSystem;
using DecorationSystem.Save;

namespace DecorationSystem.Undo {

    public abstract class AbstractCommand : ICommand {

        public AbstractCommand(bool canExecute, bool canUnExecute) {
            CanExecute = canExecute;
            CanUnExecute = canUnExecute;
        }

        public AbstractCommand()
            : this(true, true) {
        }

        public virtual void Execute() {
            if (!CanExecute) {
                return;
            }
            ExecuteCore();
        }

        public bool CanExecute { get; set; }

        /// <summary>
        /// Override execute core to provide your logic that actually performs the command.
        /// </summary>
        protected abstract void ExecuteCore();

        public virtual void UnExecute() {
            if (!CanUnExecute) {
                return;
            }
            UnExecuteCore();
        }

        public bool CanUnExecute { get; set; }

        /// <summary>
        /// Override this to provide the logic that undoes the command.
        /// </summary>
        protected abstract void UnExecuteCore();
    }
}