using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Transform canvas;

    public static UIManager Instance;
    public static float ScreenWidth = 1920;
    public static float ScreenHeight = 1080;

    private Dictionary<string, UIBase> uiDictionary = new Dictionary<string, UIBase>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public T Show<T>(params object[] param) where T : UIBase
    {
        string uiName = typeof(T).ToString();
        if (uiDictionary.TryGetValue(uiName, out UIBase existingUI))
        {
            existingUI.Opened(param);
            return (T)existingUI;
        }
        UIBase go = Resources.Load<UIBase>("UI/" + uiName);
        if (go == null) // 프리펩이 제대로 로드되지 않았을 때 오류 방지
        {
            return null;
        }
        var ui = Load<T>(go, uiName);
        uiDictionary.Add(uiName, ui);
        ui.Opened(param);

        Debug.Log(uiName);
        return (T)ui;
    }

    private T Load<T>(UIBase prefab, string uiName) where T : UIBase
    {
        GameObject newUIObject = Instantiate(prefab.gameObject, canvas);
        newUIObject.name = uiName;

        UIBase ui = newUIObject.GetComponent<UIBase>();
        ui.canvas = canvas.GetComponent<Canvas>();
        ui.canvas.sortingOrder = uiDictionary.Count;

        return (T)ui;
    }

    public void Hide<T>() where T : UIBase
    {
        string uiName = typeof(T).ToString();

        Hide(uiName);
    }

    public void Hide(string uiName)
    {
        if (uiDictionary.TryGetValue(uiName, out UIBase go))
        {
            uiDictionary.Remove(uiName);
            Destroy(go.gameObject);
        }
    }
}
