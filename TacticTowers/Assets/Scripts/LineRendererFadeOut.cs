using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererFadeOut : MonoBehaviour
{
    [SerializeField] private float fadeTime = 2f; // время затухания линии

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float alpha = 1f;
        while (alpha > 0f)
        {
            alpha -= Time.deltaTime / fadeTime;
            Color color = lineRenderer.material.color;
            color.a = alpha;
            lineRenderer.material.color = color;
            yield return null;
        }
    }
}
