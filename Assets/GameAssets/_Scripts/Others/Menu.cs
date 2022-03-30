using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private Sprite[] tutorialSprites;
    [SerializeField] private Image tutorial;
    [SerializeField] private TMPro.TMP_Text t_myCoins;

    private int index = 0;
    private int myCoins = 0;

    private void Awake() 
    {
        if(GameManager.Instance != null) return;
        
        this.myCoins = PlayerPrefs.GetInt("MyCoins", this.myCoins);

        this.t_myCoins.text = "$" + this.myCoins;
    }

    public void NextImage()
    {
        if(index + 1 > this.tutorialSprites.Length - 1) index = this.tutorialSprites.Length - 1;
        else index++;
        ShowImage(index);
    }

    public void PreviousImage()
    {
        if(index - 1 < 0) index = 0;
        else index--;
        ShowImage(index);
    }

    private void ShowImage(int i)
    {
        this.tutorial.sprite = this.tutorialSprites[i];
    }

    public void ChangeScene(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
