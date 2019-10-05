using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    public BattleStats Stats;
    public Text Name;
    public Text Health;
    public Text Energy;
    public Text Power;
    public Text Defense;
    private void Update()
    {
        ShowStats();
    }
    private void ShowStats()
    {
        Name.text = Stats.Name;
        Health.text = "HP:\t\t" + Stats.Health + (Stats.Health < 100 ? "\t/" : "/" ) + Stats.MaxHealth;
        Energy.text = "Energy:\t" + Stats.Energy + (Stats.Energy < 100 ? "\t/" : "/") + Stats.MaxEnergy;
        Power.text = "Power:\t" + Stats.Power;
        Defense.text = "Defense:\t" + Stats.Defense;
    }
}
