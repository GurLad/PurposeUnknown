using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Fade : MonoBehaviour {
    public float speed;
    public bool expand = false;
    public bool ExpotentialExpand = false;
    public float expandSpeed;
    private float count = 0;
    private Vector3 BaseScale;
    private List<Material> myMaterials;
    void Start()
    {
        BaseScale = gameObject.transform.localScale;
        myMaterials = new List<Material>();
        List<Renderer> renderers = new List<Renderer>(gameObject.GetComponentsInChildren<Renderer>());
        foreach (var item in renderers)
        {
            myMaterials.AddRange(item.materials);
        }
    }
	void Update () {
        foreach (Material temp in myMaterials)
        {
            temp.color = new Color(temp.color.r, temp.color.g, temp.color.b, temp.color.a - Time.deltaTime * speed);
            if (temp.color.a <= 0)
            {
                Destroy(gameObject);
            }
        }
        if (expand)
        {
            Vector3 t = gameObject.transform.localScale;
            gameObject.transform.localScale = new Vector3(t.x + Time.deltaTime * expandSpeed, t.y + Time.deltaTime * expandSpeed, t.z + Time.deltaTime * expandSpeed);
        }
        if (ExpotentialExpand)
        {
            count += Time.deltaTime;
            Vector3 t = BaseScale;
            gameObject.transform.localScale = new Vector3(t.x * Mathf.Pow(expandSpeed,count), t.y * Mathf.Pow(expandSpeed, count), t.z * Mathf.Pow(expandSpeed, count));
        }
    }
}
