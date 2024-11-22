using System;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : SingletonBase<EndingManager>
{
    public enum EndingState
    {
        ����1,
        ����2,
        ����3
    }

    [SerializeField] private EndingState _currentEnding;
    [SerializeField] private int _currentEndingIndex;

    public Action onEnterEnding;
    public Action onExitEnding;

    public Dialogue[] endingDialogues;

    public void EnterEnding(string character)
    {
        // ���� ���� ȣ������ ĳ���Ϳ� ���� ���� ����
        switch (character)
        {
            case "������ �Ŵ���":
                _currentEndingIndex = 0; // ����1
                break;
            case "�̼��� Ʃ��":
                _currentEndingIndex = 1; // ����2
                break;
            case "����� Ʃ��":
                _currentEndingIndex = 2; // ����3
                break;
            default:
                Debug.LogError("�߸��� ĳ���� �̸��Դϴ�.");
                return;
        }

        string fileName = $"HappyEnding {_currentEndingIndex + 1}";
        DialogueData endingData = DataManager.Instance.Parse(fileName);

        if (endingData != null && endingData.dialogues != null && endingData.dialogues.Length > 0)
        {
            endingDialogues = endingData.dialogues;
            onEnterEnding?.Invoke();
        }
        else
        {
            Debug.LogError("���� �����͸� �ҷ����� �� �����߽��ϴ�.");
            endingDialogues = new Dialogue[0];
        }
    }

    public void ExitEnding()
    {
        onExitEnding?.Invoke();
    }
}
