using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public IEnumerator FadeIn(CanvasGroup canvasGroup, float duration)
    {
        float startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, (Time.time - startTime) / duration);
            yield return null;
        }
        canvasGroup.alpha = 1;
    }

    public IEnumerator FadeOut(CanvasGroup canvasGroup, float duration)
    {
        float startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, (Time.time - startTime) / duration);
            yield return null;
        }
        canvasGroup.alpha = 0;
    }
}
