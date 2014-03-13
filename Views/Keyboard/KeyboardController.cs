///<summary>
///<para name = "Module">KeyboardController</para>
///<para name = "Describe">Control the behaviour of the keyboard.</para>
///<para name = "Author">YS</para>
///<para name = "Date">2013-12-30</para>
///</summary>
using UnityEngine;
using System;
using System.Collections.Generic;
using DecorationSystem;

namespace DecorationSystem.Keyboard
{
    public class KeyboardController
    {

        /// <summary>
        /// The _input controller
        /// </summary>
        static KeyboardController _inputController;

        /// <summary>
        /// Instances this instance.
        /// </summary>
        /// <returns></returns>
        static public KeyboardController Instance()
        {
            if (_inputController == null)
            {
                _inputController = new KeyboardController();
            }
            return _inputController;
        }

        /// <summary>
        /// The _listeners
        /// </summary>
        static List<KeyboardListener> _listeners = new List<KeyboardListener>();

        /// <summary>
        /// Registers the listener.
        /// </summary>
        /// <param name="newListener">The new listener.</param>
        public void RegisterListener(KeyboardListener newListener)
        {
            _listeners.Add(newListener);
        }

        /// <summary>
        /// Unregister listener.
        /// </summary>
        /// <param name="listener">The listener.</param>
        public void UnRegisterListener(KeyboardListener listener)
        {
            _listeners.Remove(listener);
        }

        /// <summary>
        /// Listens the keyboard.
        /// </summary>
        public void ListenKeyboard()
        {
            _listeners.ForEach(item => item.Run());
        }
    }
}
