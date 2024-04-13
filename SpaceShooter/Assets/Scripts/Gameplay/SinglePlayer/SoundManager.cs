using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Slider _soundvol;
    [SerializeField] private string _volumeName;
    public static SoundManager instance { get; private set; }
    private AudioSource _soundSource;
    // Start is called before the first frame update
    void Start()
    {
       _soundSource= GetComponent<AudioSource>();
      
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }

        if (!PlayerPrefs.HasKey(_volumeName))
        {
            PlayerPrefs.GetFloat(_volumeName, 1);
            LoadVol();
        }
        else
        {
            LoadVol();
        }

    }

    // Update is called once per frame
    void Update()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void PlayAudio(AudioClip clip)
    {
        _soundSource.PlayOneShot(clip);
    }
    public void SoundChange()
    {
        _soundSource.volume = _soundvol.value;
        Savevol();
    }
    private void Savevol()
    {
        PlayerPrefs.SetFloat(_volumeName, _soundvol.value);
    }
    private void LoadVol()
    {
        _soundvol.value = PlayerPrefs.GetFloat(_volumeName,1);
    }
}
