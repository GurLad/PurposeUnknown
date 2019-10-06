using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePanel : MonoBehaviour
{
    public GameObject Original;
    public GameObject Target;
    public void Click()
    {
        Original.SetActive(false);
        Target.SetActive(true);
    }
}
