using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private Sprite[] levelImgs;
    [SerializeField] private Image targetImg;
    [SerializeField] private string[] levelName;
    [SerializeField] private TextMeshProUGUI levelText;

    private int counter = 0;

    private void Start()
    {
        targetImg.sprite = levelImgs[counter];
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(counter + 1);
    }

    public void NextLevel()
    {
        counter++;
        Debug.Log("Counter : " + counter);
        if (counter > levelImgs.Length)
            counter = 0;

        targetImg.sprite = levelImgs[counter];
        levelText.text = levelName[counter];
            
    }

    public void PrevLevel()
    {
        counter--;
        Debug.Log("Counter : " + counter);
        if (counter < 0)
            counter = 0;

        targetImg.sprite = levelImgs[counter];
        levelText.text = levelName[counter];

    }

    public void QuitButton()
    {
        Application.Quit();
    }

}
