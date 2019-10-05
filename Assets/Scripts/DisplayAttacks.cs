using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayAttacks : MonoBehaviour
{
    public Button Base;
    private void Start()
    {
        Display(new List<string>(new string[] { "Laser gun", "Fireball" }));
    }
    private void Display(List<string> attackNames)
    {
        List<Attack> attacks = AllAttacks.Instance.Attacks.FindAll(a => attackNames.Contains(a.Name));
        for (int i = 0; i < attacks.Count; i++)
        {
            Button newButton = Instantiate(Base, Base.transform.parent);
            newButton.gameObject.SetActive(true);
            AttackButton attackButton = newButton.GetComponent<AttackButton>();
            attackButton.TheText.text = attacks[i].Name;
            attackButton.TheAttack = attacks[i];
            RectTransform rectTransform = newButton.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = rectTransform.anchoredPosition + new Vector2(0, -30 * i);
        }
    }
}
