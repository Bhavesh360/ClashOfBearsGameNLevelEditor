using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    public void AttackButton()
    {
        Globals.isAttacking = true;
        SceneManager.LoadScene("GameScene");
    }

    public void BuildButton()
    {
        Globals.isAttacking = false;
        SceneManager.LoadScene("GameScene");
    }

    public void ClearButton()
    {
        PlayerPrefs.SetString(Globals.playerPrefsWorldVariable, "");
    }
}
