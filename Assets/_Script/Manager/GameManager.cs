using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    public enum GamePhase
    {
        InGame,
        Endgame
    }

    public GamePhase CurrentPhase { get; private set; }
    public UnityEvent<GamePhase> onPhaseChanged;
    
    public EnemyManager enemyManager;
    public GameObject mainUI;
    public GameObject winUI;
    
    public void ChangePhase(GamePhase phase)
    {
        CurrentPhase = phase;
        onPhaseChanged.Invoke(phase);
    }

    private void Start()
    {
        ChangePhase(GamePhase.InGame);
    }

    private void Update()
    {
        if (CurrentPhase == GamePhase.InGame)
        {
            if (enemyManager.HasTargetGoalReached())
            {
                mainUI.SetActive(false);
                winUI.SetActive(true);
                ChangePhase(GamePhase.Endgame);
            }
        }
    }
}
