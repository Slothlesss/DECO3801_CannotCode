using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : Singleton<MainMenu>
{

    // [SerializeField] private GameObject startPanel;

    // [SerializeField] private GameObject gameOverPanel;

    public int players;

    // public void Start() {

    // }

    /// <summary>
    /// Starts game when button is pressed.
    /// </summary>
    public void Play() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }

    public void OnePlayer() {
        players = 1;
        Play();
    }

    public void TwoPlayers() {
        players = 2;
        Play();
    }
}
