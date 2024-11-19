using UnityEngine;

public class AudioManager : SingletonBase<AudioManager>
{
    private GameObject _bgmObj;
    private GameObject _sfxObj;

    private AudioSource _bgmSource;
    private AudioSource _sfxSource;
    
    [Header("BGM")]
    [SerializeField] private AudioClip bgmClip;

    [Header("SFX")]
    [SerializeField] private AudioClip clickSfx;
    
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        
        SetAudioSource();
        SetAudioClip();
    }
    
    private void Start()
    {
        // 초기 볼륨 설정
        _bgmSource.volume = 0.5f;
        _sfxSource.volume = 0.5f;
        
        // 시작시 BGM 바로 시작하게 하는 TestCode
        // TODO: 시작 화면에서 플레이하도록 수정
        PlayBGM(bgmClip);
    }
    
    private void SetAudioSource()
    {
        // AudioManager의 자식으로 AudioSource 컴포넌트 가진 @BGM 생성
        _bgmObj = new GameObject("@BGM");
        _bgmObj.transform.parent = transform;
        _bgmSource = _bgmObj.AddComponent<AudioSource>();
        
        // AudioManager의 자식으로 AudioSource 컴포넌트 가진 @SFX 생성
        _sfxObj = new GameObject("@SFX");
        _sfxObj.transform.parent = transform;
        _sfxSource = _sfxObj.AddComponent<AudioSource>();
    }

    private void SetAudioClip()
    {
        // Resource 폴더에서 각 AudioClip에 맞는 파일 로드
        bgmClip = ResourceLoad<AudioClip>("Audios/BGM_Clip");
        clickSfx = ResourceLoad<AudioClip>("Audios/SFX_ButtonClick");
    }

    public void PlayBGM(AudioClip clip)
    {
        if (_bgmSource.clip != clip)
        {
            _bgmSource.clip = clip;
            _bgmSource.loop = true;
            _bgmSource.Play();
        }
    }
    
    public void PlaySFX(AudioClip clip)
    {
        _sfxSource.PlayOneShot(clip);
    }
    
    // 리소스 폴더에서 필요한 오브젝트 가져오는 메소드
    public T ResourceLoad<T>(string path) where T : Object
    {
        T instance = Resources.Load<T>(path);
        if (instance == null)
        {
            Debug.Log($"{typeof(T).Name} not found in Resources folder at {path}.");
        }
        return instance;
    }
    
    public void PlayStartBGM() => PlayBGM(bgmClip);
    public void PlayClickSFX() => PlaySFX(clickSfx);
    
    // 볼륨 조절 설정창 있는 경우에 사용
    public float GetBGMVolume() => _bgmSource.volume;
    public void SetBGMVolume(float volume) => _bgmSource.volume = volume;
    public float GetSFXVolume() => _sfxSource.volume;
    public void SetSFXVolume(float volume) => _sfxSource.volume = volume;
}