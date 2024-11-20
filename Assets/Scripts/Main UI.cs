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

    public event Action<UIState> OnStateChanged; // ���� ��ȯ �̺�Ʈ

    private void Start()
    {
        SetState(UIState.Default); // ���� ���� ����
    }

    /// <summary>
    /// ���¸� �����ϰ� �̺�Ʈ ȣ��
    /// </summary>
    public void SetState(UIState newState)
    {
        if (currentState == newState)
            return;

        currentState = newState;
        OnStateChanged?.Invoke(newState); // ���� ���� �̺�Ʈ ȣ��

        HandleStateChange(newState); // ���� ��ȯ ���� ó��
    }

    /// <summary>
    /// ���¿� ���� UI�� Show/Hide
    /// </summary>
    private void HandleStateChange(UIState state)
    {
        // ��� ������ UI�� �ݱ�
        //UIManager.Instance.Hide<Prefab>();
        //UIManager.Instance.Hide<OptionUI>();
        //UIManager.Instance.Hide<ResultUI>();

        // ���¿� ���� UI ����
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
    /// �޴� ���� ��ư Ŭ�� �� ȣ�� (�ν����Ϳ��� ����)
    /// </summary>
    public void OnMenuButtonClicked()
    {
        SetState(UIState.Option); // �ɼ� ���·� ��ȯ
    }
}
