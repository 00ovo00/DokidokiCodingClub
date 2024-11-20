using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class MainSceneUI : MonoBehaviour 
{
    public enum UIState
    {
        Default,
        Option,
        Result
    }

    private UIState currentState;
    public event Action<UIState> OnStateChanged; 

    private void Awake()
    {
        //SetState(UIState.Default); 
        UIManager.Instance.Show<DialogueUI>();
        Debug.Log("다이얼로그를 찾습니다.");
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
        // 모든 상태의 UI를 닫기
        //UIManager.Instance.Hide<DialogueUI>();
        //UIManager.Instance.Hide<OptionUI>();
        //UIManager.Instance.Hide<ResultUI>();

        // 상태에 따른 UI 열기
        switch (state)
        {
            case UIState.Default:
                UIManager.Instance.Show<DialogueUI>();
                break;
                //case UIState.Option:
                //    UIManager.Instance.Show<OptionUI>();

                //    break;
                //case UIState.Result:
                //    UIManager.Instance.Show<ResultUI>();
                //    break;
        }
    }

    public void OnFriendshipButton()
    {
        Debug.Log("호감도를 확인합니다.");
    }

    public void OnSettingButton()
    {
        Debug.Log("설정이 눌렸습니다.");
    }
    public void OnMailButton()
    {
        Debug.Log("메일이 눌렸습니다.");
    }
}


