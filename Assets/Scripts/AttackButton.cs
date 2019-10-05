using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameController;
public class AttackButton : MonoBehaviour
{
    public Text TheText;
    public Attack TheAttack;
    public void ActivateAttack()
    {
        InteractableUI.Instance.GetComponent<RectTransform>().localScale = Vector3.zero;
        Game.Player.IdleAnimation.Active = false;
        Game.PlayerAttack = TheAttack;
        Game.PlayerAttack.ToolAnimation.StartAnimations();
        Game.TheWaitMode = WaitMode.WaitForFinishTool;
    }
}
