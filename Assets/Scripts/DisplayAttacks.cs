using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameController;

public class DisplayAttacks : MonoBehaviour
{
    public Button Base;
    private void Start()
    {
        Display(new List<string>(new string[] { "Laser gun", "Fireball" }));
    }
    private void Display(List<string> attackNames)
    {
        for (int i = 0; i < Game.PlayerAttacks.Count; i++)
        {
            Button newButton = Instantiate(Base, Base.transform.parent);
            newButton.gameObject.SetActive(true);
            AttackButton attackButton = newButton.GetComponent<AttackButton>();
            attackButton.TheText.text = Game.PlayerAttacks[i].Name;
            attackButton.TheAttack = Game.PlayerAttacks[i];
            RectTransform rectTransform = newButton.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = rectTransform.anchoredPosition + new Vector2(0, -30 * i);
        }
    }
}
