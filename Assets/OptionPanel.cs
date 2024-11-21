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
        // �����͸� �ְ� ���� ���� ���� �� ���
        base.Opened(param);

        if (param.Length > 0 && param[0] is string description)
        {
            questionText.text = description;
        }
    }

    public void ShowMain()
    {
        // �ڱ� �ڽ��� �����ϰ� �ʿ��� ���� ����
        // ������ Ȯ���Ǿ��� �� ó�� �ۼ�
        Hide();
    }
}
