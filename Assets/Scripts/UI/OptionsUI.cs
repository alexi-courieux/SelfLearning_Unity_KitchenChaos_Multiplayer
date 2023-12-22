using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }
    
    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;

    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAltButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAltText;
    [SerializeField] private TextMeshProUGUI pauseText;

    [SerializeField] private Transform pressToRebindKeyTransform;
    
    private void Awake()
    {
        Instance = this;
        soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisuals();
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisuals();
        });
        closeButton.onClick.AddListener(Hide);
        
        moveUpButton.onClick.AddListener(() => { RebindKey(GameInput.Binding.MoveUp); });
        moveDownButton.onClick.AddListener(() => { RebindKey(GameInput.Binding.MoveDown); });
        moveLeftButton.onClick.AddListener(() => { RebindKey(GameInput.Binding.MoveLeft); });
        moveRightButton.onClick.AddListener(() => { RebindKey(GameInput.Binding.MoveRight); });
        interactButton.onClick.AddListener(() => { RebindKey(GameInput.Binding.Interact); });
        interactAltButton.onClick.AddListener(() => { RebindKey(GameInput.Binding.InteractAlternate); });
        pauseButton.onClick.AddListener(() => { RebindKey(GameInput.Binding.Pause); });
    }

    private void Start()
    {
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        UpdateVisuals();
        Hide();
        HidePressToRebindKey();
    }

    private void GameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void UpdateVisuals()
    {
        float soundEffectsVolume = Mathf.Round(SoundManager.Instance.Volume * 10f);
        soundEffectsText.text = "Sound Effects: " + soundEffectsVolume + "/10";

        float musicVolume = Mathf.Round(MusicManager.Instance.Volume * 10f);
        musicText.text = "Music: " + musicVolume + "/10";

        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveUp);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveDown);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveLeft);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveRight);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
    }
    
    public void Show()
    {
        gameObject.SetActive(true);
    }
    
    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowPressToRebindKey()
    {
        Debug.Log("ShowPressToRebindKey");
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }
    
    private void HidePressToRebindKey()
    {
        Debug.Log("HidePressToRebindKey");
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindKey(GameInput.Binding binding)
    {
        ShowPressToRebindKey();
        GameInput.Instance.RebindKey(binding, () =>
        {
            HidePressToRebindKey();
            UpdateVisuals();
        });
    }
}
