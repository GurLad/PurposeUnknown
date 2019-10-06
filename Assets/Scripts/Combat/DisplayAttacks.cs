using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameController;

public class DisplayAttacks : MonoBehaviour
{
    public static DisplayAttacks Instance { get; private set; }
    public Button Base;
    public Button ScrollDown;
    public Button ScrollUp;
    private List<RectTransform> attackButtons;
    private void Start()
    {
        Instance = this;
        attackButtons = new List<RectTransform>();
        Display();
    }
    public void Display()
    {
        attackButtons.Clear();
        for (int i = 0; i < Game.PlayerAttacks.Count; i++)
        {
            Button newButton = Instantiate(Base, Base.transform.parent);
            newButton.gameObject.SetActive(true);
            AttackButton attackButton = newButton.GetComponent<AttackButton>();
            attackButton.TheText.text = Game.PlayerAttacks[i].Name;
            attackButton.TheAttack = Game.PlayerAttacks[i];
            RectTransform rectTransform = newButton.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = rectTransform.anchoredPosition + new Vector2(0, -30 * i);
            attackButtons.Add(rectTransform);
        }
        if (attackButtons.Count <= 3)
        {
            ScrollDown.interactable = false;
            ScrollUp.interactable = false;
        }
        else
        {
            ScrollDown.interactable = true;
            ScrollUp.interactable = false;
        }
    }
    public void ScrollUpAction()
    {
        foreach (var item in attackButtons)
        {
            item.anchoredPosition = item.anchoredPosition + new Vector2(0, -30);
            if (item.anchoredPosition.y <= 0)
            {
                item.gameObject.SetActive(true);
            }
        }
        if (attackButtons[0].anchoredPosition.y <= 0)
        {
            ScrollUp.interactable = false;
        }
        ScrollDown.interactable = true;
    }
    public void ScrollDownAction()
    {
        foreach (var item in attackButtons)
        {
            item.anchoredPosition = item.anchoredPosition + new Vector2(0, 30);
            if (item.anchoredPosition.y > 0)
            {
                item.gameObject.SetActive(false);
            }
        }
        if (attackButtons[attackButtons.Count - 1].anchoredPosition.y >= -60)
        {
            ScrollDown.interactable = false;
        }
        ScrollUp.interactable = true;
    }
}
