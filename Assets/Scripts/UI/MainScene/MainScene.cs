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
        End
    }

    private UIState currentState;
    public event Action<UIState> OnStateChanged; 

    private void Start()
    {
        SetState(UIState.None);
    }

    public void SetState(UIState newState)
    {
        if (currentState == newState)
            return;

        currentState = newState;
        OnStateChanged?.Invoke(newState); 

        HandleStateChange(newState);
    }

    private void HandleStateChange(UIState state)
    {
        UIManager.Instance.Hide<DialogueUI>();
        UIManager.Instance.Hide<OptionUI>();
        UIManager.Instance.Hide<ResultUI>();

        switch (state)
        {
            case UIState.None:
                UIManager.Instance.Show<NoneUI>();
                break;
            case UIState.Default:
                UIManager.Instance.Show<DialogueUI>();
                break;
            case UIState.End:
                UIManager.Instance.Show<GameOverEnd>();
                UIManager.Instance.Hide<NoneUI>();
                break;
        }
    }
}
