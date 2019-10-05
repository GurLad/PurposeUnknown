using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMusic : MonoBehaviour {
    public int musicID;
    void Start()
    {
        PlayerPrefs.SetInt("Music", musicID);
    }
}
