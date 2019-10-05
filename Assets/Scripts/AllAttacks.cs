using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllAttacks : MonoBehaviour
{
    public static AllAttacks Instance { get; private set; }
    public List<Attack> Attacks;
    private void Awake()
    {
        Instance = this;
    }
}
