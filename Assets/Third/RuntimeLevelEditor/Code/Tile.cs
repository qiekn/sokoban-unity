using System;
using UnityEngine;


public class Tile : MonoBehaviour {
    public enum Type { Wall, Box, Player, Point };

    [Serializable]
    public struct Data {
        public Vector3 pos;
        public Type type;
    }

    public Data data;
}
