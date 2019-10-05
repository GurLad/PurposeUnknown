using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableUI : MonoBehaviour
{
    public static InteractableUI Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
}
