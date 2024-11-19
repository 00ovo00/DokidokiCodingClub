using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogUI : UIBase
{
    [SerializeField] private ChapterManager chapterManager;
    [SerializeField] private TextMeshProUGUI nameTxt;
    [SerializeField] private TextMeshProUGUI lineTxt;
    [SerializeField] private Button nextButton;

    private int currentDialogueIndex = 0;   // 현재 대화의 인덱스(ID)
    private int currentLineIndex = 0;       // 현재 대사의 인덱스(Line[])
    private bool isOptionActive = false;    // 선택창 활성 상태

    private void OnEnable()
    {
        nextButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(ShowNextLine);
    }

    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        var dialogue = ChapterManager.Instance.dialogues[currentDialogueIndex];

        if (dialogue.isOption)  // 현재 대화가 선택 지점이면
        {
            isOptionActive = true;  // 선택창 활성 상태로 설정
            UIManager.Instance.Show<OptionUI>(currentDialogueIndex);    // 현 대화 지점에서 선택창 띄우기
            return;
        }
        // 현재 대화가 선택 지점아니면 이름과 대사만 변경
        nameTxt.text = dialogue.name;
        lineTxt.text = dialogue.lines[currentLineIndex];
    }
    
    public void ShowNextLine()
    {
        if (isOptionActive) return; // 선택창 활성 시에 다음 버튼 무효화
        
        currentLineIndex++; // 다음 대사로 넘어가기
        
        // 현재 대화에서 더이상 대사가 없으면
        if (currentLineIndex >= ChapterManager.Instance.dialogues[currentDialogueIndex].lines.Length)
        {
            currentDialogueIndex++; // 다음 대화로 넘어가고
            currentLineIndex = 0;   // 대사 인덱스 초기화

            if (currentDialogueIndex >= ChapterManager.Instance.dialogues.Length)   // 해당 챕터 대화 종료
            {
                Debug.Log("모든 대화가 종료되었습니다.");
                return;
            }
        }
        UpdateUI();
    }
}