///<summary>
///<para name = "Module">KeyboardListener</para>
///<para name = "Describe">Listen to the keyboard for checking key down.</para>
///<para name = "Author">YS</para>
///<para name = "Date">2013-12-30</para>
///</summary>
using UnityEngine;
using System;
using System.Collections;
using DecorationSystem;
using DecorationSystem.CommonUtilities;

namespace DecorationSystem.Keyboard
{
    public class KeyboardListener
    {
        public static KeyboardListener Create()
        {
            return new KeyboardListener(KeyboardController.Instance());
        }

        KeyboardController _inputController;
        public KeyboardListener(KeyboardController input)
        {
            this._inputController = input;
        }

        Action _action;
        public event Action Listener
        {
            add { _action += value; } remove { _action -= value; }
        }

        public KeyCode Key { get; set; }

        public KeyboardListener SetKey(KeyCode key)
        {
            this.Key = key;
            return this;
        }

        public KeyboardListener SetAction(Action action)
        {
            this.Listener += action;
            return this;
        }

        public void Register()
        {
            this._inputController.RegisterListener(this);
        }

        public void Unregister()
        {
            this._inputController.UnRegisterListener(this);
        }

        public void Run()
        {
            if (IsKeyAndAction())
            {
                this._action.ActionIf(() => Input.GetKey(Key));
            }
        }

        bool IsKeyAndAction()
        {
            return this.Key != KeyCode.None && this._action != null;
        }

    }
}
