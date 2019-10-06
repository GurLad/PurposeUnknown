using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldEnemy : MonoBehaviour
{
    public BattleStats Stats;
    public string WeaponName;
    public bool IsHuman;
    public bool IsBoss;
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
        if (!(PlayerPrefs.GetInt(id) == 1))
        {
            Debug.Log("Collided with " + id + ", is " + PlayerPrefs.GetInt(id) + " completed");
            PlayerPrefs.SetInt("IsHuman", IsHuman ? 1 : 0);
            PlayerPrefs.SetInt("IsBoss", IsBoss ? 1 : 0);
            PlayerPrefs.SetString("EnemyStats", Stats.ToString());
            PlayerPrefs.SetString("EnemyID", id);
            PlayerPrefs.SetString("EnemyWeapon", WeaponName);
            PlayerPrefs.SetFloat("PlayerXPos", OverworldController.Instance.transform.position.x);
            PlayerPrefs.SetFloat("PlayerZPos", OverworldController.Instance.transform.position.z);
            SceneManager.LoadScene("Battle");
        }
    }
}
