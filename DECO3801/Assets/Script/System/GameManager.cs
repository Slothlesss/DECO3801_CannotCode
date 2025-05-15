using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameState gameState;
    public int numPlayers;
       
    public Difficulty difficulty;
    public Fatigue fatigue;
    public Focus focus;

    private List<IDifficultyListener> difficultyListeners = new List<IDifficultyListener>();
    private List<IFatigueListener> fatigueListeners = new List<IFatigueListener>();
    private List<IFocusListener> focusListeners = new List<IFocusListener>();


    private void Start()
    {
        gameState = GameState.Paused;

        SetDifficulty(difficulty);
        SetFatigue(fatigue);
        SetFocus(focus);
    }

    /// <summary>
    /// Replays game when button is pressed.
    /// </summary>
    public void Replay()
    {
        Time.timeScale = 1f;
        gameState = GameState.Running;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // ============ Difficulty ============
    public void RegisterListener(IDifficultyListener listener)
    {
        if (!difficultyListeners.Contains(listener))
        {
            difficultyListeners.Add(listener);
            listener.OnDifficultyChanged(difficulty);
        }
    }

    public void UnregisterListener(IDifficultyListener listener)
    {
        difficultyListeners.Remove(listener);
    }

    public void SetDifficulty(Difficulty newDifficulty)
    {
        if (difficulty == newDifficulty) return;

        difficulty = newDifficulty;

        foreach (var listener in difficultyListeners)
        {
            listener.OnDifficultyChanged(newDifficulty);
        }
    }

    // ============ End - Difficulty ============


    // ============ Fatigue ============
    public void RegisterListener(IFatigueListener listener)
    {
        if (!fatigueListeners.Contains(listener))
        {
            fatigueListeners.Add(listener);
            listener.OnFatigiueChanged(fatigue);
        }
    }

    public void UnregisterListener(IFatigueListener listener)
    {
        fatigueListeners.Remove(listener);
    }

    public void SetFatigue(Fatigue newFatigue)
    {
        if (fatigue == newFatigue) return;

        fatigue = newFatigue;

        foreach (var listener in fatigueListeners)
        {
            listener.OnFatigiueChanged(newFatigue);
        }
    }

    // ============ End - Fatigue ============


    // ============ Focus ============
    public void RegisterListener(IFocusListener listener)
    {
        if (!focusListeners.Contains(listener))
        {
            focusListeners.Add(listener);
            listener.OnFocusChanged(focus);
        }
    }

    public void UnregisterListener(IFocusListener listener)
    {
        focusListeners.Remove(listener);
    }

    public void SetFocus(Focus newFocus)
    {
        if (focus == newFocus) return;

        focus = newFocus;

        foreach (var listener in focusListeners)
        {
            listener.OnFocusChanged(newFocus);
        }
    }

    // ============ End - Difficulty ============


    /// <summary>
    /// Exits game when button is pressed.
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

}


