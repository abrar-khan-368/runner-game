using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerTemp : MonoBehaviour
{

    [SerializeField] private PlayerController player;
    [SerializeField] private CameraController camera;
    [SerializeField] private GameObject countDown;
    [SerializeField] private UnityEngine.UI.Text countDownText;
    [SerializeField] private GameObject startButton;

    private void Start()
    {
        player.enabled = false;
        countDown.SetActive(false);
        camera.enabled = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        StartCoroutine(countDownTimer());
    }

    IEnumerator countDownTimer()
    {
        startButton.SetActive(false);
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
        player.enabled = true;
        camera.enabled = true;
    }

}
