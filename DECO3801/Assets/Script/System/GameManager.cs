using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameState gameState;
    public int numPlayers;
       
    [HideInInspector]
    public Frustration frustration;
    [HideInInspector]
    public Fatigue fatigue;
    [HideInInspector]
    public Focus focus;

    private List<IFrustrationListener> frustrationListeners = new List<IFrustrationListener>();
    private List<IFatigueListener> fatigueListeners = new List<IFatigueListener>();
    private List<IFocusListener> focusListeners = new List<IFocusListener>();

    [Header("Random Mode")]
    [SerializeField] private bool randomMode;
    [SerializeField] private float interval;
    private float timer;

    private void Awake()
    {
        gameState = GameState.Paused;

        frustration = Frustration.Normal;
        fatigue = Fatigue.Normal;
        focus = Focus.Low;

        SetFrustration(frustration);
        SetFatigue(fatigue);
        SetFocus(focus);
    }

    private void Update()
    {
        if (!randomMode) return;
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer = 0f;
            AssignRandomLevels();
        }
    }
    private void AssignRandomLevels()
    {
        Frustration newFrustration = (Frustration)Random.Range(0, 4);
        Fatigue newFatigue = (Fatigue)Random.Range(0, 4);
        Focus newFocus = (Focus)Random.Range(0, 4);

        SetFrustration(newFrustration);
        SetFatigue(newFatigue);
        SetFocus(newFocus);

        Debug.Log($"[Random EEG] Frustration: {(int)newFrustration}, Fatigue: {(int)newFatigue}, Focus: {(int)newFocus}");
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
    public void RegisterFrustrationListener(IFrustrationListener listener)
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
    public void RegisterFatigueListener(IFatigueListener listener)
    {
        if (!fatigueListeners.Contains(listener))
        {
            fatigueListeners.Add(listener);
            listener.OnFatigueChanged(fatigue);
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
            listener.OnFatigueChanged(newFatigue);
        }
    }

    // ============ End - Fatigue ============


    // ============ Focus ============
    public void RegisterFocusListener(IFocusListener listener)
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




