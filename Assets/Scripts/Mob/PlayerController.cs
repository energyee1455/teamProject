using UnityEngine;

//プレイヤーのコントロールクラス
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MobState
{
    //動き用の変数
    [SerializeField]
    readonly float moveSpeed = 1.0f;
    private Rigidbody2D rigidBody;
    private Vector2 inputAxis;

    private int friendNum; //パーティー内での順番
    private int firstHp;   //初期HP

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        // 衝突時にobjectを回転させない設定(スクリプトに書いてなくてもいい)
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        //ステージの初期座標を取得
        SetFirstPosition(StageManager.instance.GetFirstPosition());

        canAttack = true;

        //HPをセット<後から変更>
        //ステージ跨ぎする場合の処理を，PartyManagerで実装
        Hp = firstHp = 250;
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
    public override void Attacked(int damageValue)
    {
        int damageCut = 0;//ステータスから取得
        int damage = damageValue - damageCut;
        if(damage > 0)
        {
            state = State.Damaged;
            Hp = (Hp - damage);
            ShowHp();
        }
    }

    //HPのUIを更新(後で削除するかも)
    void ShowHp()
    {
        string hpText = Hp.ToString() + " / " + firstHp.ToString();
        UiManager.instance.SetHpUi(friendNum, hpText);
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
