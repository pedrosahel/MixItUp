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
    private int myScore = 0;

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
            }
            else
            {
                if(color == "blue" || color == "red" || color == "yellow")
                {
                    score -= this.primaryPoints / 2;
                }
                else if(color == "green" || color == "purple" || color == "orange")
                {
                    score -= this.secundaryPoints / 2;
                }
            }
        }
        ScoreToAdd(score);
        ChangeScore(score);
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
}
