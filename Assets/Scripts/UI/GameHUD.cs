using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameHUD : MonoBehaviour
{
    [Header("Round Info")]
    public TextMeshProUGUI txtRound;
    public TextMeshProUGUI txtRoundPercent;
    public TextMeshProUGUI txtRoundTime;
    public TextMeshProUGUI txtObstacleCount;

    [Header("Button")]
    public Button btnStartBattle;
    public Button btnRestart;
    public Button btnPause;

    [Header("Dead Screen")]
    public GameObject panelDeathScreen;

    private Statistic statistic;
    private Level level;

    #region Singleton
    static protected GameHUD s_Instance;
    static public GameHUD instance { get { return s_Instance; } }
    #endregion

    void Awake()
    {
        #region Singleton
        if (s_Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        s_Instance = this;
        #endregion
        btnStartBattle.onClick.AddListener(OnStartBattle);
        btnRestart.onClick.AddListener(OnRestartLevel);
        btnPause.onClick.AddListener(OnPauseGame);
    }

    private void Start()
    {
        level = Level.instance;
        statistic = Statistic.instance;
    }

    private void Update()
    {
        txtRound.text = statistic.GetRound().ToString();
        txtRoundPercent.text = ((int)statistic.GetTravaledPercent()).ToString() + " %";
        txtRoundTime.text = ((int)statistic.GetRoundTime()).ToString() + " s";
        txtObstacleCount.text = statistic.GetObstacleCount().ToString();
    }

    public void ViewBtnStart(bool show)
    {
        btnStartBattle.gameObject.SetActive(show);
    }

    private void OnStartBattle()
    {
        level.startGame = true; 
        ViewBtnStart(false);
    }
    private void OnRestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnPauseGame()
    {
        if(Time.timeScale == 0) level.ContinueGame();
        else level.PauseGame();

    }

    public void ShowDeathScreen()
    {
        panelDeathScreen.gameObject.SetActive(true);

    }

}
