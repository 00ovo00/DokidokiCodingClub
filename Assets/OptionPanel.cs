using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : UIBase
{
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private Button[] optionButtons;

    public void SetupOptions(string question, string[] options, System.Action<int> onOptionSelected)
    {
        questionText.text = question;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i < options.Length)
            {
                //optionButtons[i].gameObject.SetActive(true);
                optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = options[i];
                int optionIndex = i; // Capture the current value of i
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => onOptionSelected(optionIndex));
            }
            else
            {
                //optionButtons[i].gameObject.SetActive(false);
            }
        }
    }
    public override void Opened(params object[] param)
    {
        // 데이터를 주고 받을 일이 잇을 때 사용
        base.Opened(param);

        if (param.Length > 0 && param[0] is string description)
        {
            questionText.text = description;
        }
    }

    public void ShowMain()
    {
        // 자기 자신은 종료하고 필요한 행위 구현
        // 선택이 확정되었을 때 처리 작성
        Hide();
    }
}
