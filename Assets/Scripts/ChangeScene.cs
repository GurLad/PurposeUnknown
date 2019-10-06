using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string Target;
    public bool ResetPlayerPos = false;
    public void Click()
    {
        if (ResetPlayerPos)
        {
            PlayerPrefs.SetFloat("PlayerXPos", -4);
            PlayerPrefs.SetFloat("PlayerZPos", 4);
        }
        SceneManager.LoadScene(Target);
    }
}
