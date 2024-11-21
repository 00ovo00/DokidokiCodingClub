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
        UpdateUI(); // 챕터 매니저에서 실행으로 변경 
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
            // 라인 인덱스가 덤위를 벗어갔을 때 => 종료가 아니라 챕터 인덱스를 늘려줘야 됨 
            return;
        }

        Dialogue currentDialogue = ChapterManager.Instance.dialogues[currentDialogueIndex];

        if (currentDialogue.isOption) // 선택지가 있는 대화일 경우 
        {
            Debug.Log(currentDialogueIndex);
            ShowOption(currentDialogue);
        }
        else // 일반 대화일 경우 
        {
            nameTxt.text = currentDialogue.name;
            lineTxt.text = currentDialogue.lines[currentLineIndex];
        }

        prevButton.interactable = (currentDialogueIndex > 0 || currentLineIndex > 0); // 첫번째 대화나 첫번째 라인이 아니면 활성화 
    }
    public void ShowNextLine()
    {
        currentLineIndex++; 

        if (currentLineIndex >= ChapterManager.Instance.dialogues[currentDialogueIndex].lines.Length)
        {
            currentDialogueIndex++; 
            currentLineIndex = 0;  

            if (currentDialogueIndex >= ChapterManager.Instance.dialogues.Length) 
            {
                currentDialogueIndex = ChapterManager.Instance.dialogues.Length - 1;
                currentLineIndex = ChapterManager.Instance.dialogues[currentDialogueIndex].lines.Length - 1;
                return;
            }
        }

        UpdateUI(); 
    }
    private void ShowOption(Dialogue dialogue)
    {
        Debug.Log(currentDialogueIndex);

        var optionPanel = UIManager.Instance.Show<OptionPanel>();
        if (optionPanel != null) 
        {
            Debug.Log(currentDialogueIndex);
            optionPanel.SetupOptions(dialogue.Question, dialogue.Options, OnOptionSelected); // 실행합니다 
            // 인덱스 값만 보내주고 그쪽에서 다 해결 OnOptionSelected
        }
    }
    public void OnOptionSelected(int resultIndex)
    {
        // 만약 옵션의 0번을 눌렀다 0 =  D이거에 맞는 결과, 파라미터 인덱스 ==  
        // 호감도에 따른 팝업 띄워줘야 해요 
        // 옵션 판넬 끄고 호감도 팝업 띄워줬다가 코루틴 돌려서 3초 뒤에 삭제되기 

        int nextDialogueID = ChapterManager.Instance.dialogues[currentDialogueIndex].Param[resultIndex];
        if (nextDialogueID >= 0 && nextDialogueID < ChapterManager.Instance.dialogues.Length)
        {
            currentDialogueIndex = nextDialogueID;
            currentLineIndex = 0; // 대화의 첫 번째 라인으로 이동
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


