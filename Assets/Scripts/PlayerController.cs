using UnityEngine;

//プレイヤーのコントロールクラス
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MobState
{
    [SerializeField]
    float moveSpeed = 1.0f;
    private Rigidbody2D rigidBody;
    private Vector2 inputAxis;

    private int friendNum; //パーティー内での順番
    private int firstHp;   //初期HP

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        // 衝突時にobjectを回転させない設定
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        //ステージの初期座標を取得
        SetFirstPosition(StageManager.instance.GetFirstPosition());

        Hp = firstHp = 250;
        canAttack = true;
        friendNum =  GameManager.instance.AddToFriendStateList(this, true);
        ShowHp();
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
        if (canAttack)
        {

        }
    }
    public override void Damaged(int damageValue)
    {
        int damageCut = 0;//ステータスから取得
        int damage = damageValue - damageCut;
        if(damage > 0)
        {
            Hp = (Hp - damage);
            ShowHp();
        }
    }
    //HPのUIを更新
    void ShowHp()
    {
        string hpText = Hp.ToString() + " / " + firstHp.ToString();
        MenuUI.instance.SetHpUi(friendNum, hpText);
    }

    //ステージでの初期位置を設定
    private void SetFirstPosition(int[] position)
    {
        transform.position = new Vector2(position[0], position[1]);
    }
    //キー入力を取得
    private void GetInput()
    {
        inputAxis.x = Input.GetAxis("Horizontal");
        inputAxis.y = Input.GetAxis("Vertical");
    }
    
}
