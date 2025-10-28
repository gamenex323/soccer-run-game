using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject mainMenuPanel;
    public GameObject gameplayPanel;
    public GameObject loadingPanel;
    public GameObject levelFailPanel;
    public Image loadingFiller;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    public void OnClickPlay()
    {
        mainMenuPanel.SetActive(false);
        gameplayPanel.SetActive(true);
        loadingPanel.SetActive(true);

        EnableLoading(() =>
        {
            GameManager.Instance.SpawnLevel();
        });
    }

    private void EnableLoading(Action onComplete = null)
    {
        loadingFiller.fillAmount = 0f; // reset first
        loadingFiller.DOFillAmount(1f, 1f).OnComplete(() =>
        {
            loadingPanel.SetActive(false);
            onComplete?.Invoke(); // call callback if provided
        });
    }
}