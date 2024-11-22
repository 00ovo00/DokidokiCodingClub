using UnityEngine;
using System;
using static MainScene;
using System.Collections;
using UnityEngine.UI;

public class NoneUI : UIBase
{
    [SerializeField] private Image backgroundGroup;
    [SerializeField] private Image characterGroup;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private Image backgroundImage; // 추가: 배경 이미지를 표시할 UI Image
    [SerializeField] private Image characterImage; // 추가: 캐릭터 이미지를 표시할 UI Image

    private FadeController fadeController;
    private string currentCharacterImageName = null; // 현재 캐릭터 이미지 이름을 저장하기 위한 변수

    // 백그라운드 이미지 표시 
    private void Start()
    {
        fadeController = gameObject.AddComponent<FadeController>();

        ChapterManager.Instance.onFadeEffect += OnChangeBackground;
        ChapterManager.Instance.onFadeEffect?.Invoke();

        Debug.Log("백그라운드 UI가 호출되었습니다.");
        MainScene.Instance.OnStateChanged += HandleStateChange;
        StartCoroutine(ChangeStateToDefaultAfterDelay());

        // ChapterManager의 ChangeArt 이벤트 구독
        ChapterManager.Instance.ChangeArt += ChangeArt;
    }

    private void OnDestroy()
    {
        // 이벤트 구독 해제
        MainScene.Instance.OnStateChanged -= HandleStateChange;
        ChapterManager.Instance.ChangeArt -= ChangeArt;
    }

    private void OnChangeBackground()
    {

    }

    public IEnumerator FadeBackGround(int dialogueIndex)
    {
        if (dialogueIndex == 0)
        {
            
            // 챕터가 시작할 때는 배경 페이드 인
            ChangeBackGroundImage(dialogueIndex);
            yield return fadeController.FadeIn(backgroundGroup, fadeDuration);
        }
        else if (dialogueIndex >= ChapterManager.Instance.dialogues.Length)
        {
            // 챕터가 끝날 때는 배경 페이드 아웃
            yield return fadeController.FadeOut(backgroundGroup, fadeDuration);
        }
        else
        {
            // 그 외의 경우 페이드 아웃 -> 이미지 변경 -> 페이드 인
            yield return fadeController.FadeOut(backgroundGroup, fadeDuration);
            ChangeBackGroundImage(dialogueIndex);
            yield return fadeController.FadeIn(backgroundGroup, fadeDuration);
        }
    }

    public IEnumerator FadeCharacter(int dialogueIndex)
    {
        string characterImageName = ChapterManager.Instance.dialogues[dialogueIndex].CharImage != null && ChapterManager.Instance.dialogues[dialogueIndex].CharImage.Length > 0
            ? ChapterManager.Instance.dialogues[dialogueIndex].CharImage[0] : null;

        if (characterImageName == "player")
        {
            // 'player' 캐릭터는 페이드 인/아웃 없이 표시
            ChangeCharacterImage(dialogueIndex);
        }
        else if (characterImageName == null)
        {
            // 캐릭터 이미지가 null인 경우, 아무것도 하지 않음 (상태 유지)
            if (currentCharacterImageName != null)
            {
                // 이미지가 있던 상태에서 null이 되는 경우에도 아무 페이드 없이 그대로 유지
                characterImage.sprite = null;
                currentCharacterImageName = null;
                Debug.Log("캐릭터 이미지가 null로 설정되었지만 상태를 유지합니다.");
            }
        }
        else
        {
            if (currentCharacterImageName == null)
            {
                // 캐릭터가 처음 등장하는 경우, 페이드 인 수행
                ChangeCharacterImage(dialogueIndex);
                yield return fadeController.FadeIn(characterGroup, fadeDuration);
            }
            else if (currentCharacterImageName != characterImageName)
            {
                // 캐릭터 이미지가 변경되는 경우, 페이드 아웃 -> 이미지 변경 -> 페이드 인
                yield return fadeController.FadeOut(characterGroup, fadeDuration);
                ChangeCharacterImage(dialogueIndex);
                yield return fadeController.FadeIn(characterGroup, fadeDuration);
            }
        }
    }

    private void ChangeBackGroundImage(int dialogueIndex)
    {
        // 추가: 배경 이미지 전환 코드
        if (ChapterManager.Instance.dialogues[dialogueIndex].BGImage != null && ChapterManager.Instance.dialogues[dialogueIndex].BGImage.Length > 0)
        {
            string backgroundImageName = ChapterManager.Instance.dialogues[dialogueIndex].BGImage[0];
            Sprite newBackground = Resources.Load<Sprite>($"Images/Background/{backgroundImageName}");
            if (newBackground != null)
            {
                backgroundImage.sprite = newBackground;
                Debug.Log($"배경 이미지가 {backgroundImageName}으로 전환되었습니다.");
            }
            else
            {
                Debug.LogWarning($"배경 이미지 {backgroundImageName}을(를) 찾을 수 없습니다.");
            }
        }
    }

    private void ChangeCharacterImage(int dialogueIndex)
    {
        // 추가: 캐릭터 이미지 전환 코드
        if (ChapterManager.Instance.dialogues[dialogueIndex].CharImage != null && ChapterManager.Instance.dialogues[dialogueIndex].CharImage.Length > 0)
        {
            string characterImageName = ChapterManager.Instance.dialogues[dialogueIndex].CharImage[0];
            if (characterImageName == "player")
            {
                // 캐릭터 이미지가 "player"인 경우, 아무것도 하지 않음
                characterImage.gameObject.SetActive (false);
                currentCharacterImageName = "player";
                Debug.Log("캐릭터 이미지가 'player'이므로 이미지가 비워집니다.");
            }
            else
            {
                Sprite newCharacter = Resources.Load<Sprite>($"Images/Character/{characterImageName}");
                if (newCharacter != null)
                {
                    characterImage.gameObject.SetActive(true);
                    characterImage.sprite = newCharacter;
                    currentCharacterImageName = characterImageName;
                    Debug.Log($"캐릭터 이미지가 {characterImageName}으로 전환되었습니다.");
                }
                else
                {
                    Debug.LogWarning($"캐릭터 이미지 {characterImageName}을(를) 찾을 수 없습니다.");
                }
            }
        }
    }

    private void HandleStateChange(UIState newState)
    {
        // 여기 말고 다이얼로그 쪽에서 페이드 주기
        Debug.Log($"상태가 변경되었습니다: {newState}");
    }

    private IEnumerator ChangeStateToDefaultAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        MainScene.Instance.SetState(UIState.Default); // 상태 변경
    }

    public void ChangeArt(int idx)
    {
        Debug.Log($"changeart 발사!: {idx}");
        ChangeBackGroundImage(idx);
        ChangeCharacterImage(idx);
        //StartCoroutine(FadeBackGround(idx)); // 추가: 다이얼로그 인덱스에 따라 배경 이미지 페이드 아웃/인
        //StartCoroutine(FadeCharacter(idx)); // 추가: 다이얼로그 인덱스에 따라 캐릭터 이미지 페이드 아웃/인
    }

  
}
