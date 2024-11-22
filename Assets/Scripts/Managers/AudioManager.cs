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
        // ì´ˆê¸° ë³¼ë¥¨ ?¤ì •
        _bgmSource.volume = 0.3f;
        _sfxSource.volume = 0.3f;
        
        // ?œì‘??BGM ë°”ë¡œ ?œì‘?˜ê²Œ ?˜ëŠ” TestCode
        // TODO: ?œì‘ ?”ë©´?ì„œ ?Œë ˆ?´í•˜?„ë¡ ?˜ì •
        PlayBGM(bgmClip);
    }
    
    private void SetAudioSource()
    {
        // AudioManager???ì‹?¼ë¡œ AudioSource ì»´í¬?ŒíŠ¸ ê°€ì§?@BGM ?ì„±
        _bgmObj = new GameObject("@BGM");
        _bgmObj.transform.parent = transform;
        _bgmSource = _bgmObj.AddComponent<AudioSource>();
        
        // AudioManager???ì‹?¼ë¡œ AudioSource ì»´í¬?ŒíŠ¸ ê°€ì§?@SFX ?ì„±
        _sfxObj = new GameObject("@SFX");
        _sfxObj.transform.parent = transform;
        _sfxSource = _sfxObj.AddComponent<AudioSource>();
    }

    private void SetAudioClip()
    {
        // Resource ?´ë”?ì„œ ê°?AudioClip??ë§ëŠ” ?Œì¼ ë¡œë“œ
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
    
    // ë¦¬ì†Œ???´ë”?ì„œ ?„ìš”???¤ë¸Œ?íŠ¸ ê°€?¸ì˜¤??ë©”ì†Œ??
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
    
    // ë³¼ë¥¨ ì¡°ì ˆ ?¤ì •ì°??ˆëŠ” ê²½ìš°???¬ìš©
    public float GetBGMVolume() => _bgmSource.volume;
    public void SetBGMVolume(float volume) => _bgmSource.volume = volume;
    public float GetSFXVolume() => _sfxSource.volume;
    public void SetSFXVolume(float volume) => _sfxSource.volume = volume;
}