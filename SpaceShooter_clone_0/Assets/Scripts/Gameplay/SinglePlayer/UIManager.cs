using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header ("Text Components")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _highscoreText;
    [SerializeField] private TextMeshProUGUI _gameOverTxt;
    [SerializeField] private TextMeshProUGUI _restartTxt;

    [Header("Other Componemts")]
    [SerializeField] private Image _livesImg;
    [SerializeField] private Sprite[] _livesSprites;
    [SerializeField] private GameObject _restartButton;

    public int _score;
    public int _highScore;
    // Start is called before the first frame update
    void Start()
    {
        _gameOverTxt.gameObject.SetActive(false);
        _restartTxt.gameObject.SetActive(false);
        _restartButton.gameObject.SetActive(false);
        _scoreText.text = "Score : " + 0;
        _highScore = PlayerPrefs.GetInt("HighScore", 0);
        _highscoreText.text = "High Score: " + _highScore;
    }

    // Update is called once per frame
    public void UpdateScore()
    {
        _score += 10;
        _scoreText.text = "Score : " + _score;
    }
    public void HighScore()
    {
        if( _score>_highScore )
        {
            _highScore = _score;
            PlayerPrefs.SetInt("HighScore", _highScore);
            _highscoreText.text = "High Score :" + _highScore;
        }
       
    }
    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _livesSprites[currentLives];
        if(currentLives == 0)
        {
            _restartTxt.gameObject.SetActive(true);
            _restartButton.gameObject.SetActive(true);
            StartCoroutine(Gameovertext());
           
        }
    }
    IEnumerator Gameovertext()
    {
         
           while(true)
        {
            yield return new WaitForSeconds(0.5f);
            _gameOverTxt.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOverTxt.gameObject.SetActive(false);
        }

    }
}
