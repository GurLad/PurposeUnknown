using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeTree : MonoBehaviour
{
    public float ScaleMaxMod;
    public float ColorMaxMod;
    public float RotMaxMod;
    private void Start()
    {
        foreach (Transform item in transform)
        {
            MeshRenderer meshRenderer = item.gameObject.GetComponent<MeshRenderer>();
            Color color = meshRenderer.material.color;
            color.r *= Random.Range(1.0f - ColorMaxMod, 1.0f + ColorMaxMod);
            color.g *= Random.Range(1.0f - ColorMaxMod, 1.0f + ColorMaxMod);
            color.b *= Random.Range(1.0f - ColorMaxMod, 1.0f + ColorMaxMod);
            meshRenderer.material.color = color;
            item.localScale = new Vector3(
                item.localScale.x * Random.Range(1.0f - ScaleMaxMod, 1.0f + ScaleMaxMod),
                item.localScale.y * Random.Range(1.0f - ScaleMaxMod, 1.0f + ScaleMaxMod),
                item.localScale.z * Random.Range(1.0f - ScaleMaxMod, 1.0f + ScaleMaxMod));
        }
    }
}
