using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager GameMangerInstance;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI LivesText;
    public GameObject MenuUI;
    public GameObject GamePLayUI;
    public GameObject Spawner;
    public GameObject BackGroundParticle;
    public long NumberOfCameraShakes;
    public bool isGameStarted = false;
    public GameObject Player;
    private int _Lives = 3;
    private long _Score = 0;
    private Vector3 _OriginalCameraPosition;

    public void Awake()
    {
        GameMangerInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _OriginalCameraPosition = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        isGameStarted = true;

        MenuUI.SetActive(false);
        GamePLayUI.SetActive(true);
        Spawner.SetActive(true);
        BackGroundParticle.SetActive(true);
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void EndGame()
    {
        Player.SetActive(false);
        Invoke("ReloadLevel", 1.5f);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene("Game");
    }

    public void UpdateLives()
    {
        if (_Lives <= 0)
        {
            EndGame();
        }
        else
        {
            _Lives--;
            LivesText.text = $"Lives: {_Lives}";
        }
    }

    public void UpdateScore()
    {
        _Score++;
        ScoreText.text = $"Score: {_Score}";
    }

    public void ShakeCamera()
    {
        StartCoroutine("CameraShake");
    }

    IEnumerator CameraShake()
    {
        for (int i = 0; i < NumberOfCameraShakes; i++)
        {
            var randomPosition = Random.insideUnitCircle * 0.5f;

            Camera.main.transform.position = new Vector3(randomPosition.x, randomPosition.y, _OriginalCameraPosition.z);

            yield return null;
        }

        Camera.main.transform.position = _OriginalCameraPosition;
    }
}
