using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameScreeb : MonoBehaviour {

    [SerializeField]
    private GameObject originalButton;
	// Use this for initialization
	void Start ()
    {
        BaseObject[] objects = Resources.LoadAll<BaseObject>(Globals.isAttacking? "Units" : "Buildings");
        foreach(var obj in objects)
        {
            GameObject clone = Instantiate(originalButton);
            clone.transform.SetParent(originalButton.transform.parent, false);
            clone.GetComponentInChildren<Text>().text = obj.name;
        }

        originalButton.SetActive(false);
        GetComponentInChildren<Toggle>().isOn = true;
	}

    public void OnToggle(Toggle current)
    {
        //print("OnToggle" + (current ? "true" : "False"));
        if (current.isOn)
        {
            //Get the name from the button and store it somewhere
            Globals.objectToSpawn = current.GetComponentInChildren<Text>().text;
        }
    }

    public void OnExitButton()
    {
        if (!Globals.isAttacking)
        {
            WorldManager.instance.SaveLevel();
        }
        SceneManager.LoadScene(0);
    }
}
