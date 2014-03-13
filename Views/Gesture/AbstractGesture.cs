///<summary>
///<para name = "Module">Name</para>
///<para name = "Describe">Words</para>
///<para name = "Author">htoo</para>
///<para name = "Date">20130902_1639</para>
///</summary>

using UnityEngine;
using DecorationSystem.Framework;
using DecorationSystem.Undo;

namespace DecorationSystem.Gestures {

    public abstract class AbstractGesture : IGesture {

        static protected GestureController _gc;
        protected AbstractGesture() {
            if (_gc == null) {
                _gc = InputController.Instance().GestureControl;
            }
        }

        public void Register() {
            RegisterCore();
        }

        public void Unregister() {
            UnregisterCore();
        }

        protected abstract void RegisterCore();

        protected abstract void UnregisterCore();

        protected abstract ICommand CreateCommand();

        protected Camera RaycastCamera {
            get { return GlobalManager.CurrentCamera; }
            set { GlobalManager.CurrentCamera = value; }
        }

        protected GameObject Target {
            get { return GlobalManager.TempGameObject; }
            set { }
        }

        protected bool IsTargetNull() {
            return Target == null ? true : false;
        }

        protected void RecordChange() {
            //record cmd
            var cmd = CreateCommand();
            CommandManager.Instance().RecordCommand(cmd);
            cmd.Execute();
            GlobalManager.SetActiveTempGameObjectToFalse();

            //record gameobject
            GlobalManager.MarkChanged(GlobalManager.Selection);

            //set gestureRunning
            GestureRunning = false;
        }

        protected bool GestureRunning = false;
        protected Transform TargetTransform;

        protected void StartChange() {
            GlobalManager.SetActiveTempGameObjectToTrue();
            TargetTransform = Target.transform;
        }
    }
}