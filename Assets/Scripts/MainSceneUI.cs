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
        Debug.Log("���̾�α׸� ã���ϴ�.");
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
        // ��� ������ UI�� �ݱ�
        //UIManager.Instance.Hide<DialogueUI>();
        //UIManager.Instance.Hide<OptionUI>();
        //UIManager.Instance.Hide<ResultUI>();

        // ���¿� ���� UI ����
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
        Debug.Log("ȣ������ Ȯ���մϴ�.");
    }

    public void OnSettingButton()
    {
        Debug.Log("������ ���Ƚ��ϴ�.");
    }
    public void OnMailButton()
    {
        Debug.Log("������ ���Ƚ��ϴ�.");
    }
}


