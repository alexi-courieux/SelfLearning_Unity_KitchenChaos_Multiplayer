using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using Random = UnityEngine.Random;

public class KitchenGameLobby : MonoBehaviour
{
    public static KitchenGameLobby Instance { get; private set; }

    private Lobby joinedLobby;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        InitializeUnityAuthentication();
    }

    private async void InitializeUnityAuthentication()
    {
        if (UnityServices.State != ServicesInitializationState.Uninitialized) return;

        InitializationOptions initializationOptions = new InitializationOptions();
        initializationOptions.SetProfile(Random.Range(0, int.MaxValue).ToString());
        
        await UnityServices.InitializeAsync();
        
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    public async void CreateLobby(String lobbyName, bool isPrivate)
    {
        CreateLobbyOptions createLobbyOptions = new CreateLobbyOptions
        {
            IsPrivate = isPrivate
        };
        try
        {
            joinedLobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, KitchenGameMultiplayer.MaxPlayersCount, createLobbyOptions);
            KitchenGameMultiplayer.Instance.StartHost();
            Loader.LoadNetwork(Loader.Scene.CharacterSelectScene);
        }
        catch (LobbyServiceException lse)
        {
            Debug.LogError($"Failed to create lobby: {lse.Message}");
        }
    }

    public async void QuickJoin()
    {
        try
        {
           await LobbyService.Instance.QuickJoinLobbyAsync();
           KitchenGameMultiplayer.Instance.StartClient();
        }
        catch (LobbyServiceException lse)
        {
            Debug.LogError($"Failed to join lobby: {lse.Message}");
        }
    }
}
