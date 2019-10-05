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
    private void Start()
    {
        EnableButtons();
    }
    public void EnableButtons()
    {
        if (Game.EnemyAttack == null || Game.PlayerAttacks.Contains(Game.EnemyAttack))
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
            foreach (AttackButton item in AttackUI.GetComponentsInChildren<AttackButton>())
            {
                if (item.TheAttack.EnergyCost <= Game.Player.Energy)
                {
                    item.gameObject.GetComponent<Button>().interactable = true;
                }
                else
                {
                    item.gameObject.GetComponent<Button>().interactable = false;
                }
            }
        }
    }
}
