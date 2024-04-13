using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Player _player;
    private bool _onConsole;
    public bool IsCoOpMode;
    private Animator _anim;
    public static GameManager Singleton { get; private set; }
    [HideInInspector] public bool _IsGameover;
    [SerializeField] private GameObject _firebutton, _joystick;
    [SerializeField] private GameObject _pauseAnimobj;



    // Start is called before the first frame update
    void Start()
    {
        _anim=_pauseAnimobj.GetComponent<Animator>();
      _anim.updateMode=AnimatorUpdateMode.UnscaledTime;
        try
        {
            _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        }
        catch
        {
            if (!_IsGameover)
            {
                Debug.LogError("Player Not found");
            }
        }
       
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            _onConsole = true;
            _firebutton.gameObject.SetActive(false);
            _joystick.gameObject.SetActive(false);
        }
        else if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            _onConsole = false;
            _firebutton.gameObject.SetActive(true);
            _joystick.gameObject.SetActive(true);

        }
        Singleton = this;
        Scene scene = SceneManager.GetActiveScene();
        if(scene.buildIndex==2)
        {
            IsCoOpMode = true;
        }
        else if(scene.buildIndex==1)
        {
            IsCoOpMode = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
         
        
        try
        {
            if (_onConsole)
            {
                if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
                {
                    _player.laser();
                }
            }
        }
        catch
        {
            if(!_IsGameover)
            {
                Debug.LogError("Player not found.");
            }
           
        }
        
    }
    public void restartButton()
    {
        if(_IsGameover&& IsCoOpMode)
        {
            SceneManager.LoadScene(2);
        }
        else if (_IsGameover && !IsCoOpMode)
        {
            SceneManager.LoadScene(1);
        }
    }
    public void PauseButton()
    {
        _pauseAnimobj.SetActive(true);
        Time.timeScale = 0;
        _anim.SetBool("IsPause", true);
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            _firebutton.gameObject.SetActive(false);
            _joystick.gameObject.SetActive(false);

        }
    }
    public void ResumeButton()
    {
         Time.timeScale = 1;
        _pauseAnimobj.SetActive(false);
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            _firebutton.gameObject.SetActive(true);
            _joystick.gameObject.SetActive(true);

        }
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
