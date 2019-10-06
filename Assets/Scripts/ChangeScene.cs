using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string Target;
    public bool ResetPlayerPos = false;
    public bool IsIntro;
    public void Click()
    {
        if (ResetPlayerPos)
        {
            PlayerPrefs.SetFloat("PlayerXPos", -4);
            PlayerPrefs.SetFloat("PlayerZPos", 4);
        }
        if (IsIntro && PlayerPrefs.GetInt("GotScanner", 0) != 1)
        {
            SceneManager.LoadScene("Intro");
        }
        else
        {
            SceneManager.LoadScene(Target);
        }
    }
}
