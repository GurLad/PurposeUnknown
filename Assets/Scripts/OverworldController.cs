using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldController : MonoBehaviour
{
    public static OverworldController Instance;
    public float Speed;
    public GameObject Model;
    public AdvancedAnimation IdleAnimation;
    public AdvancedAnimation WalkAnimation;
    private Rigidbody rigidbody;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerXPos", -4), 0, PlayerPrefs.GetFloat("PlayerZPos", 4));
    }
    private void Update()
    {
        Vector3 newSpeed = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Speed;
        if (newSpeed.magnitude >= 0.1f)
        {
            Model.transform.LookAt(Model.transform.position + newSpeed);
            rigidbody.velocity = newSpeed;
            IdleAnimation.Active = false;
            if (!WalkAnimation.Active)
            {
                WalkAnimation.StartAnimations(true);
            }
        }
        else
        {
            rigidbody.velocity = Vector3.zero;
            WalkAnimation.Active = false;
            if (!IdleAnimation.Active)
            {
                IdleAnimation.StartAnimations(true);
            }
        }
    }
}
