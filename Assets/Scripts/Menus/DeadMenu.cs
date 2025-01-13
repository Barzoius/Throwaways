using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public GameObject deadMenuUi;
    public GameObject winMenuUi;


    public GameObject hpText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (GameIsPaused)
            {
                ShowYouDiedMenu();
            }
            else
            {
                ShowYouDiedMenu();
            }
        }
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;

        SceneManager.LoadScene(0);
    }

    public void ShowYouDiedMenu()
    {
        deadMenuUi.SetActive(true);
        hpText.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ShowYouWonMenu()
    {
        winMenuUi.SetActive(true);
        hpText.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
