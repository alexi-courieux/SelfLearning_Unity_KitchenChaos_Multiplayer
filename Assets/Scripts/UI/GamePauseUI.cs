using System;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button mainMenuButton;
    
    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.ToggleGamePaused();
        });
        
        optionsButton.onClick.AddListener(() =>
        {
            OptionsUI.Instance.Show(Show);
            Hide();
        });
        
        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        
        Hide();
    }

    private void GameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnGamePaused(object sender, EventArgs e)
    {
        Show();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        resumeButton.Select();
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
