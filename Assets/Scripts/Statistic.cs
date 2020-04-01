using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistic : MonoBehaviour
{

    #region Singleton
    static protected Statistic s_Instance;
    static public Statistic instance { get { return s_Instance; } }
    #endregion
    public CharacterMovement observation;
    public ObstacleSpawn obsSpawn;
    public Material[] roundColors;

    private Vector3 oldObservationPosition;
    private float roundDistance = 628f;
    private float traveledDistance;
    private float travaledPercent;
    private float roundTime;
    private int round;
    private int obstacleCount;
    private List<RoundStatistic> roundsStatistic = new List<RoundStatistic>();
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
        obsSpawn.EventSpawn.AddListener(AddSpawnStatistic);
    }
    private void Start()
    {
        oldObservationPosition = observation.transform.position;
    }

    void Update()
    {
        if (observation.gameObject.activeSelf)
        {
            UpObservationStatistic();
            oldObservationPosition = observation.transform.position;
        }
        else
        {
            roundTime = 0;
            traveledDistance = 0;
            travaledPercent = 0;
            round = 0;
            obstacleCount = 0;
        }

    }

    public int GetRound()
    {
        return round;
    }

    public float GetTravaledPercent()
    {
        return travaledPercent;
    }
    public float GetRoundTime()
    {
        return roundTime;
    }
    public int GetObstacleCount()
    {
        return obstacleCount;
    }
    public List<RoundStatistic> GetRoundsStatistics()
    {
        return new List<RoundStatistic>(roundsStatistic);
    }
    private void AddSpawnStatistic()
    {
        obstacleCount++;
    }

    

    private void UpObservationStatistic()
    {
        traveledDistance += (observation.transform.position - oldObservationPosition).magnitude;
        roundTime += Time.deltaTime;
        if (traveledDistance >= roundDistance)
        {
            round++;
            ChangeRound();
            roundTime = 0;
            obstacleCount = 0;
            traveledDistance = 0;
        }
        travaledPercent = traveledDistance / roundDistance * 100;
    }

    private void ChangeRound()
    {
        roundsStatistic.Add(new RoundStatistic() { roundNumber = round, distance = roundDistance, time = roundTime });
        Material tempMat = GetRoundColor(round);
        observation.UpLevel(tempMat);
    }


    private Material GetRoundColor(int roundNumber)
    {
        if (roundNumber >= roundColors.Length) roundNumber = 0;
        return roundColors[roundNumber];
    }

}

public struct RoundStatistic
{
    public int roundNumber;
    public float time;
    public float distance;
}