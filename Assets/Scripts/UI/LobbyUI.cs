using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private Button MainMenuButton;
    [SerializeField] private Button CreateLobbyButton;
    [SerializeField] private Button QuickJoinButton;

    private void Awake()
    {
        MainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        
        CreateLobbyButton.onClick.AddListener(() =>
        {
            KitchenGameLobby.Instance.CreateLobby("Test Lobby", false);
        });
        
        QuickJoinButton.onClick.AddListener(() =>
        {
            KitchenGameLobby.Instance.QuickJoin();
        });
    }
}
