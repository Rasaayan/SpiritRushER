using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public Transform player;

    public GameObject barrierLeft;
    public GameObject barrierRight;

    public Vector3 leftPosOffset;
    public Vector3 rightPosOffset;

    // Start is called before the first frame update
    void Start()
    {

    }
    
    void Update()
    {
        leftPosOffset.y = player.transform.position.y;
        leftPosOffset.z = player.transform.position.z;
        leftPosOffset.x = -4.2f;
        barrierLeft.transform.position = leftPosOffset;

        rightPosOffset.y = player.transform.position.y;
        rightPosOffset.z = player.transform.position.z;
        rightPosOffset.x = 4.2f;
        barrierRight.transform.position = rightPosOffset;
    }
}
