using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class FollowPlayer : MonoBehaviour
{
    private Transform playerPos;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(playerPos.position.x, transform.position.y, transform.position.z);

    }
}
