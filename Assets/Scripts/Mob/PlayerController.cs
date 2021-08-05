using System.Collections;
using UnityEngine;

//プレイヤーのコントロールクラス
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MobState
{
    //動き用の変数
    private float moveSpeed = 2f;
    private Rigidbody2D rigidBody;
    private Vector2 inputAxis;

    private int friendNum; //パーティー内での順番
    private int firstHp = 250;   //初期HP

    //プレイヤーキャラが向いている方向
    private enum PointerDirection
    {
        Right,
        Left,
        Up,
        Down
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        // 衝突時にobjectを回転させない設定(スクリプトに書いてなくてもいい)
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;

        //フロア1の初期座標へ移動
        GotoFirstPosition(StageManager.instance.GetFirstPosition());

        canAttack = true;

        //HPをセット<後から変更>
        //ステージ跨ぎする場合の処理を，PartyManagerで実装
        Hp = firstHp;
        friendNum =  GameManager.instance.AddToFriendStateList(this, true);
        ShowHp();

    }

    //仲間の追従機能関係
    //プレイヤーの移動経路格納配列
    public Vector2[] playerTrail = new Vector2[3] {
        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0)
    };
    private Vector2 prePos; //移動前の座標
    private int partyMemberNum;  //プレイヤーを除くパーティメンバーの人数
    private float posTh = 1.5f; //移動経路格納における距離の閾値
    void Update()
    {
        // x,ｙの入力値を得る
        GetInput();
    }
    void FixedUpdate()
    {
        //移動と移動経路格納
        Move();
        SetTrail();
    }

    //キー入力を取得
    private void GetInput()
    {
        inputAxis.x = Input.GetAxis("Horizontal");
        inputAxis.y = Input.GetAxis("Vertical");
    }
    //入力から移動
    protected override void Move()
    {
        // 速度を代入する
        rigidBody.velocity = inputAxis.normalized * moveSpeed;
    }
    //移動経路の格納
    private void SetTrail()
    { 
        //前回の座標と今の座標の間の距離が閾値より大きい時，プレイヤーの通った座標をずらして格納する
        if (((Vector2)transform.position - prePos).magnitude > posTh)
        {
            prePos = transform.position;

            int i = GameManager.instance.PartyMenberNum() - 1;
            while (i > 0)
            {
                
                playerTrail[i].x = playerTrail[i - 1].x;
                playerTrail[i].y = playerTrail[i - 1].y;
                i--;
            }
            playerTrail[0].x = transform.position.x;
            playerTrail[0].y = transform.position.y;
        }
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

    //ステージでの初期位置に移動　←　StageManager.Move()
    public void GotoFirstPosition(int[] position)
    {
        transform.position = new Vector2(position[0], position[1]);
        prePos = transform.position;
    }
}
