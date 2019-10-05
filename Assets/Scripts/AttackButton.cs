using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum WaitMode { None, WaitForFinishTool, WaitForFinishPlayerAttack, WaitForFinishWithdrawingTool, WaitForEnemyAttack}
public class AttackButton : MonoBehaviour
{
    public BattleStats Player;
    public BattleStats Enemy;
    public Text TheText;
    public Attack TheAttack;
    private WaitMode waitMode = WaitMode.None;
    private Attack OtherAttack;
    public void ActivateAttack()
    {
        InteractableUI.Instance.GetComponent<RectTransform>().localScale = Vector3.zero;
        Player.IdleAnimation.Active = false;
        TheAttack.ToolAnimation.StartAnimations();
        waitMode = WaitMode.WaitForFinishTool;
    }
    private void Update()
    {
        switch (waitMode)
        {
            case WaitMode.None:
                break;
            case WaitMode.WaitForFinishTool:
                if (!TheAttack.ToolAnimation.Active)
                {
                    TheAttack.Launch(Player, Enemy);
                    waitMode = WaitMode.WaitForFinishPlayerAttack;
                }
                break;
            case WaitMode.WaitForFinishPlayerAttack:
                if (!TheAttack.AttackAnimation.Active)
                {
                    TheAttack.ToolAnimation.Reverse();
                    TheAttack.ToolAnimation.StartAnimations();
                    waitMode = WaitMode.WaitForFinishWithdrawingTool;
                }
                break;
            case WaitMode.WaitForFinishWithdrawingTool:
                if (!TheAttack.ToolAnimation.Active)
                {
                    TheAttack.ToolAnimation.Reverse();
                    Player.IdleAnimation.StartAnimations(true);
                    //Enemy do stuff
                    Attack attack = AllAttacks.Instance.Attacks.FindAll(a => Enemy.Attacks.Contains(a.Name))[Random.Range(0, Enemy.Attacks.Count)];
                    attack.Launch(Enemy, Player);
                    OtherAttack = attack;
                    waitMode = WaitMode.WaitForEnemyAttack;
                }
                break;
            case WaitMode.WaitForEnemyAttack:
                if (!OtherAttack.AttackAnimation.Active)
                {
                    Enemy.IdleAnimation.StartAnimations(true);
                    InteractableUI.Instance.GetComponent<RectTransform>().localScale = Vector3.one;
                    waitMode = WaitMode.None;
                }
                break;
            default:
                break;
        }
    }
}
