using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestUI : MonoBehaviour
{
    [SerializeField] private ChapterManager chapterManager;
    [SerializeField] private TextMeshProUGUI nameTxt;
    [SerializeField] private TextMeshProUGUI lineTxt;
    [SerializeField] private Button nextButton;

    public Dialogue[] dialogues;
    private int currentDialogueIndex = 0; 
    private int currentLineIndex = 0;

    private void Start()
    {
        if (chapterManager == null)
        {
            chapterManager = FindObjectOfType<ChapterManager>();
        }

        dialogues = chapterManager.GetDialogues();
        if (dialogues == null || dialogues.Length == 0)
        {
            Debug.LogError("대화 데이터가 비어 있습니다!");
            return;
        }

        chapterManager.onEnterChapter += UpdateUI;

    }

    private void UpdateUI()
    {
        nameTxt.text = dialogues[currentDialogueIndex].name;
        lineTxt.text = dialogues[currentDialogueIndex].lines[currentLineIndex];
    }
    public void ShowNextLine()
    {
        currentLineIndex++;

        if (currentLineIndex >= dialogues[currentDialogueIndex].lines.Length)
        {
            currentDialogueIndex++;
            currentLineIndex = 0;

            if (currentDialogueIndex >= dialogues.Length)
            {
                Debug.Log("모든 대화가 종료되었습니다.");
                return;
            }
        }

        UpdateUI();
    }
}







