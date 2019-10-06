using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishMessage : MonoBehaviour
{
    public void Click()
    {
        Time.timeScale = 1;
        MessageUI.Instance.gameObject.SetActive(false);
    }
}
