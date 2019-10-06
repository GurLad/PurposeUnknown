using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveProjectile : MonoBehaviour
{
    public float Speed = 10;
    public bool DealDamage;
    public GameObject Particle;
    private bool missed;
    private float count = 3;
    private void Start()
    {
        Speed *= -Mathf.Sign(transform.position.x);
        GetComponent<Rigidbody>().velocity = new Vector3(Speed, 0, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!missed)
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
