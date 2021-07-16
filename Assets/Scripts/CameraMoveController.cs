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
    public Transform player;
    private Vector2 playerPos;

    private void Start()
    {
        position.z = -1;
    }

    private void FixedUpdate()
    {
        playerPos = player.position;
        if (playerPos.x < xMax && playerPos.x > xMin) position.x = playerPos.x;
        if (playerPos.y < yMax && playerPos.y > yMin) position.y = playerPos.y;
        this.transform.position = position;
        
    }
}
