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
        for (int i = 0; i < optionButtons.Length; i++) // �ɼǿ� �ִ� ��縦 �ϳ��� �־ ��� 
        {
            if (i < options.Length)
            {
                optionButtons[i].gameObject.SetActive(true);
                optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = options[i];
                int optionIndex = i; 
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => onOptionSelected(optionIndex));
            }
            else
            {
                optionButtons[i].gameObject.SetActive(false);
            }
            
        }
    }
    public override void Opened(params object[] param)
    {
        // �����͸� �ְ� ���� ���� ���� �� ���
        base.Opened(param);
    }

    public void ShowMain()
    {
        // �ڱ� �ڽ��� �����ϰ� �ʿ��� ���� ����
        // ������ Ȯ���Ǿ��� �� ó�� �ۼ�
        Hide();
    }
}
