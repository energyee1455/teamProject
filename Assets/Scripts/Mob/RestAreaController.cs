using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestAreaController : MonoBehaviour
{
    //動き用の変数
    private float moveSpeed = 2f;
    private Rigidbody2D rigidBody;
    private Vector2 inputAxis;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        // 衝突時にobjectを回転させない設定(スクリプトに書いてなくてもいい)
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;

        //休憩エリアの初期座標へ移動
    }

    void Update()
    {
        GetInput();
    }
    void FixedUpdate()
    {
        //移動と移動経路格納
        Move();
    }

    //キー入力を取得
    private void GetInput()
    {
        inputAxis.x = Input.GetAxis("Horizontal");
        inputAxis.y = Input.GetAxis("Vertical");
    }

    //入力から移動
    private void Move()
    { 
        // 速度を代入する
        rigidBody.velocity = inputAxis.normalized * moveSpeed;
    }
}
