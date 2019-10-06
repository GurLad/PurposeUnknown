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
    public string ID;
    private void Awake()
    {
        Transform item = transform;
        while (item != null)
        {
            ID = (int)item.position.x + "," + (int)item.position.z;
            item = item.parent;
        }
        Debug.Log(ID);
        if (PlayerPrefs.GetInt(ID) == 1)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!(PlayerPrefs.GetInt(ID) == 1))
        {
            Debug.Log("Collided with " + ID + ", is " + PlayerPrefs.GetInt(ID) + " completed");
            PlayerPrefs.SetInt("IsHuman", IsHuman ? 1 : 0);
            PlayerPrefs.SetInt("IsBoss", IsBoss ? 1 : 0);
            PlayerPrefs.SetString("EnemyStats", Stats.ToString());
            PlayerPrefs.SetString("EnemyID", ID);
            PlayerPrefs.SetString("EnemyWeapon", WeaponName);
            PlayerPrefs.SetInt(ID, PlayerPrefs.GetInt(ID, 0));
            PlayerPrefs.SetFloat("PlayerXPos", OverworldController.Instance.transform.position.x);
            PlayerPrefs.SetFloat("PlayerZPos", OverworldController.Instance.transform.position.z);
            SceneManager.LoadScene("Battle");
        }
    }
}
