using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageUI : MonoBehaviour
{
    public static MessageUI Instance;
    public Text MessageText;
    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }
    public void Show(string message)
    {
        MessageText.text = message;
        Time.timeScale = 0;
        gameObject.SetActive(true);
    }
}
