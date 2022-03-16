using UnityEngine;
using System.Collections;
using Main;

public class Task : MonoBehaviour
{
    [SerializeField] private float timeMatch;
    [SerializeField] protected float timeToNextTask;

    private float timerHolder;
    private float timeMatchHolder;

    private void Start() 
    {
        this.timerHolder = this.timeToNextTask;
        this.timeMatchHolder = this.timeMatch;
    }

    private void OnEnable()
    {
        GameManager._OnSucess += CallResetTimer;
        GameManager._OnFailed += CallResetTimer;
    }

    private void OnDisable() 
    {
        GameManager._OnSucess -= CallResetTimer;
        GameManager._OnFailed -= CallResetTimer;
    }

    private void Update()
    {
        Timer();
        MatchTimer();
    }

    private void Timer()
    {
        this.timeToNextTask -= Time.fixedDeltaTime;

        if(this.timeToNextTask <= 0)
        {
            if(TaskManager.Instance != null)
            {
                TaskManager.Instance.RandomTask();
                this.timeToNextTask = this.timerHolder;
            }
        }
    }

    private void MatchTimer()
    {
        this.timeMatch -= Time.fixedDeltaTime;

        if(this.timeMatch <= this.timeMatchHolder / 2)
        {
            if(TaskManager.Instance != null)
            {
                TaskManager.Instance.ActivedSecundary("Half");
            }
        }

        if(this.timeMatch <= this.timeMatchHolder / 4)
        {
            if(TaskManager.Instance != null)
            {
                TaskManager.Instance.ActivedSecundary("Quarter");
            }
        }

        if(this.timeMatch <= 0)
        {
            print("Acabou o Tempo!");
            //endgame
        }
    }

    private void CallResetTimer()
    {
        StartCoroutine(ResetTimer());
    }
    private IEnumerator ResetTimer()
    {
        yield return new WaitForSeconds(0.5f);
        this.timeToNextTask = this.timerHolder;
    }

    public float GetTimer()
    {   
        return this.timeToNextTask / this.timerHolder;
    }
}
