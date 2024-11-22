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
        ������,
        ����
    }

    [SerializeField] private ChapterState _currentChapter;
    [SerializeField] private int _currentChapterIndex;

    public Action onEnterChapter;
    public Action onCurrentChapter;
    public Action onExitChapter;

    public Action onFadeEffect; // ȣ��Ǹ� ���
    public Action<int> ChangeArt; //

    public Dialogue[] dialogues;

    private void Start()
    {
        _currentChapterIndex = 0;
        EnterChapter(_currentChapterIndex);
    }
    public int CurrentChapterIndex // �ٸ� Ŭ�������� �б� �������� ���� ����
    {
        get { return _currentChapterIndex; }
    }

    private void EnterChapter(int chapterIndex)
    {
        if (chapterIndex > 6) { return; }
        string fileName = $"Chapter {chapterIndex + 1}";
        _currentChapter = (ChapterState)chapterIndex;
        DialogueData dialogueData = DataManager.Instance.Parse(fileName); 

        if (dialogueData != null && dialogueData.dialogues != null && dialogueData.dialogues.Length > 0)
        {
            dialogues = dialogueData.dialogues;
        }
        else
        {
            dialogues = new Dialogue[0];
        }

        onEnterChapter?.Invoke();
    }
    //private void CurrentChapter()
    //{
    //    onCurrentChapter?.Invoke();
    //}
    public void ExitChapter()
    {
        onExitChapter?.Invoke();
        _currentChapterIndex++;
        EnterChapter(_currentChapterIndex);

        if (_currentChapterIndex >= System.Enum.GetValues(typeof(ChapterState)).Length)
        {
            return;
        }


    }
    
    public void OnDialogueIndexChanged(int newDialogueIndex)
    {
        ChangeArt?.Invoke(newDialogueIndex);
    }
}
