using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyCreateUI : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Button createPublicLobbyButton;
    [SerializeField] private Button createPrivateLobbyButton;
    [SerializeField] private TMP_InputField lobbyNameInputField;
    [SerializeField] private LobbyUI lobbyUI;
    
    private void Awake()
    {
        closeButton.onClick.AddListener(() =>
        {
            Hide();
        });
        
        createPublicLobbyButton.onClick.AddListener(() =>
        {
            KitchenGameLobby.Instance.CreateLobby(lobbyNameInputField.text, false);
        });
        
        createPrivateLobbyButton.onClick.AddListener(() =>
        {
            KitchenGameLobby.Instance.CreateLobby(lobbyNameInputField.text, true);
        });
    }

    private void Start()
    {
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
