﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameController;
public class Scan : MonoBehaviour
{
    public void ActivateScan()
    {
        Game.StartPlayerTurn();
        Game.Player.IdleAnimation.Active = false;
        Game.PlayerAttacks.Add(Game.EnemyAttack);
        Game.Player.Attacks.Add(Game.EnemyAttack.Name);
        //Write to PlayerPrefs
        Game.Player.ScanAnimation.StartAnimations();
        Game.TheWaitMode = WaitMode.WaitForFinishScan;
    }
}
