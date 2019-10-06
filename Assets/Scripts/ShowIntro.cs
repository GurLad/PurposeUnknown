using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowIntro : MonoBehaviour
{
    public List<string> Parts;
    public float Rate;
    public Text TheText;
    private int part;
    private float count;
    void Update()
    {
        count += Time.deltaTime;
        if (count >= Rate)
        {
            count = 0;
            TheText.text += Parts[part].Replace(@"\r\n", "\r\n");
            part++;
            if(part >= Parts.Count)
            {
                SceneManager.LoadScene("Overworld");
            }
        }
    }
}
