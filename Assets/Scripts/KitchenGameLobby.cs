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

    private const float HeartbeatInterval = 10f;
    
    private Lobby joinedLobby;
    private float heartbeatTimer = HeartbeatInterval;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        InitializeUnityAuthentication();
    }

    private void Update()
    {
        HandleHeartbeat();
    }

    private void HandleHeartbeat()
    {
        if (IsLobbyHost())
        {
            heartbeatTimer -= Time.deltaTime;
            if (heartbeatTimer <= 0)
            {
                Debug.Log("Sending heartbeat ping");
                heartbeatTimer = HeartbeatInterval;
                try
                {
                    LobbyService.Instance.SendHeartbeatPingAsync(joinedLobby.Id);
                } catch (LobbyServiceException lse)
                {
                    Debug.LogError($"Failed to send heartbeat ping: {lse.Message}");
                }
            }
        }
    }

    private bool IsLobbyHost()
    {
        return joinedLobby != null && joinedLobby.HostId == AuthenticationService.Instance.PlayerId;
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
           joinedLobby = await LobbyService.Instance.QuickJoinLobbyAsync();
           KitchenGameMultiplayer.Instance.StartClient();
        }
        catch (LobbyServiceException lse)
        {
            Debug.LogError($"Failed to join lobby: {lse.Message}");
        }
    }

    public Lobby GetLobby()
    {
        return joinedLobby;
    }
    
    public async void JoinByCode(string code)
    {
        try
        {
            joinedLobby = await LobbyService.Instance.JoinLobbyByCodeAsync(code);
            KitchenGameMultiplayer.Instance.StartClient();
        }
        catch (LobbyServiceException lse)
        {
            Debug.LogError($"Failed to join lobby: {lse.Message}");
        }
    }
}
