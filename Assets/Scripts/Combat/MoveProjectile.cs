using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveProjectile : MonoBehaviour
{
    public float Speed = 10;
    public bool DealDamage;
    public GameObject Particle;
    private void Start()
    {
        Speed *= -Mathf.Sign(transform.position.x);
        GetComponent<Rigidbody>().velocity = new Vector3(Speed, 0, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Attack.CurrentAttack != null)
        {
            Attack.CurrentAttack.DealDamage();
            if (Particle != null)
            {
                Instantiate(Particle, transform.position, Quaternion.identity).SetActive(true);
            }
            Destroy(gameObject);
        }
    }
}
