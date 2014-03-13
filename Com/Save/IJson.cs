///<summary>
///<para name = "Module">Name</para>
///<para name = "Describe">Words</para>
///<para name = "Author">htoo</para>
///<para name = "Date">20130902_1639</para>
///</summary>

using UnityEngine;

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Reflection;

using DecorationSystem;
using DecorationSystem.Undo;
using DecorationSystem.MiniJSON;

namespace DecorationSystem.Save {

    public interface ISave {
        IState GetState(GameObject go);
        void RecoveryState(IState state);
    }

    public interface IState {
        GameObject GetTarget();
        string Name { get; set; }
        Dictionary<string, System.Object> ToDic();
        void FromDic(Dictionary<string, System.Object> value);
        void Print();
    }

    public class TransFormSave : ISave {
        static ISave _save;
        public static ISave Instance() {
            if (_save == null) {
                _save = new TransFormSave();
            }
            return _save;
        }

        public IState GetState(GameObject go) {
            var tfmState = new TransformState(go);
            return tfmState;
        }

        public void RecoveryState(IState state){
            GameObject go = state.GetTarget();
            Transform tfm = go.transform;
            var dic = state.ToDic();
          
            Converter<System.Object,float> doubleTofloat = i =>{
                double temp = (double)i;
                return (float)temp;
            };

            /// dic to value
            ////////////////////////////////////////////////////////////////////////////////
            foreach (var item in dic) {
                PropertyInfo property = tfm.GetType().GetProperty(item.Key);
                List<System.Object> temp= item.Value as List<System.Object>;
                List<float> value = temp.ConvertAll(doubleTofloat);

                switch (item.Key) {
                    case "position": {
                            Vector3 pos = new Vector3(value[0], value[1], value[2]);
                            property.SetValue(tfm, pos, null);
                            break;
                    }
                    case "rotation": {
                            Quaternion rotation = new Quaternion(value[0], value[1], value[2], value[3]);
                            property.SetValue(tfm, rotation, null);
                            break;
                    }
                    case "localScale": {
                            Vector3 scale = new Vector3(value[0], value[1], value[2]);
                            property.SetValue(tfm, scale, null);
                            break;
                    }
                }
            }
            ////////////////////////////////////////////////////////////////////////////////
        }

    }

    public class TransformState : IState {
        GameObject _go;
        Dictionary<string, System.Object> _dic;

        public TransformState(GameObject go) {
            _go = go;
            this.Name = _go.name;
        }

        public TransformState(string name) {
            this.Name = name;
            _go = GameObject.Find(this.Name) as GameObject;
        }

        public GameObject GetTarget() {
            return _go;
        }

        public string Name { get; set; }

        public Dictionary<string, System.Object> ToDic() {

            if (_dic == null) {
                _dic = new Dictionary<string, System.Object>();
            }
            else {
                return _dic;
            }

            // value to dic
            ////////////////////////////////////////////////

            Transform tfm = _go.transform;

            _dic["position"] = new List<float>(){
                tfm.position.x,
                tfm.position.y,
                tfm.position.z};

            _dic["rotation"] = new List<float>(){
                tfm.rotation.x* 1.0f,
                tfm.rotation.y * 1.0f,
                tfm.rotation.z * 1.0f,
                tfm.rotation.w * 1.0f};

            _dic["localScale"] = new List<float>(){
                tfm.localScale.x,
                tfm.localScale.y,
                tfm.localScale.z};

            ////////////////////////////////////////////////

            return _dic;
        }

        public void FromDic(Dictionary<string, System.Object> value) {
            _dic = value;
        }

        public void Print() {
            Debug.Log(Name);
            foreach (var item in _dic) {
                List<float> value = item.Value as List<float>;
                Debug.Log("Key:["+item.Key +"]\t Value:[" + value + "]");
            }
        }
    }

    public interface IMKL {
        IMKL ToText(out string value);
        IMKL FromString(string value);
        IMKL To(out Dictionary<string, System.Object> dic);
        IMKL From(Dictionary<string, System.Object> value);
    }

    public class JsonFormat : IMKL {

        Dictionary<string, System.Object> _dic;
        
        public IMKL ToText(out string value) {
            value = Json.Serialize(_dic);
            return this;
        }

        public IMKL FromString(string json) {
            _dic = Json.Deserialize(json) as Dictionary<string,System.Object>;
            return this;
        }

        public IMKL To(out Dictionary<string, System.Object> dic) {
            dic = _dic;
            return this;
        }

        public IMKL From(Dictionary<string, System.Object> value) {
            this._dic = value;
            return this;
        }
    }

    public interface IFile {
        void Write(Dictionary<string,System.Object> value);
        IMKL Read();
    }

    public class TxtFile : IFile {
        FileInfo _fp;
        IMKL _fmt;

        public TxtFile(string path, IMKL fmt) {
            _fp = new FileInfo(Application.dataPath + "/"+path);
            _fmt = fmt;
        }

        public void Write(Dictionary<string, System.Object> value) {
            if (_fp.Exists) {
                _fp.Delete();
            }

            string context;
            _fmt.From(value).ToText(out context);

            using (var sw = _fp.CreateText()) {
                sw.Write(context);
            }
        }

        public IMKL Read() {
            if (!_fp.Exists) throw new System.IO.FileNotFoundException();

            string context;
            using (var sr = _fp.OpenText()) {
                context = sr.ReadToEnd();
            }

            IMKL fmt = _fmt.FromString(context);
            return fmt;
        }
    }

    public class SaveManager {

        static SaveManager _save;
        public static SaveManager Instance() {
            if (_save == null) {
                _save = new SaveManager();
            }
            return _save;
        }
        
        List<GameObject> _gos = new List<GameObject>();

        List<IState> _states = new List<IState>();

        Dictionary<string, System.Object> _dic;

        void DicToList() {
            _states.Clear();

            Debug.Log(Json.Serialize(_dic));

            foreach (var item in _dic) {
                IState state = new TransformState(item.Key);

                Dictionary<string, System.Object> dic = item.Value as Dictionary<string, System.Object>;

                state.FromDic(dic);

                _states.Add(state);
            }
        }

        void ListToDic() {
            if (_dic != null) {
                _dic.Clear();
            }
            else {
                _dic = new Dictionary<string, System.Object>();
            }

            _states.ForEach(item => {
                _dic[item.Name] = item.ToDic();
            });
        }

        public void SaveFile(IFile file) {
            file.Write(_dic);
        }

        public void LoadFile(IFile file) {
            file.Read().To(out _dic);
        }

        public void SetSnapsTarget(GameObject go) {
            if (_gos.Contains(go)) return; 
            _gos.Add(go);
        }

        public void ClearSnapsTarget(GameObject go) {
            _gos.Remove(go);
        }

        public void RegisterSnapshot() {
            _gos.ForEach(item => {
                var state = TransFormSave.Instance().GetState(item);
                _states.Add(state);
            } );

            ListToDic();
        }

        public void PerformSnapshot() {
            DicToList();

            _states.ForEach(item => {
                TransFormSave.Instance().RecoveryState(item);
                SetSnapsTarget(item.GetTarget());
            });

            Clear();
        }

        void Clear() {
            _states.Clear();
            _dic.Clear();
        }
    }
}
