using System;
using System.Linq;
using System.Collections;
using TMPro;
using UnityEngine;
 using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public partial class DialogueUI : UIBase
{
    // 캐릭터별 호감도 관리 딕셔너리
    private Dictionary<string, int> affectionLevels = new Dictionary<string, int>();

    // 대화창 호출 
    private void Start()
    {
        prevButton.interactable = false;
        ChapterManager.Instance.onEnterChapter -= UpdateUI;
        ChapterManager.Instance.onEnterChapter += UpdateUI;

        currentDialogueIndex = 0;
        currentLineIndex = 0;

        // 호감도 초기화 (캐릭터 이름, 초기 호감도)
        affectionLevels["안혜린 매니저"] = 30;
        affectionLevels["이성언 튜터"] = 30;
        affectionLevels["김재경 튜터"] = 30;

        ChapterManager.Instance.ChangeArt?.Invoke(currentDialogueIndex);
        UpdateUI();
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
    private Coroutine typingCoroutine;

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
            //lineTxt.text = currentDialogue.lines[currentLineIndex];
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            typingCoroutine = StartCoroutine(TypeLine(currentDialogue.lines[currentLineIndex]));
        }
        else
        {
            if (currentDialogue.isOption) // 선택지가 있는 대화일 경우 
            {
                nameTxt.text = currentDialogue.name;
                //lineTxt.text = currentDialogue.lines[currentLineIndex];
                if (typingCoroutine != null)
                {
                    StopCoroutine(typingCoroutine);
                }
                typingCoroutine = StartCoroutine(TypeLine(currentDialogue.lines[currentLineIndex]));
                ShowOption(currentDialogue);
            }
            else // 일반 대화일 경우 
            {
                nameTxt.text = currentDialogue.name;
                //lineTxt.text = currentDialogue.lines[currentLineIndex];
                if (typingCoroutine != null)
                {
                    StopCoroutine(typingCoroutine);
                }
                typingCoroutine = StartCoroutine(TypeLine(currentDialogue.lines[currentLineIndex]));
            }
        }

        prevButton.interactable = (currentDialogueIndex > 0 || currentLineIndex > 0); // 첫번째 대화나 첫번째 라인이 아니면 활성화 

    }
    public void ShowNextLine()
    {
        currentLineIndex++;

        if (currentLineIndex >= ChapterManager.Instance.dialogues[currentDialogueIndex].lines.Length) // 현재 대화의 라인 인덱스를 초과했을 때
        {
            // 챕터 7에서 호감도에 따른 분기 처리
            if (ChapterManager.Instance.CurrentChapterIndex == 6)//
            {
                HandleChapter7();
                ChapterManager.Instance.ChangeArt?.Invoke(currentDialogueIndex);
                return;
            }
            if(ChapterManager.Instance.CurrentChapterIndex == 7)
            {
                ChapterManager.Instance.dialogues = EndingManager.Instance.endingDialogues;
                currentDialogueIndex = 0;
                currentLineIndex = 0;
                ChapterManager.Instance.ChangeArt?.Invoke(currentDialogueIndex);
                UpdateUI();

                ChapterManager.Instance.ExitChapter();
                return;

            }

            // 현재 대화가 선택지가 없고 다음으로 이어지는 대화가 있는 경우
            if (!ChapterManager.Instance.dialogues[currentDialogueIndex].isOption && ChapterManager.Instance.dialogues[currentDialogueIndex].Param.Length > 0)
            {
                currentDialogueIndex = ChapterManager.Instance.dialogues[currentDialogueIndex].Param[0];
                ChapterManager.Instance.ChangeArt?.Invoke(currentDialogueIndex);
                currentLineIndex = 0;
            }
            else if (currentDialogueIndex < ChapterManager.Instance.dialogues.Length - 1)
            {
                // 마지막 대화가 아닌 경우 다음 대화로 이동
                currentDialogueIndex++;
                ChapterManager.Instance.ChangeArt?.Invoke(currentDialogueIndex);
                currentLineIndex = 0;
            }
            else
            {
                // 마지막 대사 처리 후 챕터 종료
                currentDialogueIndex = 0;
                currentLineIndex = 0;
                ChapterManager.Instance.ExitChapter();
                ChapterManager.Instance.ChangeArt?.Invoke(currentDialogueIndex);
                return;
            }
        }
        //if (currentLineIndex >= ChapterManager.Instance.dialogues[currentDialogueIndex].lines.Length) // 대사 인덱스가 있다면 
        //{
        //    if (ChapterManager.Instance.dialogues[currentDialogueIndex].isOption == false && ChapterManager.Instance.dialogues[currentDialogueIndex].Param.Length > 0)
        //    {
        //        currentDialogueIndex = ChapterManager.Instance.dialogues[currentDialogueIndex].Param[0];
        //        //ChapterManager.Instance.ChangeArt?.Invoke(currentDialogueIndex);
        //        currentLineIndex = 0;
        //    }

        //    if (currentDialogueIndex >= ChapterManager.Instance.dialogues.Length - 1)
        //    {
        //        currentDialogueIndex = 0;
        //        currentLineIndex = 0;
        //        ChapterManager.Instance.ExitChapter();
        //        return;
        //    }
        //}

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
        // Results 배열이 있는지 확인하고 처리
        if (ChapterManager.Instance.dialogues[currentDialogueIndex].Results != null && resultIndex < ChapterManager.Instance.dialogues[currentDialogueIndex].Results.Length)
        {
            string resultName = ChapterManager.Instance.dialogues[currentDialogueIndex].Target[resultIndex];  // 호감작 인물 이름
            int resultID = ChapterManager.Instance.dialogues[currentDialogueIndex].Results[resultIndex]; //호감도 수치
            if (resultName == "All")
            {
                foreach (var key in affectionLevels.Keys.ToList())
                {
                    affectionLevels[key] += resultID;
                }
            }
            else
            {
                affectionLevels[resultName] += resultID; // 수치 반영
            }

            var resultPopup = UIManager.Instance.Show<Popup004>();
            resultPopup.SetUpResults(resultID);
        }


        //int resultID = ChapterManager.Instance.dialogues[currentDialogueIndex].Results[resultIndex];

        UIManager.Instance.Hide<OptionPanel>();
        //var resultPopup = UIManager.Instance.Show<Popup004>();
       // resultPopup.SetUpResults(resultID);

        if (nextDialogueID >= 0 && nextDialogueID < ChapterManager.Instance.dialogues.Length)
        {
            currentDialogueIndex = nextDialogueID;
            currentLineIndex = 0;
            ChapterManager.Instance.ChangeArt?.Invoke(currentDialogueIndex);
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

    private IEnumerator TypeLine(string line)
    {
        lineTxt.text = "";
        foreach (char letter in line.ToCharArray())
        {
            lineTxt.text += letter;
            yield return new WaitForSeconds(0.05f); // 타이핑 속도 조절
        }
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

    // 챕터 7에서의 분기 처리 메서드
    private void HandleChapter7()
    {
        var sortedAffectionLevels = affectionLevels.OrderByDescending(x => x.Value).ToList();
        Debug.Log(sortedAffectionLevels);
        string character = sortedAffectionLevels[0].Key;


         Debug.Log(character);

        if (affectionLevels[character] >= 50)
        {
            currentDialogueIndex = 2;
           
            
        }
        else
        {
            currentDialogueIndex = 1;
        }

        EndingManager.Instance.EnterEnding(character);
        ChapterManager.Instance.ExitChapter();
        currentLineIndex = 0;
        UpdateUI();
    }
    // 호감도가 가장 높은 캐릭터의 엔딩을 보여주는 메서드
    private void ShowHappyEnding()
    {

        var highestAffectionCharacter = affectionLevels.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
        EndingManager.Instance.EnterEnding(highestAffectionCharacter);

       
    }
}
