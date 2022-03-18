using UnityEngine;
using System.Collections;
using TMPro;

public class ScoreHUD : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text scoreToAddText;
   private int myScore = 0;

   private void OnEnable() 
   {
        ScoreManager._OnScoreToAdd += ScoreToAdd;    
   }
   private void OnDisable() 
   {
        ScoreManager._OnScoreToAdd -= ScoreToAdd;    
   }

   private void Update() 
   {
       Score();
   }

   private void Score()
   {
       if(this.myScore == ScoreManager.Instance.Score()) return;

       this.myScore = ScoreManager.Instance.Score();
       ScoreUI();
   }

   private void ScoreUI()
   {
       this.scoreText.text = "$ " + this.myScore;
   }

   private void ScoreToAdd(int score)
   {
       StartCoroutine(Score(score));
   }

   private IEnumerator Score(int score)
   {
       this.scoreToAddText.gameObject.SetActive(true);

       if(score > 0) this.scoreToAddText.color = Color.green;
       else if(score < 0) this.scoreToAddText.color = Color.red;

       this.scoreToAddText.text = "+ $" + score;
       yield return new WaitForSeconds(0.5f);

       this.scoreToAddText.gameObject.SetActive(false);
   }
}
