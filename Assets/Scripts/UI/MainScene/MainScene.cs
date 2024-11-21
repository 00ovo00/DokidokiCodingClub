using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
public class MainScene : SingletonBase<MainScene>
{
    // ���ξ��� ������ UI���� FSM���� ����
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
        Debug.Log("���� ���¸� �����մϴ�.");
    }

    public void SetState(UIState newState)
    {
        if (currentState == newState)
            return;

        currentState = newState;
        OnStateChanged?.Invoke(newState); 

        HandleStateChange(newState);
        Debug.Log($"{newState}���·� ��ȯ�մϴ�.");
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
                Debug.Log("None �����Դϴ�.");
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
