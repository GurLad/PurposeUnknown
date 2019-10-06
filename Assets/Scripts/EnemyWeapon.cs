using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public string Name;
    private void Start()
    {
        if (Name != PlayerPrefs.GetString("EnemyWeapon"))
        {
            Destroy(gameObject);
        }
    }
}
