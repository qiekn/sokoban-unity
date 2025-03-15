using System;
using UnityEngine;

namespace qiekn.learn_editor {
    public class Tile : MonoBehaviour {
        public enum Type { Shape1, Shape2, Shape3, Shape4 };

        [Serializable]
        public struct Data {
            public Vector3 pos;
            public Type type;
        }

        public Data data;
    }
}
