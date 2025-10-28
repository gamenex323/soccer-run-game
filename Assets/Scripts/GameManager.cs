using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Mode[] modes;
    public ModeType modeType;
    [HideInInspector] public Mode currentMode;
    public int lifeAtStart;
    private int _lifeCounter;
    public TextMeshProUGUI lifeText;
    public BallController ball;
    public GameObject currentLevel;
    private GameObject _levelToSpawn;
    [HideInInspector] public bool stopGame;
    public GameObject gameplay;
    public static GameManager Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        SetMode(0);
        _lifeCounter = lifeAtStart;
        SetLifeText();
    }

    private void SetLifeText()
    {
        lifeText.text = "Life " + _lifeCounter;
    }

    public void SetMode(int modeIndex)
    {
        switch (modeIndex)
        {
            case 0:
                currentMode = modes[0];
                _levelToSpawn = modes[0].level;
                break;
            case 1:
                currentMode = modes[1];
                break;
            case 2:
                currentMode = modes[2];
                break;
        }
    }

    void Update()
    {
        // Example: check difficulty
        if (currentMode.modeType == ModeType.Hard)
        {
            // Add behavior for hard mode
        }
    }

    public void HitWithHurdle()
    {
        _lifeCounter--;
        SetLifeText();
        if (_lifeCounter == 0)
        {
            GameFail();
        }
        else
        {
            ball.CoolTheBall();
        }
    }

    public void SpawnLevel()
    {
        gameplay.SetActive(true);
        if (currentLevel)
        {
            Destroy(currentLevel);
        }
        currentLevel = Instantiate(_levelToSpawn);
    }

    private void GameFail()
    {
        print("Game Fail");
        stopGame = true;
        UIManager.Instance.levelFailPanel.SetActive(true);
    }

    public void RestartGame()
    {
        ball.BallCollider(true);
        stopGame = false;
        _lifeCounter = lifeAtStart;
        SetLifeText();
        SpawnLevel();
    }
}

public enum ModeType
{
    Easy,
    Medium,
    Hard
}

[Serializable]
public class Mode
{
    public ModeType modeType;
    public float speedOfLevel;
    public GameObject level;

}