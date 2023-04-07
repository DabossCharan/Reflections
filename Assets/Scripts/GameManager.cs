using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float totalMirrorShards;
    public float collectedMirrorShards;
    public Canvas gameCanvas;
    public Camera mainCamera;
    public TextMesh totalShardsTxt;
    public TextMesh collectedShardsTxt;
    public TextMesh slash;
    public TextMesh progress;
    public TextMesh newWorldTxt;

    public bool collectedAll;

    public GameObject background;
    public GameObject player;
    public GameObject[] obstacles;
    public GameObject mirror;
    public SpriteRenderer mirrorWorld;
    public GameObject newWorld;

    public bool isLastScene;



    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        gameCanvas = GameObject.Find("GameCanvas").GetComponent<Canvas>();
        gameCanvas.worldCamera = mainCamera;

        totalMirrorShards = GameObject.FindGameObjectsWithTag("MirrorShard").Length;
        collectedMirrorShards = 0;
        totalShardsTxt = GameObject.Find("TotalMirrorShards").GetComponent<TextMesh>();
        collectedShardsTxt = GameObject.Find("CollectedMirrorShards").GetComponent<TextMesh>();
        slash = GameObject.Find("Slash").GetComponent<TextMesh>();
        progress = GameObject.Find("Progress").GetComponent<TextMesh>();

        totalShardsTxt.text = totalMirrorShards.ToString();

        collectedAll = false;

        
        background = GameObject.FindGameObjectWithTag("Background");
        player = GameObject.FindGameObjectWithTag("Player");
        obstacles = GameObject.FindGameObjectsWithTag("Obstacles");
        mirror = GameObject.FindGameObjectWithTag("Mirror");
        mirrorWorld = GameObject.FindGameObjectWithTag("MirrorWorld").GetComponent<SpriteRenderer>();
        newWorld = GameObject.Find("NewWorld");
        newWorldTxt = newWorld.GetComponent<TextMesh>();
        newWorld.SetActive(false);

        isLastScene = false;
    }

    // Update is called once per frame
    void Update()
    {
        collectedShardsTxt.text = collectedMirrorShards.ToString();

        if (collectedMirrorShards == totalMirrorShards)
        {
            collectedShardsTxt.gameObject.SetActive(false);
            totalShardsTxt.gameObject.SetActive(false);
            slash.gameObject.SetActive(false);
            progress.text = "You Got All the Shards!";
            collectedAll = true;
            
        }

        
    }

    public void Cutscene()
    {
        progress.gameObject.SetActive(false);
        background.SetActive(false);
        player.SetActive(false);

        foreach(GameObject obstacle in obstacles)
        {
            obstacle.SetActive(false);
        }
        mirror.SetActive(false);
        mirrorWorld.enabled = false;
        if (isLastScene)
        {
            newWorldTxt.text = "It seems the end is near...";
        }
        newWorld.SetActive(true);
        
    }

}
