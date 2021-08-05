using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float xMinPos;
    public float xMaxPos;
    public float yMinPos;
    public float yMaxPos;

    private Vector3 position;

    private Transform player;
    private Vector2 playerPos;

    private void Start()
    {
        position.z = -1;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        MoveCamera();
    }

    //フロア移動に伴い制限を変更
    public void MoveFloor(float[] limitation)
    {
        xMinPos = limitation[0];
        xMaxPos = limitation[1];
        yMinPos = limitation[2];
        yMaxPos = limitation[3];
    }

    private void MoveCamera()
    {
        playerPos = player.position;
        if (playerPos.x < xMaxPos)
            if (playerPos.x > xMinPos) position.x = playerPos.x;
            else position.x = xMinPos;
        else position.x = xMaxPos;

        if (playerPos.y < yMaxPos)
            if (playerPos.y > yMinPos) position.y = playerPos.y;
            else position.y = yMinPos;
        else position.y = yMaxPos;

        this.transform.position = position;
    }
}
