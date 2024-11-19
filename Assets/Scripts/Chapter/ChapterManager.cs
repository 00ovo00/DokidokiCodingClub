using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterManager : MonoBehaviour
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

    private Dictionary<ChapterState, string> _chapterCSV;

    [Tooltip("���� é��")]
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
        Debug.Log($"{_currentChapter}�� ����Ǿ����ϴ�.");

        _currentChapter = (ChapterState)chapterIndex;
        //DataManager.Instance.LoadCSV("storydata"); // ���߿� ���� ���¿� �´� CSV ���� 
        onEnterChapter?.Invoke();
        CurrentChapter();
    }
    private void CurrentChapter()
    {
        // ��� �����
        Debug.Log($"{_currentChapter}�� ���� ���Դϴ�.");

        onCurrentChapter?.Invoke();
        ExitChapter();
    }
    private void ExitChapter()
    {
        Debug.Log($"{_currentChapter}�� ����Ǿ����ϴ�.");

        onExitChapter?.Invoke();
        _currentChapterIndex++;

        if (_currentChapterIndex >= System.Enum.GetValues(typeof(ChapterState)).Length)
        {
            Debug.Log("��� é�Ͱ� �Ϸ�Ǿ����ϴ�.");
            // �� �Ŵ����� ������ ���� �� ����  
            return;
        }

        // ����ڰ� ��ư�� ������ ��
        EnterChapter(_currentChapterIndex);
    }
}
