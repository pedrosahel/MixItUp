using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public delegate void OnGame();

    public static event OnGame _OnCompletedAllTasks;

    public static event OnGame _OnDelivery;

    public static event OnGame _OnSucess;
    public static event OnGame _OnFailed;

    private new void Awake() 
    {
        base.Awake();
    }

    #region Public Functions.
    public void OnSucess()
    {
        _OnSucess?.Invoke();
    }

    public void OnFailed()
    {
        _OnFailed?.Invoke();
    }

    public void OnDelivery()
    {
        _OnDelivery?.Invoke();
    }

    public void OnCompleted()
    {
        _OnCompletedAllTasks?.Invoke();
        print("CompletedAllTasks");
    }
    #endregion
}
