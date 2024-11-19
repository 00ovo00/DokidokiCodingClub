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

    [Tooltip("현재 챕터")]
    [SerializeField] private ChapterState _currentChapter;
    [SerializeField] private int _currentChapterIndex;

    public Action onEnterChapter;
    public Action onCurrentChapter;
    public Action onExitChapter;

    private void Start()
    {
        _currentChapterIndex = 0;
        EnterChapter(_currentChapterIndex);
    }
    private void EnterChapter(int chapterIndex)
    {
        Debug.Log($"{_currentChapter}가 실행되었습니다.");

        _currentChapter = (ChapterState)chapterIndex;
        //DataManager.Instance.LoadCSV("storydata"); // 나중에 현재 상태에 맞는 CSV 파일 
        onEnterChapter?.Invoke();
        CurrentChapter();
    }
    private void CurrentChapter()
    {
        // 대사 출력하
        Debug.Log($"{_currentChapter}가 진행 중입니다.");

        onCurrentChapter?.Invoke();
        ExitChapter();
    }
    private void ExitChapter()
    {
        Debug.Log($"{_currentChapter}가 종료되었습니다.");

        onExitChapter?.Invoke();
        _currentChapterIndex++;

        if (_currentChapterIndex >= System.Enum.GetValues(typeof(ChapterState)).Length)
        {
            Debug.Log("모든 챕터가 완료되었습니다.");
            // 씬 매니저로 마지막 엔딩 씬 실행  
            return;
        }

        // 사용자가 버튼을 눌렀을 때
        EnterChapter(_currentChapterIndex);
    }
}
