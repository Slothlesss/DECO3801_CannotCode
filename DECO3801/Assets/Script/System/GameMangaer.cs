using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public Difficulty difficulty;

    private List<IDifficultyListener> listeners = new List<IDifficultyListener>();
    private void Start()
    {
        SetDifficulty(Difficulty.Hard);
    }

    public void RegisterListener(IDifficultyListener listener)
    {
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
            listener.OnDifficultyChanged(difficulty);
        }
    }

    public void UnregisterListener(IDifficultyListener listener)
    {
        listeners.Remove(listener);
    }

    public void SetDifficulty(Difficulty newDifficulty)
    {
        if (difficulty == newDifficulty) return;

        difficulty = newDifficulty;

        foreach (var listener in listeners)
        {
            listener.OnDifficultyChanged(newDifficulty);
        }
    }
}

