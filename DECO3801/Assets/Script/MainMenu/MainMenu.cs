using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : Singleton<MainMenu>
{
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private TextMeshProUGUI playerModeText;


    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject tutorialPanel;

    [SerializeField] private GameObject playerTwoPrefab;

    private void Start()
    {
        UpdateUI();
        leftButton.onClick.AddListener(() =>
        {
            GameManager.Instance.numPlayers = 1;
            UpdateUI();
        });

        rightButton.onClick.AddListener(() =>
        {
            GameManager.Instance.numPlayers = 2;
            UpdateUI();
        });
    }
    /// <summary>
    /// Starts game when button is pressed.
    /// </summary>
    public void Play() {
        Time.timeScale = 1f;
        GameManager.Instance.gameState = GameState.Running;
        startPanel.SetActive(false);
    }

    private void UpdateUI()
    {
        if (GameManager.Instance.numPlayers == 1)
        {
            playerModeText.text = "<color=#00A1FF>1 Player</color>";
            leftButton.interactable = false;
            leftButton.image.color = Color.grey;

            rightButton.interactable = true;
            rightButton.image.color = Color.white;

            playerTwoPrefab.SetActive(false);
        }
        else if (GameManager.Instance.numPlayers == 2)
        {
            playerModeText.text = "<color=#FF6400>2 Players</color>";
            leftButton.interactable = true;
            leftButton.image.color = Color.white;

            rightButton.interactable = false;
            rightButton.image.color = Color.grey;

            playerTwoPrefab.SetActive(true);
        }
    }
}
