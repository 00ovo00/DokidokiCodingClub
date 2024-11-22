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
        // 초기 볼륨 ?�정
        _bgmSource.volume = 0.3f;
        _sfxSource.volume = 0.3f;
        
        // ?�작??BGM 바로 ?�작?�게 ?�는 TestCode
        // TODO: ?�작 ?�면?�서 ?�레?�하?�록 ?�정
        PlayBGM(bgmClip);
    }
    
    private void SetAudioSource()
    {
        // AudioManager???�식?�로 AudioSource 컴포?�트 가�?@BGM ?�성
        _bgmObj = new GameObject("@BGM");
        _bgmObj.transform.parent = transform;
        _bgmSource = _bgmObj.AddComponent<AudioSource>();
        
        // AudioManager???�식?�로 AudioSource 컴포?�트 가�?@SFX ?�성
        _sfxObj = new GameObject("@SFX");
        _sfxObj.transform.parent = transform;
        _sfxSource = _sfxObj.AddComponent<AudioSource>();
    }

    private void SetAudioClip()
    {
        // Resource ?�더?�서 �?AudioClip??맞는 ?�일 로드
        bgmClip = ResourceLoad<AudioClip>("Audios/BGM");
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
    
    // 리소???�더?�서 ?�요???�브?�트 가?�오??메소??
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
    
    // 볼륨 조절 ?�정�??�는 경우???�용
    public float GetBGMVolume() => _bgmSource.volume;
    public void SetBGMVolume(float volume) => _bgmSource.volume = volume;
    public float GetSFXVolume() => _sfxSource.volume;
    public void SetSFXVolume(float volume) => _sfxSource.volume = volume;
}