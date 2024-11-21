using System;
using System.Linq;
using TMPro;
using UnityEngine;
 using UnityEngine.UI;

public partial class DialogueUI : UIBase
{
    // 대화창 호출 
    private void Start()
    {
        prevButton.interactable = false;
        ChapterManager.Instance.onEnterChapter -= UpdateUI;
        ChapterManager.Instance.onEnterChapter += UpdateUI;
    }

    [Tooltip("캐릭터 이름이 표시됩니다.")]
    [SerializeField] private TextMeshProUGUI nameTxt;
    [Tooltip("대사 내용이 표시됩니다.")]
    [SerializeField] private TextMeshProUGUI lineTxt;
    [Tooltip("클릭 시 다음 대사로 넘어갑니다.")]
    [SerializeField] private Button nextButton;
    [Tooltip("클릭 시 이전 대사로 돌아갑니다.")]
    [SerializeField] private Button prevButton;

    [Tooltip("선택지를 보여줄 패널입니다.")]
    [SerializeField] private GameObject optionUIPanel;
    [Tooltip("선택지의 질문을 보여줍니다.")]
    [SerializeField] private TextMeshProUGUI questionTxt;
    [Tooltip("선택지를 배열로 저장합니다.")]
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
        if (currentDialogue.lines.Length - 1 > currentLineIndex)
        {
            nameTxt.text = currentDialogue.name;
            lineTxt.text = currentDialogue.lines[currentLineIndex];
        }
        else
        {
            if (currentDialogue.isOption) // 선택지가 있는 대화일 경우 
            {
                nameTxt.text = currentDialogue.name;
                lineTxt.text = currentDialogue.lines[currentLineIndex];
                ShowOption(currentDialogue);
            }
            else // 일반 대화일 경우 
            {
                nameTxt.text = currentDialogue.name;
                lineTxt.text = currentDialogue.lines[currentLineIndex];
            }
        }

        prevButton.interactable = (currentDialogueIndex > 0 || currentLineIndex > 0); // 첫번째 대화나 첫번째 라인이 아니면 활성화 
    }
    public void ShowNextLine()
    {
        currentLineIndex++;

        if (currentLineIndex >= ChapterManager.Instance.dialogues[currentDialogueIndex].lines.Length) // 대사 인덱스가 있다면 
        {
            if (ChapterManager.Instance.dialogues[currentDialogueIndex].isOption == false && ChapterManager.Instance.dialogues[currentDialogueIndex].Param.Length > 0)
            {
                currentDialogueIndex = ChapterManager.Instance.dialogues[currentDialogueIndex].Param[0];
                ChapterManager.Instance.ChangeArt?.Invoke(currentDialogueIndex);
                currentLineIndex = 0;
            }

            if (currentDialogueIndex >= ChapterManager.Instance.dialogues.Length - 1)
            {
                currentDialogueIndex = 0;
                currentLineIndex = 0;
                ChapterManager.Instance.ExitChapter();
                return;
            }
        }

        UpdateUI(); 
    }
    private void ShowOption(Dialogue dialogue)

    {
        //currentDialogueIndex++;
        //ChapterManager.Instance.ChangeArt?.Invoke(currentDialogueIndex);
        Debug.Log(currentDialogueIndex);

        var optionPanel = UIManager.Instance.Show<OptionPanel>();
        if (optionPanel != null) 
        {
            Debug.Log(currentDialogueIndex);
            optionPanel.SetupOptions(dialogue.Options, OnOptionSelected); 
        }
    }
    public void OnOptionSelected(int resultIndex)
    {
      
        int nextDialogueID = ChapterManager.Instance.dialogues[currentDialogueIndex].Param[resultIndex];
        int resultID = ChapterManager.Instance.dialogues[currentDialogueIndex].Results[resultIndex];

        UIManager.Instance.Hide<OptionPanel>();
        var resultPopup = UIManager.Instance.Show<Popup004>();
        resultPopup.SetUpResults(resultID);

        if (nextDialogueID >= 0 && nextDialogueID < ChapterManager.Instance.dialogues.Length)
        {
            currentDialogueIndex = nextDialogueID;
            currentLineIndex = 0; 
        }
        else
        {
            Debug.LogError($"잘못된 대화 ID: {nextDialogueID}");
        }

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
