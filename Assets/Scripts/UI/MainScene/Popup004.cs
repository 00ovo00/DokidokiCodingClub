using System.Collections;
using TMPro;
using UnityEngine;

public class Popup004 : UIBase
{
    [SerializeField] private TextMeshProUGUI results;
    public void SetUpResults(int result)
    {
        results.text = ($"호감도 {result}!");
        StartCoroutine(HidePopupAfterDelay(1f));
    }

    private IEnumerator HidePopupAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 
        UIManager.Instance.Hide<Popup004>();
    }
}
