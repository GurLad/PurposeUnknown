using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateProjectileDuringAnimaton : MonoBehaviour, IAdvancedAnimationListener
{
    public GameObject Projectile;
    public int Step;
    private AdvancedAnimation animation;
    private void Start()
    {
        animation = GetComponent<AdvancedAnimation>();
        animation.AdvancedAnimationListeners.Add(this);
    }
    public void OnStepChange(int nextStep)
    {
        if (nextStep == Step)
        {
            GameObject newProjectile = Instantiate(Projectile);
            newProjectile.SetActive(true);
            newProjectile.transform.position = new Vector3(newProjectile.transform.position.x * Mathf.Sign(animation.Main.transform.position.x), newProjectile.transform.position.y, newProjectile.transform.position.z);
        }
    }
}
