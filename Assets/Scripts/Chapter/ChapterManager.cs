using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterManager : SingletonBase<ChapterManager>
{
    public enum ChapterState
    {
        개강,
        첫프로젝트,
        특강,
        봉사,
        일상,
        최종프로젝트,
        수료일
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
        if (dialogueData != null)
        {
            dialogues = dialogueData.dialogues;
        }
        else
        {
            Debug.LogError("DialogueData가 null이거나 dialogues가 비어 있습니다.");
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
