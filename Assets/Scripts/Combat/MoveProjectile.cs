using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveProjectile : MonoBehaviour
{
    public float Speed = 10;
    public bool DealDamage = true;
    public GameObject Particle;
    public bool UseDirection;
    private bool missed;
    private float count = 3;
    private void Start()
    {
        Speed *= -Mathf.Sign(transform.position.x);
        GetComponent<Rigidbody>().velocity = Speed * (!UseDirection ? Vector3.right : transform.right);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (DealDamage && !missed)
        {
            if (Attack.CurrentAttack != null)
            {
                if (Attack.CurrentAttack.DealDamage())
                {
                    if (Particle != null)
                    {
                        Instantiate(Particle, transform.position, Quaternion.identity).SetActive(true);
                    }
                    Destroy(gameObject);
                }
                else
                {
                    missed = true;
                }
            }
        }
    }
    private void Update()
    {
        if (missed)
        {
            count -= Time.deltaTime;
            if (count <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
