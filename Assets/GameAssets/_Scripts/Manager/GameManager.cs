using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum GameState {game, finish}
    public GameState myState;
    public delegate void OnGame();
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

    public void OnCompleted()
    {
        this.myState = GameState.finish;
    }
    #endregion
}
