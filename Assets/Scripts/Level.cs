using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public Health hero;

    public bool startGame;
    public bool continueGame;

    private GameObject roundWinner;
    private GameObject gameWinner;
    private GameObject levelResultat;
    private StatusLevel status;
    #region Singleton
    static protected Level s_Instance;
    static public Level instance { get { return s_Instance; } }
    #endregion

    private void Awake()
    {
        #region Singleton
        if (s_Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        s_Instance = this;
        #endregion
    }
    
    public void StartLevel()
    {
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(LevelPausing());
        yield return StartCoroutine(LevelPlaying());
        yield return StartCoroutine(LevelEnding());
    }
    private IEnumerator LevelPausing()
    {
        status = StatusLevel.start;
        MainMenuManager.instance.HideMainMenu();
        Time.timeScale = 0;
        startGame = false;
        while (!startGame)
        {
            yield return null;
        }
    }

    private IEnumerator LevelPlaying()
    {
        status = StatusLevel.playing;
        Time.timeScale = 1;
        while (CheckHero())
        {
            yield return null;
        }
    }

    private IEnumerator LevelEnding()
    {
        status = StatusLevel.end;
        Time.timeScale = 0;
        DeathScreen.instance.Show(Statistic.instance.GetRoundsStatistics());
        yield return null;
    }


    private bool CheckHero()
    {
        return hero.gameObject.activeSelf;
    }

    public void PauseGame()
    {
        if(status == StatusLevel.playing) Time.timeScale = 0;
    }
    public void ContinueGame()
    {
        if (status == StatusLevel.playing) Time.timeScale = 1;
    }
}
public enum StatusLevel
{
    start = 1,
    playing = 2,
    end = 3
}