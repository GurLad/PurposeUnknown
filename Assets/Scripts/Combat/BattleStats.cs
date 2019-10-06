using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
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
    public AdvancedAnimation ScanAnimation;
    public MissedMessage Missed;
    public override string ToString()
    {
        return Name + ";" + MaxHealth + ";" + MaxEnergy + ";" + Power + ";" + Defense + ";" + String.Join(",", Attacks);
    }
    public void FromString(string str)
    {
        string[] parts = str.Split(';');
        Name = parts[0];
        MaxHealth = int.Parse(parts[1]);
        Health = MaxHealth;
        MaxEnergy = int.Parse(parts[2]);
        Energy = MaxEnergy;
        Power = int.Parse(parts[3]);
        Defense = int.Parse(parts[4]);
        Attacks = new List<string>(parts[5].Split(','));
    }
}
