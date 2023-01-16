using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

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

        }
    }

}
