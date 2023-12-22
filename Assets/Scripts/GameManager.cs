using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{ 
    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        Playing,
        GameOver
    }

    private State _state;
    private float _countdownToStartTimer = 3f;
    private float _gamePlayingTimerMax = 30f;
    private float _gamePlayingTimer;
    private bool _isGamePaused;

    private void Awake()
    {
        Instance = this;
        _state = State.WaitingToStart;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteract += GameInput_OnInteract;
    }

    private void GameInput_OnInteract(object sender, EventArgs e)
    {
        if (State.WaitingToStart == _state)
        {
            _state = State.CountdownToStart;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        ToggleGamePaused();
    }

    private void Update()
    {
        switch (_state)
        {
            case State.WaitingToStart:
                break;
            case State.CountdownToStart:
                _countdownToStartTimer -= Time.deltaTime;
                if (_countdownToStartTimer <= 0f)
                {
                    _state = State.Playing;
                    _gamePlayingTimer = _gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.Playing:
                _gamePlayingTimer -= Time.deltaTime;
                if (_gamePlayingTimer <= 0f)
                {
                    _state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
            default:
                throw new ArgumentOutOfRangeException("state", _state, "Unexpected state");
        }
    }
    
    public bool IsGamePlaying()
    {
        return _state == State.Playing;
    }

    public bool IsCountdownToStartActive()
    {
        return _state == State.CountdownToStart;
    }
    
    public float GetCountdownToStartTimer()
    {
        return _countdownToStartTimer;
    }

    public bool IsGameOver()
    {
        return _state == State.GameOver;
    }
    
    public float GetPlayingTimerNormalized()
    {
        return 1 - (_gamePlayingTimer / _gamePlayingTimerMax);
    }

    public void ToggleGamePaused()
    {
        _isGamePaused = !_isGamePaused;
        if (_isGamePaused) 
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else 
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }
}