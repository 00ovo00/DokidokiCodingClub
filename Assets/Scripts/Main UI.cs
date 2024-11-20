using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class MainUI : UIBase
{
    public enum UIState
    {
        Default,
        Option,
        Result
    }

    private UIState currentState;

    public event Action<UIState> OnStateChanged; // 상태 전환 이벤트

    private void Start()
    {
        SetState(UIState.Default); // 시작 상태 설정
    }

    /// <summary>
    /// 상태를 설정하고 이벤트 호출
    /// </summary>
    public void SetState(UIState newState)
    {
        if (currentState == newState)
            return;

        currentState = newState;
        OnStateChanged?.Invoke(newState); // 상태 변경 이벤트 호출

        HandleStateChange(newState); // 상태 전환 로직 처리
    }

    /// <summary>
    /// 상태에 따라 UI를 Show/Hide
    /// </summary>
    private void HandleStateChange(UIState state)
    {
        // 모든 상태의 UI를 닫기
        //UIManager.Instance.Hide<Prefab>();
        //UIManager.Instance.Hide<OptionUI>();
        //UIManager.Instance.Hide<ResultUI>();

        // 상태에 따른 UI 열기
        switch (state)
        {
            //case UIState.Default:
            //    UIManager.Instance.Show<DialogueUI>();
            //    break;
            //case UIState.Option:
            //    UIManager.Instance.Show<OptionUI>();
                
            //    break;
            //case UIState.Result:
            //    UIManager.Instance.Show<ResultUI>();
            //    break;
        }
    }

    /// <summary>
    /// 메뉴 열기 버튼 클릭 시 호출 (인스펙터에서 연결)
    /// </summary>
    public void OnMenuButtonClicked()
    {
        SetState(UIState.Option); // 옵션 상태로 전환
    }
}
