using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _TaskManager : Singleton<_TaskManager>
{
    public delegate void OnTask(string color, string frasco, bool tampa = false);

    public static event OnTask _Task;

    public ListOfTasks[] easyTask;

    public ListOfTasks[] hardTasks;

    private List<Task_SO> tasks;

    private bool deliveryBoxFull = false;

    private int currentTask = 0;

    private new void Awake() 
    {
        base.Awake();
        tasks = new List<Task_SO>();

        int randomNumber = Random.Range(0, this.easyTask.Length);
        int randomNumber2 = Random.Range(0, this.hardTasks.Length);

        GetRandomList(easyTask[randomNumber]);
        GetRandomList(hardTasks[randomNumber2]);
    }

    private void GetRandomList(ListOfTasks tasks)
    {
        for(int i = 0; i < tasks.tasks.Count -1; i++)
        {
                this.tasks.Add(tasks.tasks[i]);
        }
    }

    private void Start()
    {
        Task();
    }

    private void OnEnable()
    {
        DeliveryBoxManager._IsFull += Full;
        DeliveryBoxManager._IsEmpty += Empty;
    }

    private void OnDisable()
    {
        DeliveryBoxManager._IsFull -= Full;
        DeliveryBoxManager._IsEmpty -= Empty;
    }

    private void Task()
    {
        _Task?.Invoke(tasks[currentTask].myColor.ToString(), tasks[currentTask].myFrasco.ToString());
    }

    public void NextTask()
    {
        if(this.tasks.Count - 1 == 0 && GameManager.Instance != null) GameManager.Instance.OnCompleted();
        else
        {
            tasks.Remove(tasks[this.currentTask]);
            Task();
        }
    }

    public void TaskFailure()
    {
        if(GameManager.Instance != null) GameManager.Instance.OnFailed();
        if(this.tasks.Count - 1 == 0 && GameManager.Instance != null) GameManager.Instance.OnCompleted();
        else
        {
            tasks.Remove(tasks[this.currentTask]);
            Task();
        }
    }

    public void Descarte(string color, string frasco)
    {
        if(ScoreManager.Instance != null) ScoreManager.Instance.Score(color, false, false);
    }

    public void Check(string color, string frasco, bool tampa)
    {
         if(deliveryBoxFull) return;

            if(!deliveryBoxFull)
            {
                if(tampa)
                {
                    if(color == tasks[currentTask].myColor.ToString() && frasco == tasks[currentTask].myFrasco.ToString())
                    {
                        if(GameManager.Instance != null) GameManager.Instance.OnSucess();
                        if(ScoreManager.Instance != null) ScoreManager.Instance.Score(color, true, true);
                    }
                    else if(color == tasks[currentTask].myColor.ToString() && frasco != tasks[currentTask].myFrasco.ToString())
                    {
                        if(GameManager.Instance != null) GameManager.Instance.OnFailed(); 
                        if(ScoreManager.Instance != null) ScoreManager.Instance.Score(color, true, false);
                    }
                    else if(color != tasks[currentTask].myColor.ToString() && frasco == tasks[currentTask].myFrasco.ToString())
                    {
                        if(GameManager.Instance != null) GameManager.Instance.OnFailed(); 
                        if(ScoreManager.Instance != null) ScoreManager.Instance.Score(color, false, true);
                    }
                }
                else if (!tampa)
                {
                    if(GameManager.Instance != null) GameManager.Instance.OnFailed();
                    if(ScoreManager.Instance != null) ScoreManager.Instance.Score(color, false, false);
                }

                StartCoroutine(CallNextTask());
            }
    }

    public IEnumerator CallNextTask()
    {
        yield return new WaitForSeconds(0.5f);
        NextTask();
    }

    public void Full()
    {
        this.deliveryBoxFull = true;
    }

    public void Empty()
    {
        this.deliveryBoxFull = false;
    }
}
