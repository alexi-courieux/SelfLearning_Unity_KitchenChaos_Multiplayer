using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    
    [SerializeField] private Button MainMenuButton;
    [SerializeField] private Button CreateLobbyButton;
    [SerializeField] private Button QuickJoinButton;
    [SerializeField] private LobbyCreateUI lobbyCreateUI;
    [SerializeField] private Button JoinCodeButton;
    [SerializeField] private TMP_InputField JoinCodeInputField;

    private void Awake()
    {
        MainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        
        CreateLobbyButton.onClick.AddListener(() =>
        {
            lobbyCreateUI.Show();
        });
        
        QuickJoinButton.onClick.AddListener(() =>
        {
            KitchenGameLobby.Instance.QuickJoin();
        });

        JoinCodeButton.onClick.AddListener(() =>
        {
            KitchenGameLobby.Instance.JoinByCode(JoinCodeInputField.text);
        });
    }
}
