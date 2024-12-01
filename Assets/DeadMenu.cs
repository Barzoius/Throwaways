using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public GameObject deadMenuUi;

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
        deadMenuUi.SetActive(false);
        hpText.SetActive(true);
        // TODO (1) Load the scene for the gamestart


        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void ShowYouDiedMenu()
    {
        deadMenuUi.SetActive(true);
        hpText.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
