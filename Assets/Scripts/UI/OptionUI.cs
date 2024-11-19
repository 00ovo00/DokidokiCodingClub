using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : UIBase
{
    [SerializeField] private ChapterManager chapterManager;
    [SerializeField] private TextMeshProUGUI _questionTxt; 
    
    [SerializeField] private Button _optionBtn0;
    [SerializeField] private Button _optionBtn1; 
    [SerializeField] private Button _optionBtn2;
    
    private int _currentDialogueIndex;  // 현재 대화의 인덱스(ID)

    public override void Opened(params object[] param)
    {
        // 인자로 받은 지점부터 대화 시작하도록 설정
        if (param.Length > 0 && param[0] is int dialogueIndex)
        {
            _currentDialogueIndex = dialogueIndex;
        }
        ShowOption();
    }

    private void ShowOption()
    {
        // 대화 리스트에서 현재 대화 인덱스와 일치하는 대화 정보 가져오기
        // TODO: 성능 부하 낮출 다른 방법 찾아보기
        var dialogue = ChapterManager.Instance.dialogues.FirstOrDefault(d => d.ID == _currentDialogueIndex);
        // 대화를 찾지 못하거나 대화 타입이 옵션이 아닐 경우 예외처리
        if (dialogue == null || !dialogue.isOption) return;

        // 옵션창 텍스트 세팅
        _questionTxt.text = dialogue.Question;
        _optionBtn0.GetComponentInChildren<TextMeshProUGUI>().text = dialogue.Options[0];
        _optionBtn1.GetComponentInChildren<TextMeshProUGUI>().text = dialogue.Options[1];
        _optionBtn2.GetComponentInChildren<TextMeshProUGUI>().text = dialogue.Options[2];
    }
}
