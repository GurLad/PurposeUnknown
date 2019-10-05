using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameController;

public class InteractableUI : MonoBehaviour
{
    public static InteractableUI Instance { get; private set; }
    public GameObject AttackUI;
    public Button ScanButton;
    private void Awake()
    {
        Instance = this;
    }
    public void EnableButtons()
    {
        if (Game.EnemyAttack == null)
        {
            ScanButton.interactable = false;
        }
        else
        {
            ScanButton.interactable = true;
        }
        if (Game.PlayerAttacks.Count == 0)
        {
            AttackUI.SetActive(false);
        }
        else
        {
            AttackUI.SetActive(true);
        }
    }
}
