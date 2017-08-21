using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public static UIController Instance;
    [SerializeField]
    private Slider trainControllSlider;
    [SerializeField]
    private Button pickUpButton;
    [SerializeField]
    private Text score;
    [SerializeField]
    private Text timer;
    [SerializeField]
    private GameObject endGameWindow;
    [SerializeField]
    private Text endGameScore;
    [SerializeField]
    private Button restartGameButton;
    [SerializeField]
    private Button cameraButton;
    [SerializeField]
    private Button hornButton;

    void Awake()
    {
        Instance = this;
    }
    // Use this for initialization

    // Update is called once per frame
    public float GetSliderValue()
    {
        return trainControllSlider.value;
    }
    public void SetScore(int score)
    {
        if(this.score)
            this.score.text = score.ToString();
        else
            Debug.LogError("score UI.Text is Null!");
    }
    public void SetTimerText(int minutes,int seconds)
    {
        if (timer)
        {
            timer.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }
        else
        {
            Debug.LogError("timer UI.Text is Null!");
        }
    }
    public void ActivatePickUpButton(System.Action pickUpAction)
    {
        if (pickUpButton)
        {
            if (!pickUpButton.IsActive())
            {
            
                pickUpButton.gameObject.SetActive(true);
                pickUpButton.onClick.AddListener(pickUpAction.Invoke);
            }
        }
        else
        {
            Debug.LogError("pickUpButton is Null!");
        }
    }     
    public void DeactivatePickUpButton()
    {
        if (pickUpButton)
        {
            pickUpButton.gameObject.SetActive(false);
            pickUpButton.onClick.RemoveAllListeners();
        }
        else
        {
            Debug.LogError("pickUpButton is Null!");
        }
    }
    public void ActivateEndGameWindow(System.Action restartAction)
    {
        DeactivatePickUpButton();
        trainControllSlider.gameObject.SetActive(false);
        score.gameObject.SetActive(false);
        timer.gameObject.SetActive(false);
        cameraButton.gameObject.SetActive(false);
        hornButton.gameObject.SetActive(false);

        endGameWindow.SetActive(true);
        endGameScore.text = GamePlayController.Instance.GetScore().ToString();
        restartGameButton.onClick.AddListener(restartAction.Invoke);
    }
    public void ActivateCameraButton(System.Action cameraButtonAction)
    {
        if (cameraButton)
        {
            cameraButton.gameObject.SetActive(true);
            cameraButton.onClick.AddListener(cameraButtonAction.Invoke);
        }
        else
        {
            Debug.LogError("camera button is Null!");
        }
    }
}
