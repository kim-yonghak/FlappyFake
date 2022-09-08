using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    private Animator anim = null;
    public ImageNumber highScore = null;
    public ImageNumber myScore = null;
    public GameObject newRecordText = null;
    public GameObject restartButton = null;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        highScore = GameObject.Find("HighScoreNumber").GetComponent<ImageNumber>();
        myScore = GameObject.Find("PlayerScoreNumber").GetComponent<ImageNumber>();
    }

    public void OnGameOver()
    {
        anim.SetTrigger("GameOver");
        highScore.Number = GameManager.Inst.HighScore;
        myScore.Number = GameManager.Inst.Score;
        if(GameManager.Inst.HighScore < GameManager.Inst.Score)
        {
            newRecordText.SetActive(true);
        }
        else
        {
            newRecordText.SetActive(false);
        }
    }

    public void OnRestartButtonPress()
    {
        SceneManager.LoadScene(0);
    }
}
