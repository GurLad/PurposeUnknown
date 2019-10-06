using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static GameController;
public class AttackButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        AttackDisplay.Instance.ShowInfo(TheAttack);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        AttackDisplay.Instance.Hide();
    }
}
