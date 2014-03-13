///<summary>
///<para name = "Module">SetPropertyCmd</para>
///<para name = "Describe">This is a simple command that can change any property on any object</para>
///<para name = "Author">YS</para>
///<para name = "Date"> 2014-2-19 </para>
///</summary>
using UnityEngine;
using System.Reflection;
using DecorationSystem.Save;

namespace DecorationSystem.Undo
{

    /// <summary>
    /// This is a simple command that can change any property on any object
    /// It can also undo what it did
    /// </summary>
    public class SetPropertyCmd<TParent, TValue> : AbstractCommand
    {
        public SetPropertyCmd(TParent parentObject, string propertyName, TValue value)
            : base(true, true)
        {
            ParentObject = parentObject;
            Property = parentObject.GetType().GetProperty(propertyName);
            Value = value;
        }

        public TParent ParentObject { get; set; }

        public PropertyInfo Property { get; set; }

        public TValue Value { get; set; }

        public TValue OldValue { get; set; }

        protected override void ExecuteCore()
        {
            OldValue = (TValue)Property.GetValue(ParentObject, null);
            Property.SetValue(ParentObject, Value, null);
        }

        protected override void UnExecuteCore()
        {
            Property.SetValue(ParentObject, OldValue, null);
        }
    }

    /// <summary>
    /// This is a sample command that can invoke any function on any object
    /// It can undo depend on what it did
    /// </summary>
    public class CallFunctionCmd<TParent, TValue> : AbstractCommand
    {
        public CallFunctionCmd(TParent parentObject, string functionName, TValue value, bool canUnExecute)
            : base(true, canUnExecute)
        {
            ParentObject = parentObject;
            Method = parentObject.GetType().GetMethod(functionName);
            Value = value;
        }

        public TParent ParentObject { get; set; }

        public MethodInfo Method { get; set; }

        public TValue Value { get; set; }

        public virtual TValue OldValue { get; set; }

        protected override void ExecuteCore()
        {
            Method.Invoke(ParentObject, new object[] { Value });
        }

        protected override void UnExecuteCore()
        {
            Method.Invoke(ParentObject, new object[] { OldValue });
        }
    }

    /// <summary>
    /// This is a class that can set position of game object.
    /// It can undo get old value.
    /// </summary>
    public class SetPositionCmd : SetPropertyCmd<Transform, Vector3>
    {
        public SetPositionCmd(Transform parentObject, Vector3 value)
            : base(parentObject, "position", value)
        {
        }
    }

    /// <summary>
    /// This is a class that can set rotation of game object
    /// It can undo get old value.
    /// </summary>
    public class SetRotaionCmd : SetPropertyCmd<Transform, Quaternion>
    {
        public SetRotaionCmd(Transform parentObject, Quaternion value)
            : base(parentObject, "rotation", value)
        {
        }
    }

    /// <summary>
    /// This is a class that can set scale of game object
    /// It can undo get old value.
    /// </summary>
    public class SetScaleCmd : SetPropertyCmd<Transform, Vector3>
    {
        public SetScaleCmd(Transform parentObject, Vector3 value)
            : base(parentObject, "localScale", value)
        {
        }
    }

    /// <summary>
    /// This is a class that can set setActive of game object
    /// It can undo get old value.
    /// </summary>
    public class SetVisibleCmd : CallFunctionCmd<GameObject, bool>
    {
        public SetVisibleCmd(GameObject parentObject, bool value)
            : base(parentObject, "SetActive", value, true)
        {
        }

        public override bool OldValue { get { return false; } }
    }
}

