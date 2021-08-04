using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveController : MonoBehaviour
{
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    private Vector3 position;

    private Transform camera;
    private Transform player;
    private Vector2 playerPos;

    private void Start()
    {
        position.z = -1;

        camera = this.transform;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    private void Update()
    {
        playerPos = player.position;



        if (playerPos.x < xMax)
            if (playerPos.x > xMin) position.x = playerPos.x;
            else position.x = xMin;
        else position.x = xMax;

        if (playerPos.y < yMax)
            if(playerPos.y > yMin) position.y = playerPos.y;
            else position.y = yMin;
        else position.x = yMax;

        this.transform.position = position;

        
    }
}
