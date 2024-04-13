using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
   
    public static SoundManager instance { get; private set; }
   [HideInInspector] public AudioSource _soundSource;
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
    
}
