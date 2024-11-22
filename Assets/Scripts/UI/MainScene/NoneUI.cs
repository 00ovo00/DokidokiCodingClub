using UnityEngine;
using System;
using static MainScene;
using System.Collections;
using UnityEngine.UI;



public class NoneUI : UIBase
{
    [SerializeField] private Image backgroundGroup;
    [SerializeField] private Image characterGroup;

    [SerializeField] private float fadeDuration = 1f;

    private FadeController fadeController;

    // 백그라운드 이미지 표시 
    private void Start()
    {
        fadeController = gameObject.AddComponent<FadeController>();

        ChapterManager.Instance.onFadeEffect += OnChangeBackground;
        ChapterManager.Instance.onFadeEffect?.Invoke();


        Debug.Log("백그라운드 UI가 호출되었습니다.");
        MainScene.Instance.OnStateChanged += HandleStateChange;
        StartCoroutine(ChangeStateToDefaultAfterDelay());

    }

    private void OnChangeBackground()
    {
        StartCoroutine(FadeBackGround());
    }

    public IEnumerator FadeBackGround()
    {
        yield return fadeController.FadeOut(backgroundGroup, fadeDuration);

        ChangeBackGroundImage();

        yield return fadeController.FadeIn(backgroundGroup, fadeDuration);
    }
    public IEnumerator FadeCharacter()
    {
        yield return fadeController.FadeOut(characterGroup, fadeDuration);

        ChangeCharacterImage();

        yield return fadeController.FadeIn(characterGroup, fadeDuration);
    }
    private void ChangeBackGroundImage()
    {
        Debug.Log("배경 이미지 전환 코드");
    }

    private void ChangeCharacterImage()
    {
        Debug.Log("캐릭터 이미지 전환 코드");
    }

    private void OnDestroy()
    {
        MainScene.Instance.OnStateChanged -= HandleStateChange;
    }

    private void HandleStateChange(UIState newState)
    {
        // 여기 말고 다이얼로그 쪽에서 페이드 주기
        Debug.Log($"상태가 변경되었습니다: {newState}");
    }

    private IEnumerator ChangeStateToDefaultAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        MainScene.Instance.SetState(UIState.Default); // 상태 변경
    }
}
