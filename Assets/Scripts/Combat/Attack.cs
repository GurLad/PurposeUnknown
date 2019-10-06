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
    public AdvancedAnimation HumanAnimation;
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
        if (!Game.IsHuman || attacker.IsPlayer)
        {
            AttackAnimation.Main = attacker.gameObject;
            AttackAnimation.Start();
            AttackAnimation.StartAnimations();
        }
        else
        {
            HumanAnimation.Main = attacker.gameObject;
            HumanAnimation.Start();
            HumanAnimation.StartAnimations();
        }
    }
    public bool DealDamage()
    {
        if (UnityEngine.Random.Range(0f, 1f) <= Accuracy / 100.0f)
        {
            int damage = Power + attacker.Power - defender.Defense;
            if (damage > 0)
            {
                defender.Health -= damage;
                if (defender.Health <= 0)
                {
                    Game.TheWaitMode = WaitMode.None;
                    Game.Explode(defender);
                }
                return true;
            }
            else
            {
                defender.Missed.Show("Blocked");
                return true;
            }
        }
        else
        {
            defender.Missed.Show();
            return false;
        }
    }
}
