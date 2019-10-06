using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveProjectile : MonoBehaviour
{
    public float Speed = 10;
    public bool DealDamage = true;
    public GameObject Particle;
    public bool UseDirection;
    public bool IsZap;
    private bool missed;
    private float count = 3;
    private void Start()
    {
        GetComponent<Rigidbody>().velocity = Speed * (!UseDirection ? (Vector3.right * -Mathf.Sign(transform.position.x)) : new Vector3(transform.right.x * -Mathf.Sign(transform.position.x), transform.right.y, transform.right.z));
        if (IsZap)
        {
            if (-Mathf.Sign(transform.position.x) == 1)
            {
                transform.localEulerAngles = new Vector3(0, 0, -7);
            }
            else
            {
                transform.localEulerAngles = new Vector3(0, 0, 7);
            }
        }
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
