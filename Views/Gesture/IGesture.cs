using UnityEngine;
using DecorationSystem.Framework;

namespace DecorationSystem.Gestures {

    public interface IGesture {
        void Register();
        void Unregister();
    }

    public interface IEnable {
        void Enable();
        void Disable();
    }

    public interface IAppliable {
        void Apply();
        void Cancel();
    }

    public interface IOpen {
        void Open();
        void Close();
    }

    public interface IActive {
        void Active();
        void Deactive();
    }
}