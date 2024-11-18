using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameTxt;
    [SerializeField] private TextMeshProUGUI lineTxt;
    [SerializeField] private Button nextButton;

    public Dialogue[] dialogues;
    private int currentDialogueIndex = 0; 
    private int currentLineIndex = 0; 

    private void Start()
    {
        DialogueData dialogueData = DataManager.Instance.Parse("TestData");
        if (dialogueData != null)
        {
            dialogues = dialogueData.dialogues;
            UpdateUI();
        }
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







