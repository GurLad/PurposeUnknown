using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissedMessage : MonoBehaviour
{
    public float Rate;
    private float count;
    private TextMesh textMesh;
    private void Start()
    {
        textMesh = GetComponent<TextMesh>();
    }
    public void Show(string message = "Missed")
    {
        count = 1;
        textMesh.text = message;
    }
    private void Update()
    {
        textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, count);
        count -= Time.deltaTime * Rate;
    }
}
