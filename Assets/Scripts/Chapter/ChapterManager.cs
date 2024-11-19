using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterManager : MonoBehaviour
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

    private Dictionary<ChapterState, string> _chapterCSV;

    [SerializeField] private ChapterState _currentChapter;
    [SerializeField] private int _currentChapterIndex;

    public Action onEnterChapter;
    public Action onCurrentChapter;
    public Action onExitChapter;

    public Dialogue[] dialogues;
    public Dialogue[] GetDialogues()
    {
        return dialogues;
    }

    private void Start()
    {
        _currentChapterIndex = 0;
        EnterChapter(_currentChapterIndex);
    }
    private void EnterChapter(int chapterIndex)
    {
        _currentChapter = (ChapterState)chapterIndex;

        // 챕터에 맞는 파일 이름 넣기 
        DialogueData dialogueData = DataManager.Instance.Parse("TestData"); 
        if (dialogueData != null)
        {
            dialogues = dialogueData.dialogues;
        }
        onEnterChapter?.Invoke();
    }
    private void CurrentChapter()
    {
        onCurrentChapter?.Invoke();
    }
    private void ExitChapter()
    {
        onExitChapter?.Invoke();
        _currentChapterIndex++;

        if (_currentChapterIndex >= System.Enum.GetValues(typeof(ChapterState)).Length)
        {
            return;
        }
    }
}
