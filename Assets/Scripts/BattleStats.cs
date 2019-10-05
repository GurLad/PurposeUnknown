using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStats : MonoBehaviour
{
    public bool IsPlayer;
    public string Name;
    public int Health;
    public int MaxHealth;
    public int Energy;
    public int MaxEnergy;
    public int Power;
    public int Defense;
    public List<string> Attacks;
    public GameObject Body;
    public AdvancedAnimation IdleAnimation;
}
