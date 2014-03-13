///<summary>
///<para name = "Module">Name</para>
///<para name = "Describe">Words</para>
///<para name = "Author">htoo</para>
///<para name = "Date">20130902_1639</para>
///</summary>

namespace DecorationSystem.Undo {

    /// <summary>
    /// Encapsulates a user command (actually two actions: Do and Undo)
    /// Can be anything.
    /// You can give your implementation any information it needs to be able to
    /// execute and rollback what it needs.
    /// </summary>
    public interface ICommand {

        /// <summary>
        /// Gets or sets a value indicating whether this instance can execute.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can execute; otherwise, <c>false</c>.
        /// </value>
        bool CanExecute{get;set;}

        /// <summary>
        /// Gets or sets a value indicating whether this instance can un execute.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can un execute; otherwise, <c>false</c>.
        /// </value>
        bool CanUnExecute{get;set;}

        /// <summary>
        /// Apply changes encapsulated by this object.
        /// </summary>
        void Execute();

        /// <summary>
        /// Undo changes made by a previous Execute call.
        /// </summary>
        void UnExecute();

    }
}

