using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMessage : MonoBehaviour
{
    [TextArea(3,10)]
    public string Message;
    private void OnTriggerEnter(Collider other)
    {
        MessageUI.Instance.Show(Message);
        Destroy(gameObject);
    }
}
