using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject playbtn;
    public GameObject tutorialText;
    public TextMesh tutorialBTNtext;
    bool readTutorial = false;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Tutorial()
    {
        if (readTutorial)
        {
            playbtn.SetActive(true);
            tutorialText.SetActive(false);
            readTutorial = false;
            tutorialBTNtext.text = "?";
        } else
        {
            playbtn.SetActive(false);
            tutorialText.SetActive(true);
            readTutorial = true;
            tutorialBTNtext.text = "x";
        }
    }
}
