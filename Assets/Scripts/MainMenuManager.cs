using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Button btnContinue;
    [SerializeField] private Button btnRestart;
    [SerializeField] private Button btnExit;


    #region Singleton
    static protected MainMenuManager s_Instance;
    static public MainMenuManager instance { get { return s_Instance; } }
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
        btnContinue.onClick.AddListener(OnContinue);
        btnExit.onClick.AddListener(OnExitGame);

    }

    private void OnContinue()
    {

        SceneManager.LoadScene("Level1");
    }


    public void HideMainMenu()
    {
        menuPanel.gameObject.SetActive(false);
    }

    public void OnExitGame()
    {
        Application.Quit();
    }

}
