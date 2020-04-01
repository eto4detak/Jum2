using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{

    #region Singleton
    static protected DeathScreen s_Instance;
    static public DeathScreen instance { get { return s_Instance; } }
    #endregion
    public Button btnMainMenu;
    public GameObject roundItem;
    public Text round;
    public Text distance;
    public Text time;


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
        btnMainMenu.onClick.AddListener(OnMainMenu);
    }

    public void Show(List<RoundStatistic> statistic)
    {
        btnMainMenu.gameObject.SetActive(true);
        gameObject.SetActive(true);
        for (int i = 0; i < statistic.Count; i++)
        {
            round.text = statistic[i].roundNumber.ToString();
            distance.text = statistic[i].distance.ToString();
            time.text = statistic[i].time.ToString();
            Instantiate(roundItem, transform).SetActive(true);
        }
    }


    public void OnMainMenu()
    {
        
        SceneManager.LoadScene("MainMenu");
    }

}

