using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{

    public delegate void OnScore(int score);
    public static event OnScore _OnScoreToAdd;
    [SerializeField] private int containerPoints = 2;
    [SerializeField] private int primaryPoints = 10;
    [SerializeField] private int secundaryPoints = 20;

    private int myCoins = 0;
    private int myScore = 0;
    private int totalGain = 0;

    private int tasksCompleted = 0;

    private int tasksFailed = 0;
    private int p_colorFailures = 0;
    private int s_colorFailures = 0;
    private int containersFailures = 0;

    private new void Awake() 
    {
        base.Awake();
        this.myCoins = PlayerPrefs.GetInt("MyCoins", this.myCoins);
    }

    private void OnEnable() 
    {
        if(GameManager.Instance != null) GameManager._OnSucess += TasksCompleted;
        if(GameManager.Instance != null) GameManager._OnFailed += TasksFailed;
    }

    private void OnDisable() 
    {
        if(GameManager.Instance != null) GameManager._OnSucess -= TasksCompleted;
        if(GameManager.Instance != null) GameManager._OnFailed -= TasksFailed;
    }

    public void Score(string color, bool colorSucess, bool containerSucess)
    {
        int score = 0;

        if(containerSucess)
        {
            score += containerPoints;
        }
        else
        {
            score -= containerPoints / 2;
            this.containersFailures += 1;
        }


        if(color != "empty")
        {
            if(colorSucess)
            {
                if(color == "blue" || color == "red" || color == "yellow")
                {
                    score += this.primaryPoints;
                }
                else if(color == "green" || color == "purple" || color == "orange")
                {
                    score += this.secundaryPoints;
                }

                this.totalGain += score;
            }
            else
            {
                if(color == "blue" || color == "red" || color == "yellow")
                {
                    score -= this.primaryPoints / 2;
                    this.p_colorFailures += 1;
                }
                else if(color == "green" || color == "purple" || color == "orange")
                {
                    score -= this.secundaryPoints / 2;
                    this.s_colorFailures += 1;
                }
            }
        }
        ScoreToAdd(score);
        ChangeScore(score);
    }

    private void TasksCompleted()
    {
        this.tasksCompleted += 1;
    }

    private void TasksFailed()
    {
        this.tasksFailed += 1;
    }
    private void ChangeScore(int score)
    {
        this.myScore += score;
    }

    public int Score()
    {
        return myScore;
    }

    public void ScoreToAdd(int score)
    {
        _OnScoreToAdd?.Invoke(score);
    }

    public int Return_TasksCompleted()
    {
        return tasksCompleted;
    }

    public int Return_PrimaryColorFailures()
    {
        return p_colorFailures;
    }

    public int Return_SecundaryColorFailures()
    {
        return s_colorFailures;
    }

    public int Return_ContainersFailure()
    {
        return containersFailures;
    }

    public int Return_TasksFailed()
    {
        return tasksFailed;
    }

    public int Return_Score()
    {
        return myScore;
    }

    public int Return_TotalGain()
    {
        return totalGain;
    }

    public void ChangeCoins(int coins)
    {
        this.myCoins += coins;
        PlayerPrefs.SetInt("MyCoins", this.myCoins);
    }
}
