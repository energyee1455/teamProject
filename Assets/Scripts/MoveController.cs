using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class MoveController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 1.0f;
    private Rigidbody2D rigidBody;
    private Vector2 inputAxis;
    [SerializeField]
    public FixedJoystick joystick;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        // 衝突時にobjectを回転させない設定
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        // x,ｙの入力値を得る
        inputAxis.x = joystick.Horizontal;
        inputAxis.y = joystick.Vertical;
    }

    private void FixedUpdate()
    {
        // 速度を代入する
        rigidBody.velocity = inputAxis.normalized * moveSpeed;
    }
}
