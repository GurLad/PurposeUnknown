using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetPlayerPrefs : MonoBehaviour
{
    public void Click()
    {
        PlayerPrefs.DeleteAll();
        Destroy(keepMusic.Instance.gameObject);
        SceneManager.LoadScene("Menu");
    }
}
