using UnityEngine.UI;
using UnityEngine;
using Main;
public class TaskHUD : MonoBehaviour
{
    [SerializeField] private Animator anim;

    [SerializeField] private Slider taskTimer;

    [SerializeField] private Image fillImage;

    [SerializeField] private Image sucessImage;
    [SerializeField] private Image failedImage;

    [SerializeField] private Image task;
    [SerializeField] private Sprite[] i_tasks;
    private enum Colors {blue, red, yellow, purple, green, orange}
    private enum Frascos {Frasco_01, Frasco_02, Frasco_03}

    private Colors h_colors;
    private Frascos h_frasco;

    private int i_index;

    private Task mainTask;

    private void OnEnable() 
    {
        this.mainTask = FindObjectOfType<Task>();

        TaskManager._Task += GetTask;    
        GameManager._OnSucess += Sucess;
        GameManager._OnFailed += Failed;
    }

    private void OnDisable()
    {
        TaskManager._Task -= GetTask; 
        GameManager._OnSucess -= Sucess;
        GameManager._OnFailed -= Failed;   
    }

    private void Update()
    {
        ShowTaskTimer();
    }
    private void GetTask(string color, string frasco, bool tampa)
    {
        this.h_colors = (Colors)System.Enum.Parse(typeof(Colors), color);
        this.h_frasco = (Frascos)System.Enum.Parse(typeof(Frascos), frasco);

        DecideTask();
    }

    private void DecideTask()
    {
        switch(h_frasco)
        {
            case Frascos.Frasco_01:
                switch(h_colors)
                {
                    case Colors.blue:
                        this.i_index = 0;
                    break;
                    case Colors.red:
                        this.i_index = 1;
                    break;
                    case Colors.yellow:   
                        this.i_index = 2;
                    break;
                    case Colors.purple:
                        this.i_index = 3;
                    break;
                    case Colors.green:
                        this.i_index = 4;
                    break;
                    case Colors.orange:
                        this.i_index = 5;
                    break;
                }
            break;
            case Frascos.Frasco_02:
                switch(h_colors)
                {
                    case Colors.blue:
                        this.i_index = 6;
                    break;
                    case Colors.red:
                        this.i_index = 7;
                    break;
                    case Colors.yellow:   
                        this.i_index = 8;
                    break;
                    case Colors.purple:
                        this.i_index = 9;
                    break;
                    case Colors.green:
                        this.i_index = 10;
                    break;
                    case Colors.orange:
                        this.i_index = 11;
                    break;
                }
            break;
            case Frascos.Frasco_03:
                switch(h_colors)
                {
                    case Colors.blue:
                        this.i_index = 12;
                    break;
                    case Colors.red:
                        this.i_index = 13;
                    break;
                    case Colors.yellow:   
                        this.i_index = 14;
                    break;
                    case Colors.purple:
                        this.i_index = 15;
                    break;
                    case Colors.green:
                        this.i_index = 16;
                    break;
                    case Colors.orange:
                        this.i_index = 17;
                    break;
                }
            break;
        }

        ShowTask();
    }

    private void ShowTask()
    {
        
        this.sucessImage.gameObject.SetActive(false);
        this.failedImage.gameObject.SetActive(false);

        this.anim.SetTrigger("onTask");

        this.task.sprite = this.i_tasks[this.i_index];
    }

    private void ShowTaskTimer()
    {
        this.taskTimer.value = mainTask.GetTimer();

        this.fillImage.color = this.taskTimer.value > 0.5f ? Color.green : Color.red;
    }

    private void Sucess()
    {
        this.sucessImage.gameObject.SetActive(true);
    }

    private void Failed()
    {
        this.failedImage.gameObject.SetActive(true);
    }
}
