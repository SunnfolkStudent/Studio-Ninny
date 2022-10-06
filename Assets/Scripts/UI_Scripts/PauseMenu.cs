using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using Update = UnityEngine.PlayerLoop.Update;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public PlayerInput input;

    void Update()
    {
        if (input.escPressed && GameIsPaused)
        {
            print("lols");
            Resume();
        }
        
        else if (input.escPressed)
        {
            Pause();
        }
        
    }

    public void Resume()
    {
        print("nsaeg");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        
        input.characterControl = true;
    }
    public void Pause()
    {
        print("pased");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        input.characterControl = false;
    }

    public void LoadGame(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
