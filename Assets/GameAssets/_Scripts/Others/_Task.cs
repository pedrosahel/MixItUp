using System.Collections;
using Main;
using UnityEngine;

public class _Task : MonoBehaviour
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
        if(GameManager.Instance == null) return;

        GameManager._OnSucess += CallResetTimer;
        GameManager._OnFailed += CallResetTimer;
    }

    private void OnDisable() 
    {
        if(GameManager.Instance == null) return;

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
            if(_TaskManager.Instance != null)
            {
                _TaskManager.Instance.NextTask();
                this.timeToNextTask = this.timerHolder;
            }
        }
    }

    private void MatchTimer()
    {
        this.timeMatch -= Time.fixedDeltaTime;

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
