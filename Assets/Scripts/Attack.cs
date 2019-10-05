using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Attack
{
    public static Attack CurrentAttack { get; private set; }
    public string Name;
    public AdvancedAnimation ToolAnimation;
    public AdvancedAnimation AttackAnimation;
    public int Power;
    public int Accuracy;
    public int EnergyCost;
    private BattleStats attacker;
    private BattleStats defender;
    public void Launch(BattleStats attacker, BattleStats defender)
    {
        CurrentAttack = this;
        this.attacker = attacker;
        this.defender = defender;
        AttackAnimation.Main = attacker.gameObject;
        AttackAnimation.Start();
        AttackAnimation.StartAnimations();
    }
    public void DealDamage()
    {
        attacker.Energy -= EnergyCost;
        if (UnityEngine.Random.Range(0, 1) <= Accuracy / 100.0f)
        {
            defender.Health -= Power + attacker.Power - defender.Defense;
        }
    }
}
