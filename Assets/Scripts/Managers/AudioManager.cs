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
        // μ΄κΈ° λ³Όλ₯¨ ?€μ 
        _bgmSource.volume = 0.3f;
        _sfxSource.volume = 0.3f;
        
        // ?μ??BGM λ°λ‘ ?μ?κ² ?λ TestCode
        // TODO: ?μ ?λ©΄?μ ?λ ?΄ν?λ‘ ?μ 
        PlayBGM(bgmClip);
    }
    
    private void SetAudioSource()
    {
        // AudioManager???μ?Όλ‘ AudioSource μ»΄ν¬?νΈ κ°μ§?@BGM ?μ±
        _bgmObj = new GameObject("@BGM");
        _bgmObj.transform.parent = transform;
        _bgmSource = _bgmObj.AddComponent<AudioSource>();
        
        // AudioManager???μ?Όλ‘ AudioSource μ»΄ν¬?νΈ κ°μ§?@SFX ?μ±
        _sfxObj = new GameObject("@SFX");
        _sfxObj.transform.parent = transform;
        _sfxSource = _sfxObj.AddComponent<AudioSource>();
    }

    private void SetAudioClip()
    {
        // Resource ?΄λ?μ κ°?AudioClip??λ§λ ?μΌ λ‘λ
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
    
    // λ¦¬μ???΄λ?μ ?μ???€λΈ?νΈ κ°?Έμ€??λ©μ??
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
    
    // λ³Όλ₯¨ μ‘°μ  ?€μ μ°??λ κ²½μ°???¬μ©
    public float GetBGMVolume() => _bgmSource.volume;
    public void SetBGMVolume(float volume) => _bgmSource.volume = volume;
    public float GetSFXVolume() => _sfxSource.volume;
    public void SetSFXVolume(float volume) => _sfxSource.volume = volume;
}