using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : UIBase
{
    [SerializeField] private Button[] optionButtons;

    public void SetupOptions(string[] options, System.Action<int> onOptionSelected)
    {
        for (int i = 0; i < optionButtons.Length; i++) // 옵션에 있는 대사를 하나씩 넣어서 출력 
        {
            if (i < options.Length)
            {
                optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = options[i];
                int optionIndex = i; 
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => onOptionSelected(optionIndex));
            }
        }
    }
    public override void Opened(params object[] param)
    {
        // 데이터를 주고 받을 일이 잇을 때 사용
        base.Opened(param);
    }

    public void ShowMain()
    {
        // 자기 자신은 종료하고 필요한 행위 구현
        // 선택이 확정되었을 때 처리 작성
        Hide();
    }
}
