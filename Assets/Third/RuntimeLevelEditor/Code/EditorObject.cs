using System;
using UnityEngine;

namespace qiekn.learn_editor {
    public class EditorObject : MonoBehaviour {
        public enum ObjectType { Cylinder, Cube, Sphere, Player };

        [Serializable]
        public struct Data {
            public Vector3 pos;
            public Quaternion rot;
            public ObjectType objectType;
        }

        public Data data;
    }
}
