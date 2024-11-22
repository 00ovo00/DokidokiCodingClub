using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    public IEnumerator FadeIn(Image canvasGroup, float duration)
    {
        float startTime = Time.time;
        Color originColor = canvasGroup.color;
        while (Time.time < startTime + duration)
        {
            float alpha = Mathf.Lerp(0, 1, (Time.time - startTime) / duration);
            canvasGroup.color = new Color(originColor.r, originColor.g, originColor.b, alpha);
            yield return null;
        }
        canvasGroup.color = new Color(originColor.r, originColor.g, originColor.b, 1);
    }

    public IEnumerator FadeOut(Image canvasGroup, float duration)
    {
        float startTime = Time.time;
        Color originColor = canvasGroup.color;
        while (Time.time < startTime + duration)
        {
            float alpha = Mathf.Lerp(1, 0, (Time.time - startTime) / duration);
            canvasGroup.color = new Color(originColor.r, originColor.g, originColor.b, alpha);
            yield return null;
        }
        canvasGroup.color = new Color(originColor.r, originColor.g, originColor.b, 0);
    }
}
