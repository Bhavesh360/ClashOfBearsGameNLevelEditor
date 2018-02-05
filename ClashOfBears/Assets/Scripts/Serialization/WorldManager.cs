using UnityEditor;
using UnityEngine;

public class WorldManager : Singleton<WorldManager>
{

    private void Start()
    {
        string curWorld = PlayerPrefs.GetString(Globals.playerPrefsWorldVariable);
        SetData(curWorld);
    }

    private string GetData()
    {
        WorldData world = new WorldData();
        GameObject[] objectsInScene = GameObject.FindGameObjectsWithTag("WorldObject");
        //WorldObject[] objectsInScene = FindObjectsOfType<WorldObject>();
        foreach (var obj in objectsInScene)
        {
            ObjectData data = new ObjectData();
            data.name = cleanName(obj.name);
            data.position = obj.transform.position;
            data.rotation = obj.transform.rotation;
            data.scale = obj.transform.lossyScale;

            world.objects.Add(data);
        }
        string jsonWorld = JsonUtility.ToJson(world);
        return jsonWorld;
    }

    private string cleanName(string name)
    {
        int i = name.IndexOf("(");
        string str = name.Trim();
        if(i >= 0)
            str = name.Substring(0, i).Trim();
        return str;
    }

    private void SetData(string jsonWorld)
    {
        GameObject[] objectsInScene = GameObject.FindGameObjectsWithTag("WorldObject");
        foreach (var obj in objectsInScene)
            Undo.DestroyObjectImmediate(obj);

        if (jsonWorld.Length <= 0)
            return;

        WorldData world = JsonUtility.FromJson<WorldData>(jsonWorld);
        foreach (var data in world.objects)
        {
            BaseObject prefab = Resources.Load<BaseObject>("Buildings/"+ data.name);
            BaseObject clone = Instantiate(prefab);
            Undo.RegisterCreatedObjectUndo(clone.gameObject, "LevelLoaded");

            clone.transform.position = data.position;
            clone.transform.rotation = data.rotation;
            clone.transform.localScale = data.scale;

            clone.name = data.name;
        }
    }

    public void SaveLevel()
    {
        string curWorld = GetData();
        PlayerPrefs.SetString(Globals.playerPrefsWorldVariable, curWorld);
    }

    public override void SingletonAwake() { }
}
