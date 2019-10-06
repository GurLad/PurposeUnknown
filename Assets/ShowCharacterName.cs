using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCharacterName : MonoBehaviour
{
    public TextMesh NameObject;
    private void Start()
    {
        if (PlayerPrefs.HasKey(GetComponent<OverworldEnemy>().ID))
        {
            string theName = GetComponent<OverworldEnemy>().Stats.Name;
            TextMesh newMesh = Instantiate(NameObject, transform);
            if (GetComponent<OverworldEnemy>().IsHuman)
            {
                newMesh.gameObject.transform.localPosition = new Vector3(0, 0, 4);
            }
            else
            {
                newMesh.gameObject.transform.localPosition = new Vector3(0, 0, 2);
                newMesh.gameObject.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
            }
            newMesh.gameObject.transform.eulerAngles = new Vector3(90, 0, 0);
            newMesh.text = theName;
            newMesh.gameObject.SetActive(true);
        }
    }
}
