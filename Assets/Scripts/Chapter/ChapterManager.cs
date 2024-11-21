using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterManager : SingletonBase<ChapterManager>
{
    public enum ChapterState
    {
        ����,
        ù������Ʈ,
        Ư��,
        ����,
        �ϻ�,
        ����������Ʈ,
        ������
    }

    [SerializeField] private ChapterState _currentChapter;
    [SerializeField] private int _currentChapterIndex;

    public Action onEnterChapter;
    public Action onCurrentChapter;
    public Action onExitChapter;

    public Dialogue[] dialogues;
 
    private void Awake()
    {
        _currentChapterIndex = 0;
        EnterChapter(_currentChapterIndex);
    }

    private void EnterChapter(int chapterIndex)
    {
        string fileName = $"Chapter {chapterIndex + 1}";
        _currentChapter = (ChapterState)chapterIndex;
        DialogueData dialogueData = DataManager.Instance.Parse(fileName); 
        if (dialogueData != null && dialogueData.dialogues != null && dialogueData.dialogues.Length > 0)
        {
            dialogues = dialogueData.dialogues;
        }
        else
        {
            Debug.LogError("DialogueData�� null�̰ų� dialogues�� ��� �ֽ��ϴ�.");
            dialogues = new Dialogue[0];
        }

        onEnterChapter?.Invoke();
    }
    private void CurrentChapter()
    {
        onCurrentChapter?.Invoke();
    }
    public void ExitChapter()
    {
        onExitChapter?.Invoke();
        _currentChapterIndex++;

        if (_currentChapterIndex >= System.Enum.GetValues(typeof(ChapterState)).Length)
        {
            return;
        }
    }
}
