///<summary>
///<para name = "Module">TweenDistance</para>
///<para name = "Describe">Measure the distance between two points.</para>
///<para name = "Author">YS</para>
///<para name = "Date">2014-1-1</para>
///</summary>

using UnityEngine;
using System;
using System.Collections;
using DecorationSystem;
using DecorationSystem.Gestures;
using DecorationSystem.Undo;
using DecorationSystem.Framework;

using Vectrosity;

namespace DecorationSystem.Gestures
{
    public class TweenDistance : AbstractGesture
    {
        VectorLine line;
        Vector3[] pointsOfLine = new Vector3[2];
        int index = 0;
        float distance = 0.0f;

        TweenDistance() : base()
        {
        }

        protected override void RegisterCore()
        {
            _gc.OnTapHandle += Tap;

            if (line == null)
                CreateLine();
            //set line renderer camera
            VectorLine.SetCamera3D(GlobalManager.CurrentCamera);
        }

        protected override void UnregisterCore()
        {
            _gc.OnTapHandle -= Tap;

            if (line != null)
                DestroyLine();
        }

        protected override ICommand CreateCommand()
        {
            throw new NotImplementedException();
        }

        void Tap(Gesture gesture)
        {
            if (!GlobalManager.MeasureFunctionSwitch)
            {
                return;
            }

            if (!gesture.Selection)
                return;

            // draw line
            DrawLine(gesture.Hit.point);
        }

        void DrawLine(Vector3 pos)
        {
            pointsOfLine [index++] = pos;
            if (index >= pointsOfLine.Length)
            {
                index = 0;

                line.Draw3D();

                CalculateDistance();

                OnFinishMeasureRaised();
            }
        }

        public void CreateLine()
        {
            // set line
            string name = "line";
            Color color = Color.red;
            Material material = null;
//            Texture texture = 
//            material.shader = Shader.Find("Diffuse");
//            material.SetTexture("_MainTex", texture);

            float width = 5.0f;
            if (line == null)
                line = new VectorLine(name, pointsOfLine, color, material, width);
        }

        public void DestroyLine()
        {
            VectorLine.Destroy(ref line);
            InitLine();
        }

        void InitLine()
        {
            pointsOfLine = new Vector3[2];
            distance = 0.0f;
        }

        void CalculateDistance()
        {
            distance = Vector3.Distance(pointsOfLine [0], pointsOfLine [1]);
        }

        public float GetDistance()
        {
            return distance;
        }

        public event Action OnFinishMeasure;

        void OnFinishMeasureRaised()
        {
            Action handle = OnFinishMeasure;
            if (handle != null)
                handle();
        }

        static TweenDistance singleton;

        static public TweenDistance Instance()
        {
            if (singleton == null)
            {
                singleton = new TweenDistance();
            }

            return singleton;
        }
    }
}
