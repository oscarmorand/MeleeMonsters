using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEffects : MonoBehaviour
{
    public GameObject gameObjectBody;
    private List<SpriteRenderer> childrenSpriteRenderer = new List<SpriteRenderer>();
    public float fadeSpeed = 0.1f;
    public float fadeStart = 1f;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var child in GetComponentsInChildren<SpriteRenderer>())
        {
            childrenSpriteRenderer.Add(child);
        }

        StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut()
    {

        for (float f = fadeStart; f >= fadeSpeed; f -= fadeSpeed)
        {
            foreach (var childRend in childrenSpriteRenderer)
            {
                Color c = childRend.material.color;
                c.a = f;
                childRend.material.color = c;
            }
            
            yield return new WaitForSeconds(0.02f);
        }

        Destroy(gameObject);
    }


  
}
