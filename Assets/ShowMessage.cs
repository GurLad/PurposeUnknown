using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMessage : MonoBehaviour
{
    [TextArea(3,10)]
    public string Message;
    public bool IsScanner;
    private void Start()
    {
        if (IsScanner && PlayerPrefs.GetInt("GotScanner", 0) == 1)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerPrefs.SetInt("GotScanner", 1);
        MessageUI.Instance.Show(Message);
        Destroy(gameObject);
    }
}
