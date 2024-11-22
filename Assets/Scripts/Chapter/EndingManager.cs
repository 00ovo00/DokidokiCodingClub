using System;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : SingletonBase<EndingManager>
{
    public enum EndingState
    {
        엔딩1,
        엔딩2,
        엔딩3
    }

    [SerializeField] private EndingState _currentEnding;
    [SerializeField] private int _currentEndingIndex;

    public Action onEnterEnding;
    public Action onExitEnding;

    public Dialogue[] endingDialogues;

    public void EnterEnding(string character)
    {
        // 가장 높은 호감도의 캐릭터에 따라 엔딩 설정
        switch (character)
        {
            case "안혜린 매니저":
                _currentEndingIndex = 0; // 엔딩1
                break;
            case "이성언 튜터":
                _currentEndingIndex = 1; // 엔딩2
                break;
            case "김재경 튜터":
                _currentEndingIndex = 2; // 엔딩3
                break;
            default:
                Debug.LogError("잘못된 캐릭터 이름입니다.");
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
            Debug.LogError("엔딩 데이터를 불러오는 데 실패했습니다.");
            endingDialogues = new Dialogue[0];
        }
    }

    public void ExitEnding()
    {
        onExitEnding?.Invoke();
    }
}
