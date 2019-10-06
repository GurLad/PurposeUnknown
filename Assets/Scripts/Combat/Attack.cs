using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameController;
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
        attacker.IdleAnimation.Active = false;
        attacker.Energy -= EnergyCost;
        AttackAnimation.Main = attacker.gameObject;
        AttackAnimation.Start();
        AttackAnimation.StartAnimations();
    }
    public bool DealDamage()
    {
        if (UnityEngine.Random.Range(0f, 1f) <= Accuracy / 100.0f)
        {
            defender.Health -= Power + attacker.Power - defender.Defense;
            if (defender.Health <= 0)
            {
                Game.TheWaitMode = WaitMode.None;
                Game.Explode(defender);
            }
            return true;
        }
        else
        {
            defender.Missed.Show();
            return false;
        }
    }
}
