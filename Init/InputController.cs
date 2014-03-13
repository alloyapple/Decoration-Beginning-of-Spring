///<summary>
///<para name = "Module"> InputController </para>
///<para name = "Describe"> InputController Framework, to control all the input. </para>
///<para name = "Author"> YS </para>
///<para name = "Date"> 2013-12-30 </para>
///</summary>
using DecorationSystem.Gestures;
using DecorationSystem.Keyboard;
using DecorationSystem.UI;

namespace DecorationSystem.Framework
{
    public class InputController
    {
        /// <summary>
        /// The _input controller
        /// </summary>
        static InputController _inputController;

        /// <summary>
        /// Instances this instance.
        /// </summary>
        /// <returns></returns>
        public static InputController Instance()
        {
            if (_inputController == null)
            {
                _inputController = new InputController();
            }
            return _inputController;
        }

        /// <summary>
        /// The _key
        /// </summary>
        public KeyboardController KeyControl { get; set; }

        /// <summary>
        /// The _gesture
        /// </summary>
        public GestureController GestureControl { get; set; }

        /// <summary>
        /// The _gui
        /// </summary>
        public GUIController GUIControl { get; set; }

        /// <summary>
        /// Prevents a default instance of the 
        /// <see cref="InputController"/> class from being created.
        /// </summary>
        InputController()
        {
            KeyControl = KeyboardController.Instance();
            GestureControl = GestureController.Instance();
            GUIControl = GUIController.Instance();
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            KeyControl.ListenKeyboard();
        }

    }
}
