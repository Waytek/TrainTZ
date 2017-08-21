using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour {
    public static GamePlayController Instance;
    public int gameMinutes = 5;
    private int score = 0;
    private System.DateTime gameTime;
    void Awake()
    {
        Instance = this;
    }
    
	// Use this for initialization
	void Start () {
        
        StartGame();
    }
    void StartGame()
    {
        Time.timeScale = 1;
        gameTime = new System.DateTime().AddMinutes(gameMinutes);
        UIController.Instance.SetTimerText(gameTime.Minute, gameTime.Second);
        InvokeRepeating("OutTime",1,1);
    }
    void OutTime()
    {

        gameTime = gameTime.AddSeconds(-1);
        if(gameTime.Minute == 0 && gameTime.Second == 0)
        {
            GameEnd();
            CancelInvoke("OutTime");
        }        
            UIController.Instance.SetTimerText(gameTime.Minute, gameTime.Second);
    }

	public void AddScore(int score)
    {
        this.score += score;
        
        UIController.Instance.SetScore(this.score);
    }
    public int GetScore()
    {
        return score;
    }
    void GameEnd()
    {
        Time.timeScale = 0;
        UIController.Instance.ActivateEndGameWindow(Restart);
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
}
