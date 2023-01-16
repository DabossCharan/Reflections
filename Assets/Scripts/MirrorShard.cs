using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorShard : MonoBehaviour
{
    private GameManager gameManager;
    private Vector3 startPos;
    private Vector3 toPos;
    private float time;
    private float duration;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        startPos = transform.position;
        toPos = new Vector3(startPos.x, startPos.y + 1, startPos.z);
        duration = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        time = Mathf.PingPong(Time.time / duration, 1);
        transform.position = Vector3.Lerp(startPos, toPos, time);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.collectedMirrorShards += 1;
            Destroy(gameObject);
        }
    }
}
