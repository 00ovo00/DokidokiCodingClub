using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
public class MainScene : SingletonBase<MainScene>
{
    // 메인씬에 나오는 UI들을 FSM으로 관리
    public enum UIState
    {
        Null,
        None,
        Default,
        Option,
        Result
    }

    private UIState currentState;
    public event Action<UIState> OnStateChanged; 

    private void Start()
    {
        SetState(UIState.None);
        Debug.Log("최초 상태를 설정합니다.");
    }

    public void SetState(UIState newState)
    {
        if (currentState == newState)
            return;

        currentState = newState;
        OnStateChanged?.Invoke(newState); 

        HandleStateChange(newState);
        Debug.Log($"{newState}상태로 전환합니다.");
    }

    private void HandleStateChange(UIState state)
    {
        UIManager.Instance.Hide<DialogueUI>();
        UIManager.Instance.Hide<OptionUI>();
        UIManager.Instance.Hide<ResultUI>();

        switch (state)
        {
            case UIState.None:
                ChapterManager.Instance.onFadeEffect += UIManager.Instance.Show<NoneUI>().OnChangeBackground;
                ChapterManager.Instance.onFadeEffect?.Invoke();
                Debug.Log("None 상태입니다.");
                break;
            case UIState.Default:
                UIManager.Instance.Show<DialogueUI>();
                break;
            case UIState.Option:
                UIManager.Instance.Show<OptionUI>();
                break;
            case UIState.Result:
                UIManager.Instance.Show<ResultUI>(); 
                break;
        }
    }
}
