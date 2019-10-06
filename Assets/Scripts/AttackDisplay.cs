using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackDisplay : MonoBehaviour
{
    public static AttackDisplay Instance;
    public Text Power;
    public Text Accuracy;
    public Text EnergyCost;
    private Image image;
    private void Start()
    {
        image = GetComponent<Image>();
        Hide();
    }
    private void Awake()
    {
        Instance = this;
    }
    public void ShowInfo(Attack attack)
    {
        image.enabled = true;
        Power.text = "Power: " + attack.Power;
        Accuracy.text = "Accuracy: " + attack.Accuracy;
        EnergyCost.text = "Energy cost: " + attack.EnergyCost;
    }
    public void Hide()
    {
        image.enabled = false;
        Power.text = "";
        Accuracy.text = "";
        EnergyCost.text = "";
    }
}
