using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameState gameState;
    public int numPlayers;
       
    [HideInInspector]
    public Frustration frustration = Frustration.Normal;
    [HideInInspector]
    public Fatigue fatigue = Fatigue.Normal;
    [HideInInspector]
    public Focus focus = Focus.Low;

    private List<IFrustrationListener> frustrationListeners = new List<IFrustrationListener>();
    private List<IFatigueListener> fatigueListeners = new List<IFatigueListener>();
    private List<IFocusListener> focusListeners = new List<IFocusListener>();


    private void Start()
    {
        gameState = GameState.Paused;

        SetFrustration(frustration);
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

    // ============ Frustration ============
    public void RegisterListener(IFrustrationListener listener)
    {
        if (!frustrationListeners.Contains(listener))
        {
            frustrationListeners.Add(listener);
            listener.OnFrustrationChanged(frustration);
        }
    }

    public void UnregisterListener(IFrustrationListener listener)
    {
        frustrationListeners.Remove(listener);
    }

    public void SetFrustration(Frustration newFrustration)
    {
        if (frustration == newFrustration) return;

        frustration = newFrustration;

        foreach (var listener in frustrationListeners)
        {
            listener.OnFrustrationChanged(newFrustration);
        }
    }

    // ============ End - Frustration ============


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




