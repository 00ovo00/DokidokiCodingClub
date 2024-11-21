using UnityEngine;
using System;
using static MainScene;
using System.Collections;



public class NoneUI : UIBase
{
    // 백그라운드 이미지 표시 
    private void Start()
    {
        Debug.Log("백그라운드 UI가 호출되었습니다.");
        MainScene.Instance.OnStateChanged += HandleStateChange;
        StartCoroutine(ChangeStateToDefaultAfterDelay());
    }

    private void OnChangeBackground()
    {
        // 백그라운드 배경이 계속 변경되도록
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
