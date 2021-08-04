using System.Collections;
using UnityEngine;

//プレイヤーのコントロールクラス
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MobState
{
    //動き用の変数
    [SerializeField]
    readonly float moveSpeed = 3.0f;
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

    //仲間がついてくる機能関係
    public Vector2[] playerTrail = new Vector2[3] {
        new Vector2(0,0),
        new Vector2(0,0),
        new Vector2(0,0)
    };
    private Vector2 prePos; //前の座標
    private int partyMemberNum = 2;  //プレイヤーを除くパーティメンバーの人数
    private float posTh = 2f; //距離の閾値

    /*
    IEnumerator SetTrail()
    {
        int i = 0;
        int j;
        while (true)
        {
            //前回の座標と今の座標の間の距離が閾値より大きい時，プレイヤーの通った座標の格納をずらす
            if(((Vector2)transform.position - prePos).magnitude > posTh)
            {
                if(i < partyMemberNum) i++;
                j = i;
                while(j > 0)
                {
                    playerTrail[j] = playerTrail[j-1];
                    j--;
                }
                playerTrail[0].x = transform.position.x;
                playerTrail[0].y = transform.position.y;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    */


    private void SetTrail()
    {
        int i = 0;
        int j;
        //前回の座標と今の座標の間の距離が閾値より大きい時，プレイヤーの通った座標の格納をずらす
        if (((Vector2)transform.position - prePos).magnitude > posTh)
        {
            if (i < partyMemberNum) i++;
            j = i;
            while (j > 0)
            {
                playerTrail[j] = playerTrail[j - 1];
                j--;
            }
            playerTrail[0].x = transform.position.x;
            playerTrail[0].y = transform.position.y;
        }
    }

    void Update()
    {
        // x,ｙの入力値を得る
        GetInput();
    }

    void FixedUpdate()
    {
        Move();
        SetTrail();
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

    //ステージでの初期位置に移動　←　StageManager.Move()
    public void GotoFirstPosition(int[] position)
    {
        transform.position = new Vector2(position[0], position[1]);
        prePos = transform.position;
    }

    //キー入力を取得
    private void GetInput()
    {
        inputAxis.x = Input.GetAxis("Horizontal");
        inputAxis.y = Input.GetAxis("Vertical");
    }
    
}
