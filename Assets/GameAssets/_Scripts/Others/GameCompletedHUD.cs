using TMPro;
using UnityEngine;

public class GameCompletedHUD : MonoBehaviour
{
    [SerializeField] private GameObject c_completedCanvas;
    [SerializeField] private GameObject[] mainScene;
    [SerializeField] private TMP_Text t_recebidos;
    [SerializeField] private TMP_Text t_desperdicado;
    [SerializeField] private TMP_Text t_completas;
    [SerializeField] private TMP_Text t_falhas;

    [SerializeField] private TMP_Text t_totalScore;

    private int recebidos;
    private int desperdicado;
    private int completas;
    private int falhas;

    private int totalScore;

    private int gameManagerState = 0;

    private void Update() 
    {
        if(GameManager.Instance == null) return;

        if(gameManagerState != (int)GameManager.Instance.myState)
        {
            foreach(GameObject obj in mainScene)
            {
                obj.SetActive(false);
            }
            this.c_completedCanvas.SetActive(true);
            ValorUpdate();
            TextUpdate();
            if(ScoreManager.Instance != null) ScoreManager.Instance.ChangeCoins(totalScore);
            this.gameManagerState = (int)GameManager.Instance.myState;
        }
    }
    private void TextUpdate()
    {
        float r = 33f;
        float g = 70f;
        float b = 255f;

        this.t_recebidos.text = "$" + recebidos;
        this.t_desperdicado.text = "- $" + desperdicado;
        this.t_completas.text = completas + "";
        this.t_falhas.text = falhas + "";
        if(totalScore > 0)
        {
            this.t_totalScore.color = new Color(r, g, b);
            this.t_totalScore.text = "$" + totalScore;
        }
        else if(totalScore < 0)
        {
            this.t_totalScore.color = Color.red;
            this.t_totalScore.text = "-$" + Mathf.Abs(totalScore);
        }
    }

    private void ValorUpdate()
    {
        this.recebidos = ScoreManager.Instance.Return_TotalGain();
        this.desperdicado = ((ScoreManager.Instance.Return_ContainersFailure()) + (ScoreManager.Instance.Return_PrimaryColorFailures() * 5) + (ScoreManager.Instance.Return_SecundaryColorFailures() * 10));
        this.completas = ScoreManager.Instance.Return_TasksCompleted();
        this.falhas = ScoreManager.Instance.Return_TasksFailed();
        this.totalScore = ScoreManager.Instance.Return_Score();
    }

}
