using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public partial class DialogueUI : UIBase
{
    // 대화창 호출 
    private void Start()
    {
        prevButton.interactable = false;
        UpdateUI();
        // 여기서 데이터 넘겨받기.
        Debug.Log("다이얼로그 UI를 찾았습니다.");
    }

    [SerializeField] private ChapterManager chapterManager;
    [SerializeField] private TextMeshProUGUI nameTxt;
    [SerializeField] private TextMeshProUGUI lineTxt;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button prevButton;

    [SerializeField] private GameObject optionUIPanel;
    [SerializeField] private TextMeshProUGUI questionTxt;
    [SerializeField] private Button[] optionButtons;

    private int currentDialogueIndex = 0;
    private int currentLineIndex = 0;

    private void UpdateUI()
    {
        if (ChapterManager.Instance.dialogues == null || ChapterManager.Instance.dialogues.Length == 0)
        {
            // 대화 데이터 없을 시에
            return;
        }

        if (currentDialogueIndex < 0 || currentDialogueIndex >= ChapterManager.Instance.dialogues.Length)
        {
            // 대화 인덱스가 범위를 벗어났을 때
            return;
        }

        if (currentLineIndex < 0 || currentLineIndex >= ChapterManager.Instance.dialogues[currentDialogueIndex].lines.Length)
        {
            // 라인 인덱스가 덤위를 벗어갔을 때
            return;
        }

        Dialogue currentDialogue = ChapterManager.Instance.dialogues[currentDialogueIndex];

        if (currentDialogue.isOption)
        {
            Debug.Log(currentDialogueIndex);
            ShowOption(currentDialogue);
        }
        else
        {
            nameTxt.text = currentDialogue.name;
            lineTxt.text = currentDialogue.lines[currentLineIndex];
        }

        prevButton.interactable = (currentDialogueIndex > 0 || currentLineIndex > 0);
    }
    public void ShowNextLine()
    {
        currentLineIndex++;

        if (ChapterManager.Instance.dialogues[currentDialogueIndex].isOption)
        {
            ShowOption(ChapterManager.Instance.dialogues[currentDialogueIndex]);
            return;
        }

        if (currentLineIndex >= ChapterManager.Instance.dialogues[currentDialogueIndex].lines.Length)
        {
            currentDialogueIndex++;
            currentLineIndex = 0;

            if (currentDialogueIndex >= ChapterManager.Instance.dialogues.Length)
            {
                // 대화 종료 -> 안전하게 처리되게 대화 인덱스와 라인 인덱스 조절
                currentDialogueIndex = ChapterManager.Instance.dialogues.Length - 1;
                currentLineIndex = ChapterManager.Instance.dialogues[currentDialogueIndex].lines.Length - 1;
                return;
            }
        }

        UpdateUI();
    }
    private void ShowOption(Dialogue dialogue)
    {
        // OptionPanel을 직접 표시하도록 변경
        var optionPanel = UIManager.Instance.Show<OptionPanel>();
        if (optionPanel != null)
        {
            optionPanel.SetupOptions(dialogue.Question, dialogue.Options, OnOptionSelected);
        }
    }
    private void OnOptionSelected(int resultIndex)
    {
        Debug.Log($"선택된 결과: {resultIndex}");
        //currentDialogueIndex = resultIndex;
        //currentLineIndex = 0;
        UpdateUI();
    }

    public void ShowPreviousLine()
    {
        if (currentLineIndex > 0)
        {
            currentLineIndex--;
        }
        else if (currentDialogueIndex > 0)
        {
            currentDialogueIndex--;
            currentLineIndex = ChapterManager.Instance.dialogues[currentDialogueIndex].lines.Length - 1;
        }
        else
        {
            return;
        }

        UpdateUI();
    }

    public void PopUpMenu(int Index)
    {
        switch (Index)
        {
            case 0:
                UIManager.Instance.Show<Popup001>();
                break;
            case 1:
                UIManager.Instance.Show<Popup002>();
                break;
            case 2:
                UIManager.Instance.Show<Popup003>();
                break;
        }
    }
}


