using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private Image _livesImg;
    [SerializeField] private Sprite[] _livesSprites;
    [SerializeField] private TextMeshProUGUI _gameOverTxt;
    [SerializeField] private TextMeshProUGUI _restartTxt;
    [SerializeField] private GameObject _restartButton;
    // Start is called before the first frame update
    void Start()
    {
        _score.text = "Score : " + 0;
        _gameOverTxt.gameObject.SetActive(false);
        _restartTxt.gameObject.SetActive(false);
        _restartButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void UpdateScore(int points)
    {
     _score.text = "Score : " + points.ToString();
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
    public void HostButton()
    {
        NetworkManager.Singleton.StartHost();
    }
    public void joinButton()
    {
        NetworkManager.Singleton.StartClient();
    }
}
