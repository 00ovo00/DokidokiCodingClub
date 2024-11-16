using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonBase<UIManager>
{
    [Header("Dialog Elements")] // 대화창
    [SerializeField] private GameObject _dialogUI;
    [SerializeField] private TextMeshProUGUI _lineTxt;
    [SerializeField] private TextMeshProUGUI _nameTxt;
    [SerializeField] private Button _nextBtn;
    
    [Header("Option Elements")] // 선택창
    [SerializeField] private GameObject _optionUI;
    [SerializeField] private TextMeshProUGUI _questionTxt; 
    [SerializeField] private Button _optionABtn;
    [SerializeField] private Button _optionBBtn; 
    [SerializeField] private Button _optionCBtn;

    [Header("Score Elements")]  // 호감도 변동 알림
    [SerializeField] private GameObject _scoreAlert;
    [SerializeField] private TextMeshProUGUI _scoreChangeTxt;

    private int _curDialogID = 0;   // 현재 진행 중인 대화의 ID, TODO: 스토리 나오면 enum으로 관리
    private int _curID = 0;         // 현재 행의 ID(엑셀 시트 상의 행), 포인터 변수처럼 사용중
    private bool _isResult = false; // 답변에 대한 결과 상태인지 체크하는 플래그(for 분기점)

    private void Start()
    {
        ShowDialogue();
    }

    private void ShowDialogue()
    {
        // 엑셀 시트상 현재 진행되어야 할 행으로 대화 데이터 가져오기
        DialogueData data = DataManager.Instance.Dialogs[_curID];
        
        if (!data.IsOption) // 선택지 항목이 아니면
        {
            _dialogUI.SetActive(true);  // 대화창 활성화
            _optionUI.SetActive(false); // 선택창 비활성화
            
            // 공략 대상과 대사 텍스트만 화면에 출력
            _nameTxt.text = data.Speaker;
            _lineTxt.text = data.Line;
            return;
        }
        
        // 선택지 항목이면
        _dialogUI.SetActive(false); // 대화창 비활성화
        _optionUI.SetActive(true);  // 선택창 활성화

        // 선택창 텍스트 세팅
        _questionTxt.text = data.Question;
        _optionABtn.GetComponentInChildren<TextMeshProUGUI>().text = data.OptionA;
        _optionBBtn.GetComponentInChildren<TextMeshProUGUI>().text = data.OptionB;
        _optionCBtn.GetComponentInChildren<TextMeshProUGUI>().text = data.OptionC;

        // 버튼에 등록된 이벤트 모두 삭제(비정상 종료 or 예상치 못한 오류 예방)
        _optionABtn.onClick.RemoveAllListeners();
        _optionBBtn.onClick.RemoveAllListeners();
        _optionCBtn.onClick.RemoveAllListeners();

        // 버튼에 각 선택지 별 이벤트 등록, 두번째 인자(int)는 엑셀에서 넘어가는 행의 개수 
        _optionABtn.onClick.AddListener(() => SelectOption(data.Result1, 1));
        _optionBBtn.onClick.AddListener(() => SelectOption(data.Result2, 2));
        _optionCBtn.onClick.AddListener(() => SelectOption(data.Result3, 3));
    }

    // NextBtn에 OnClick Event로 등록된 메소드
    public void OnNextButton()
    {
        if (_isResult)  // 현재 화면에 띄워진 대사가 결과이면
        {
            _scoreAlert.SetActive(false);   // 점수 변동창 비활성화
            _isResult = false;
            int nextID = DataManager.Instance.Dialogs[_curID].NextDialogID; // 다음에 시작될 대화 ID 정보 가져오기
            StartNewConversation(nextID);   // 새로운 대화 시작하도록 호출
            if (nextID == -1)   // 마지막 대화이면
            {
                // TODO: EndingScene으로 넘어가도록 구현하기
                UnityEditor.EditorApplication.isPlaying = false;    // 에디터 플레이 종료
            }
        }
        _curID++;
        ShowDialogue();
    }

    // 각 버튼에 이벤트로 등록된 메소드
    private void SelectOption(int result, int num)
    {
        DataManager.Instance.Score += result;
        _isResult = true;   // 답변에 대한 결과 보여줄 상태로 전환
        _curID += num;  // 어떤 버튼 선택했는지에 따라 스킵할 행의 개수 조정

        if (result != 0)    // 점수 변동이 있으면
        {
            // 점수 변동 텍스트 세팅하고 활성화
            _scoreChangeTxt.text = result > 0 ? $"호감도 +{result}" : $"호감도 {result}";
            _scoreAlert.SetActive(true);
        }
        
        ShowDialogue();
    }

    private void StartNewConversation(int dialogID)
    {
        // 엑셀 시트 전체에서 인자로 들어온 대화 ID의 dialogueData 찾아오기
        DialogueData dialogue = DataManager.Instance.Dialogs.Find(d => d.DialogID == dialogID && d.ID == 0);
        if (dialogue != null)
        {
            // 현재 사용할 행을 새로운 대화의 시작 위치로 설정
            // (-1은 엑셀 시트의 첫행이 열에 대한 정보이므로 제외하려고)
            _curID = DataManager.Instance.Dialogs.IndexOf(dialogue) - 1;
            ShowDialogue();
        }
        else    // 다음 대화ID 못찾으면 오류이므로 중단시키기
        {
            Debug.LogError($"Dialog with ID {dialogID} not found.");
        }
    }
}
