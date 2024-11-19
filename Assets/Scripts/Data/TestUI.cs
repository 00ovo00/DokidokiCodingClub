using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestUI : MonoBehaviour
{
    [SerializeField] private ChapterManager chapterManager;
    [SerializeField] private TextMeshProUGUI nameTxt;
    [SerializeField] private TextMeshProUGUI lineTxt;
    [SerializeField] private Button nextButton;

    private int currentDialogueIndex = 0; 
    private int currentLineIndex = 0;
   
    private void UpdateUI()
    {
        nameTxt.text = ChapterManager.Instance.dialogues[currentDialogueIndex].name;
        lineTxt.text = ChapterManager.Instance.dialogues[currentDialogueIndex].lines[currentLineIndex];
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
                Debug.Log("모든 대화가 종료되었습니다.");
                return;
            }
        }

        UpdateUI();
    }
}







