using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject PauseScreen;

    public GameObject GolfBall;

    public Rigidbody rb;

    public GameObject WinScreen;

    public GameObject MainMenu;

    public GameObject StrokesText;

    public TextMeshProUGUI strokesText;

    public Vector3 StartPos;

    [SerializeField] GolfBall golfBall;

    public bool isWon = false;

    public int iLevel;    

    void Start()
    {
        //Menu();
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Pause();

        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }

        if (isWon)
        {
            WinScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else if (!isWon && Time.timeScale == 1)
        {
            WinScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

    void FixedUpdate()
    {
        if (golfBall.strokes == 1) 
        {
            strokesText.text = golfBall.strokes.ToString("0") + " stroke"; 
        }
        else strokesText.text = golfBall.strokes.ToString("0") + " strokes";
    }

    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1)
        {
            PauseScreen.SetActive(true);
            Time.timeScale = 0;
            golfBall.isIdle = false;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0)
        {
            PauseScreen.SetActive(false);
            Time.timeScale = 1;
        }        
    }

    public void play()
    {
        MainMenu.SetActive(false);
        PauseScreen.SetActive(false);
        golfBall.isIdle = false;

        Time.timeScale = 1;
    }

    public void Restart()
    {
        GolfBall.transform.position = StartPos;

        golfBall.stop();

        Time.timeScale = 1;
        golfBall.strokes = 0;

        golfBall.isIdle = false;

        PauseScreen.SetActive(false);
        MainMenu.SetActive(false);
        WinScreen.SetActive(false);
        StrokesText.SetActive(true);
        GolfBall.SetActive(true);

        isWon = false;
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        iLevel++;
        SceneManager.LoadScene(iLevel);
    }

    //public void NewGame()
    //{
    //    MainMenu.SetActive(false);
    //    StrokesText.SetActive(true);
    //    GolfBall.SetActive(true);

    //    GolfBall.transform.position = StartPos;

    //    golfBall.stop();

    //    golfBall.strokes = 0;

    //    Time.timeScale = 1;
    //}


}
