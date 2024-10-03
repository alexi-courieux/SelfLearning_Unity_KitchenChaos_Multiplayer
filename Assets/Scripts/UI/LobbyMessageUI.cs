using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class LobbyMessageUI : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI messageText;
    
    private EventHandler createLobbyStartedHandler;
    private EventHandler createLobbyFailedHandler;
    private EventHandler joinStartedHandler;
    private EventHandler joinFailedHandler;
    private EventHandler quickJoinFailedHandler;
    
    private void Awake()
    {
        closeButton.onClick.AddListener(Hide);
    }

    private void Start()
    {
        KitchenGameMultiplayer.Instance.OnFailedToJoinGame += KitchenGameMultiplayer_OnFailedToJoinGame;

        createLobbyStartedHandler = (sender, args) => ShowMessage("Creating lobby...");
        createLobbyFailedHandler = (sender, args) => ShowMessage("Failed to create lobby");
        joinStartedHandler = (sender, args) => ShowMessage("Joining lobby...");
        joinFailedHandler = (sender, args) => ShowMessage("Failed to join lobby");
        quickJoinFailedHandler = (sender, args) => ShowMessage("Could not find a lobby to quick join");
        
        KitchenGameLobby.Instance.OnCreateLobbyStarted += createLobbyStartedHandler;
        KitchenGameLobby.Instance.OnCreateLobbyFailed += createLobbyFailedHandler;
        KitchenGameLobby.Instance.OnJoinStarted += joinStartedHandler;
        KitchenGameLobby.Instance.OnJoinFailed += joinFailedHandler;
        KitchenGameLobby.Instance.OnQuickJoinFailed += quickJoinFailedHandler;
        
        Hide();
    }

    private void OnDestroy()
    {
        KitchenGameMultiplayer.Instance.OnFailedToJoinGame -= KitchenGameMultiplayer_OnFailedToJoinGame;
        KitchenGameLobby.Instance.OnCreateLobbyStarted -= createLobbyStartedHandler;
        KitchenGameLobby.Instance.OnCreateLobbyFailed -= createLobbyFailedHandler;
        KitchenGameLobby.Instance.OnJoinStarted -= joinStartedHandler;
        KitchenGameLobby.Instance.OnJoinFailed -= joinFailedHandler;
        KitchenGameLobby.Instance.OnQuickJoinFailed -= quickJoinFailedHandler;
    }

    private void KitchenGameMultiplayer_OnFailedToJoinGame(object sender, EventArgs e)
    {
        if (NetworkManager.Singleton.DisconnectReason is null or "")
        {
            ShowMessage("Failed to connect");
        }
        ShowMessage(messageText.text = NetworkManager.Singleton.DisconnectReason);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    
    private void ShowMessage(string message)
    {
        Show();
        messageText.text = message;
    }
    
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
