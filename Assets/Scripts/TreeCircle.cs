using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCircle : MonoBehaviour
{
    public GameObject Tree;
    public float Radius;
    public float Amount;
    private void Start()
    {
        for (int i = 0; i < Amount; i++)
        {
            GameObject newTree = Instantiate(Tree);
            float degree = i * 2 * Mathf.PI / Amount;
            newTree.transform.position = transform.position + Radius * new Vector3(Mathf.Sin(degree), 0, Mathf.Cos(degree));
        }
        transform.Rotate(new Vector3(0, Random.Range(0, 360f), 0));
    }
}
