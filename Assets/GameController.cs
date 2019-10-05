using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Game;
    public BattleStats Player;
    public BattleStats Enemy;
    [HideInInspector]
    public List<Attack> PlayerAttacks;
    [HideInInspector]
    public List<Attack> EnemyAttacks;
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
        InteractableUI.Instance.EnableButtons();
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
                    PlayerAttack.ToolAnimation.Reverse();
                    Player.IdleAnimation.StartAnimations(true);
                    //Enemy do stuff
                    EnemyAttack = EnemyAttacks[Random.Range(0, Enemy.Attacks.Count)];
                    EnemyAttack.Launch(Enemy, Player);
                    TheWaitMode = WaitMode.WaitForEnemyAttack;
                }
                break;
            case WaitMode.WaitForEnemyAttack:
                if (!EnemyAttack.AttackAnimation.Active)
                {
                    Enemy.IdleAnimation.StartAnimations(true);
                    InteractableUI.Instance.GetComponent<RectTransform>().localScale = Vector3.one;
                    TheWaitMode = WaitMode.None;
                    InteractableUI.Instance.EnableButtons();
                }
                break;
            default:
                break;
        }
    }
}
