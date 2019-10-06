using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldEnemy : MonoBehaviour
{
    public BattleStats Stats;
    private string id;
    private void Awake()
    {
        Transform item = transform;
        while (item != null)
        {
            id = (int)item.position.x + "," + (int)item.position.z;
            item = item.parent;
        }
        Debug.Log(id);
        if (PlayerPrefs.GetInt(id) == 1)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerPrefs.SetString("EnemyStats", Stats.ToString());
        PlayerPrefs.SetString("EnemyID", id);
        SceneManager.LoadScene("Battle");
    }
}
