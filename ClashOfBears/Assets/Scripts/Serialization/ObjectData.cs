using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectData
{
    public string name;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
}

[System.Serializable]
public class WorldData
{
    public List<ObjectData> objects = new List<ObjectData>();
}