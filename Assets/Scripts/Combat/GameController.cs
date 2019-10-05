using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum WaitMode { None, WaitForFinishTool, WaitForFinishPlayerAttack, WaitForFinishWithdrawingTool, WaitForEnemyAttack, WaitForFinishScan }

public class GameController : MonoBehaviour
{
    public static GameController Game;
    public BattleStats Player;
    public BattleStats Enemy;
    [HideInInspector]
    public List<Attack> PlayerAttacks;
    [HideInInspector]
    public List<Attack> EnemyAttacks;
    public List<Attack> PlayerAvailableAttacks
    {
        get
        {
            return PlayerAttacks.FindAll(a => a.EnergyCost <= Player.Energy);
        }
    }
    public List<Attack> EnemyAvailableAttacks
    {
        get
        {
            return EnemyAttacks.FindAll(a => a.EnergyCost <= Enemy.Energy);
        }
    }
    [HideInInspector]
    public Attack PlayerAttack;
    [HideInInspector]
    public Attack EnemyAttack;
    [HideInInspector]
    public WaitMode TheWaitMode = WaitMode.None;
    private void Awake()
    {
        Game = this;
    }
    private void Start()
    {
        //TODO: Load enemy and player attacks
        PlayerAttacks = AllAttacks.Instance.Attacks.FindAll(a => Player.Attacks.Contains(a.Name));
        EnemyAttacks = AllAttacks.Instance.Attacks.FindAll(a => Enemy.Attacks.Contains(a.Name));
        EnemyAttack = null;
    }
    private void Update()
    {
        switch (TheWaitMode)
        {
            case WaitMode.None:
                break;
            case WaitMode.WaitForFinishTool:
                if (!PlayerAttack.ToolAnimation.Active)
                {
                    PlayerAttack.Launch(Player, Enemy);
                    TheWaitMode = WaitMode.WaitForFinishPlayerAttack;
                }
                break;
            case WaitMode.WaitForFinishPlayerAttack:
                if (!PlayerAttack.AttackAnimation.Active)
                {
                    PlayerAttack.ToolAnimation.Reverse();
                    PlayerAttack.ToolAnimation.StartAnimations();
                    TheWaitMode = WaitMode.WaitForFinishWithdrawingTool;
                }
                break;
            case WaitMode.WaitForFinishWithdrawingTool:
                if (!PlayerAttack.ToolAnimation.Active)
                {
                    EndPlayerTurn();
                }
                break;
            case WaitMode.WaitForEnemyAttack:
                if (!EnemyAttack.AttackAnimation.Active)
                {
                    EndEnemyTurn();
                }
                break;
            case WaitMode.WaitForFinishScan:
                if (!Player.ScanAnimation.Active)
                {
                    foreach (AttackButton item in InteractableUI.Instance.AttackUI.GetComponentsInChildren<AttackButton>())
                    {
                        Destroy(item);
                    }
                    DisplayAttacks.Instance.Display();
                    EndPlayerTurn();
                }
                break;
            default:
                break;
        }
    }
    public void StartPlayerTurn()
    {
        InteractableUI.Instance.GetComponent<RectTransform>().localScale = Vector3.zero;
    }
    public void EndPlayerTurn()
    {
        if (TheWaitMode == WaitMode.WaitForFinishWithdrawingTool)
        { 
            PlayerAttack.ToolAnimation.Reverse();
        }
        Player.IdleAnimation.StartAnimations(true);
        //Enemy do stuff
        GetEnemyAttack();
        if (EnemyAttack != null)
        {
            EnemyAttack.Launch(Enemy, Player);
            TheWaitMode = WaitMode.WaitForEnemyAttack;
        }
        else
        {
            Enemy.Energy += 5;
            Game.Enemy.Missed.Show("Waited");
            EndEnemyTurn();
        }
    }
    private void EndEnemyTurn()
    {
        Enemy.IdleAnimation.StartAnimations(true);
        InteractableUI.Instance.GetComponent<RectTransform>().localScale = Vector3.one;
        TheWaitMode = WaitMode.None;
        InteractableUI.Instance.EnableButtons();
    }
    private void GetEnemyAttack()
    {
        //A very complicated and smart enemy AI
        if (EnemyAvailableAttacks.Count > 0)
        {
            EnemyAttack = EnemyAvailableAttacks[Random.Range(0, Enemy.Attacks.Count)];
        }
        else
        {
            EnemyAttack = null;
        }
    }
}
