using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenericUIButtons : MonoBehaviour
{
    private string mainMenuSceneName = "Title_Screen";
    private string moreInfoSceneName = "Path_Description";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeginFromTitleScreen()
    {
        // +2 to avoid the more info scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MoreInfoScene()
    {
        SceneManager.LoadScene(moreInfoSceneName);
    }

    public void ReturnToChooseFromMission()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
