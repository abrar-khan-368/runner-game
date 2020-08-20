using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject countDown;
    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private GameObject pauseGameCanvas;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private AudioSource backgroundThemeMusic;
    [SerializeField] private CurvedWorld curvedWorld;

    private bool played = false;

    private void Awake()
    {
        playerController.enabled = false;
        countDown.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(countDownTimer());
    }

    private void Update()
    {
        coinText.text = "" + playerController.coinCollected;

        if (playerController.isDead && !played)
        {
            StartCoroutine(GameOver());
        }

    }

    IEnumerator countDownTimer()
    {
        countDown.SetActive(true);
        countDownText.text = "03";
        yield return new WaitForSecondsRealtime(1f);
        countDownText.text = "02";
        yield return new WaitForSecondsRealtime(1f);
        countDownText.text = "01";
        yield return new WaitForSecondsRealtime(1f);
        countDownText.text = "GO!";
        yield return new WaitForSecondsRealtime(0.5f);
        countDown.SetActive(false);
        playerController.enabled = true;
        backgroundThemeMusic.enabled = true;
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3f);
        played = true;
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseGameCanvas.SetActive(true);
        backgroundThemeMusic.enabled = false;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseGameCanvas.SetActive(false);
        backgroundThemeMusic.enabled = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GotoHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

}
