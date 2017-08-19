using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameplayController : MonoBehaviour {

    public static GameplayController instance;
    [SerializeField]
    private GameObject[] planes;

    [SerializeField]
    private Text scoreText, endScore, highScore;

    [SerializeField]
    private Button restartGameButton, infoButton, pauseButton;

    [SerializeField]
    private GameObject pausePanel, gameOverImage;

    [SerializeField]
    private Sprite[] medals;

    [SerializeField]
    private Image medalImage;

    void Awake()
    {
        MakeInstance();
        Time.timeScale = 0;

    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
	// Use this for initialization
    void Start()
    {
        planes[GameController.instance.GetSelectedPlane()].SetActive(true);
        pauseButton.gameObject.SetActive(true);

    }
    public void PauseGame()
    {
        if (PlaneScript.instance != null)
        {
            if (PlaneScript.instance.isAlive)
            {
                pausePanel.SetActive(true);
                gameOverImage.gameObject.SetActive(false);
                endScore.text = PlaneScript.instance.score.ToString();
                highScore.text = GameController.instance.GetHighScore().ToString();
                Time.timeScale = 0f;
                restartGameButton.onClick.RemoveAllListeners();
                restartGameButton.onClick.AddListener(() => ResumeGame());
                
            }
        }
    }

    public void MainMenu()
    {
        SceneFader.instance.LoadLevel("Main Menu");
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartGame(){
        SceneFader.instance.LoadLevel(SceneManager.GetActiveScene().name);
    }

    public void PlayGame()
    {
        scoreText.gameObject.SetActive(true);
        planes[GameController.instance.GetSelectedPlane()].SetActive(true);
        infoButton.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void PlayerDiedShowScore(int score)
    {
        //TODO unlock next available plane. When fallilng into 40-60 range, i unlock blue plane but the main menu
        // will not show me that as an option because the green plane isnt unlocked
        pauseButton.gameObject.SetActive(false);
        endScore.text = score.ToString();

        if (score > GameController.instance.GetHighScore())
        {
            GameController.instance.SetHighScore(score);
        }

        highScore.text = GameController.instance.GetHighScore().ToString();

        if (score <= 20)
        {
            medalImage.sprite = medals[0]; //Bronze medal
        }
        else if (score > 20 && score < 40)
        {
            medalImage.sprite = medals[1]; // Silver medal
            if (GameController.instance.IsGreenPlaneUnlocked() == 0)
            {
                GameController.instance.UnlockGreenPlane();
            }
        }
        else if (score >= 40 && score < 60)
        {
            medalImage.sprite = medals[2]; //Gold medal
            if (GameController.instance.IsBluePlaneUnlocked() == 0)
            {
                GameController.instance.UnlockBluePlane();
            }
        }
        else if (score >= 60)
        {
            if (GameController.instance.IsYellowPlaneUnlocked() == 0)
            {
                GameController.instance.UnlockYellowPlane();
            }
        }

        restartGameButton.onClick.RemoveAllListeners();
        restartGameButton.onClick.AddListener(() => RestartGame());

        pausePanel.SetActive(true);
        gameOverImage.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);

    }
}
