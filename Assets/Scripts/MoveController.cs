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

    [SerializeField] private string RightMoveKey = "D";
    [SerializeField] private string LeftMoveKey = "A";
    [SerializeField] private string UpperMoveKey = "W";
    [SerializeField] private string LowerMoveKey = "S";

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        // 衝突時にobjectを回転させない設定
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        // x,ｙの入力値を得る
        GetInput();                                
    }

    private void GetInput()
    {
        inputAxis.x = Input.GetAxis("Horizontal");
        inputAxis.y = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        // 速度を代入する
        rigidBody.velocity = inputAxis.normalized * moveSpeed;
    }
}
