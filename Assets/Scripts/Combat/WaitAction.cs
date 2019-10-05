using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameController;

public class WaitAction : MonoBehaviour
{
    public int EnergyValue = 5;
    public void ActivateWait()
    {
        Game.TheWaitMode = WaitMode.None;
        Game.Player.Missed.Show("Waited");
        Game.StartPlayerTurn();
        Game.Player.Energy += EnergyValue;
        Game.EndPlayerTurn();
    }
}
