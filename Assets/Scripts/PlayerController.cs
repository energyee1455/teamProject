using UnityEngine;


//プレイヤーのコントロールクラス
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MobState
{
    [SerializeField]
    float moveSpeed = 1.0f;
    private Rigidbody2D rigidBody;
    private Vector2 inputAxis;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        // 衝突時にobjectを回転させない設定
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        //ステージの初期座標を取得
        SetFirstPosition(StageManager.instance.GetFirstPosition());
    }

    void Update()
    {
        // x,ｙの入力値を得る
        GetInput();
    }
    void FixedUpdate()
    {
        Move();
    }

    protected override void Move()
    {
        // 速度を代入する
        rigidBody.velocity = inputAxis.normalized * moveSpeed;
    }

    protected override void Attack()
    {
        throw new System.NotImplementedException();
    }


    //ステージでの初期位置を設定
    private void SetFirstPosition(int[] position)
    {
        transform.position = new Vector2(position[0], position[1]);
    }

    private void GetInput()
    {
        inputAxis.x = Input.GetAxis("Horizontal");
        inputAxis.y = Input.GetAxis("Vertical");
    }

    
}
