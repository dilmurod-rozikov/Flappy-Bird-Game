using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int score;
    public GameObject startBtn;
    public Player birdBek;
    public Text gameOverTxt;
    public float timerBack = 3;

    private void Start()
    {
        gameOverTxt.gameObject.SetActive(false);
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (birdBek.isDead)
        {
            gameOverTxt.gameObject.SetActive(true);
            timerBack -= Time.unscaledDeltaTime;//start counting back
        }

        gameOverTxt.text = "Restarting in " + (timerBack).ToString("0");

        if (timerBack < 0)
            RestartGame();

    }
    public void StartGame()
    {
        startBtn.SetActive(false);
        Time.timeScale = 1;
    }

    public void IncreaseScore()
    {
        score++;
    }
    public void GameOver()
    {
        Time.timeScale = 0;
    }
    public void RestartGame()
    {
        EditorSceneManager.LoadScene(0);//reload the scene initial state
    }
}
