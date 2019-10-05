using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    public BattleStats Player;
    public BattleStats Enemy;
    public Text TheText;
    public Attack TheAttack;
    private bool waiting = false;
    public void ActivateAttack()
    {
        TheAttack.ToolAnimation.StartAnimations();
        waiting = true;
    }
    private void Update()
    {
        if (waiting)
        {
            if (!TheAttack.ToolAnimation.Active)
            {
                TheAttack.Launch(Player, Enemy);
                waiting = false;
            }
        }
    }
}
