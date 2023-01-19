using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gameManager.collectedAll)
        {
            if (SceneManager.GetActiveScene().buildIndex + 1 == SceneManager.sceneCountInBuildSettings - 1) 
            {
                gameManager.isLastScene = true;
            }
            gameManager.Cutscene();
            StartCoroutine(WaitForSceneLoad());

        }
    }

    private IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

}
