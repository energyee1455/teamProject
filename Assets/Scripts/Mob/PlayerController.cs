using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//プレイヤーのコントロールクラス
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MobState
{
    //動き用の変数
    private float moveSpeed = 2f;
    private Rigidbody2D rigidBody;
    private Vector2 inputAxis;
    private int firstHp = 250;   //初期HP
    //HP表示用
    public GameObject HpviewObject;
    Slider hpview;

    //仲間の追従機能関係
    //プレイヤーの移動経路格納配列
    public Vector2[] playerTrail;

    private Vector2 prePos; //移動前の座標
    private GameManager gameManager;
    private int partyMemberNum;  //プレイヤーを除くパーティメンバーの人数
    private float posTh = 1f; //移動経路格納における距離の閾値

    //攻撃用オブジェクト
    public GameObject damageObject;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        // 衝突時にobjectを回転させない設定(スクリプトに書いてなくてもいい)
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;

        //HPをセット<後から変更>
        //ステージ跨ぎする場合の処理を，PartyManagerで実装
        Hp = firstHp;
        state = State.Normal;
        canAttack = true;

        //フィールドだった時の処理
        gameManager = GameManager.instance;
        if (gameManager.stage == GameManager.Stage.Field)
        {
            gameManager.AddPartyMember();
            //HP表示
            hpview = HpviewObject.GetComponent<Slider>();
            Hp = firstHp;
            hpview.maxValue = firstHp;
            hpview.value = firstHp;
        }

        partyMemberNum = gameManager.partyMenberNum;

        //とりあえず最初は味方が見えないようにステージ外に設定
        playerTrail= new Vector2[3]{
            new Vector2(-10, -10),
            new Vector2(-10, -10),
            new Vector2(-10, -10)
        };
    }

    void Update()
    {
        // x,ｙの入力値を得る
        if(state == State.Normal || state == State.Damaged)
        {
            GetInput();
        }
        
    }
    void FixedUpdate()
    {
        //移動と移動経路格納
        Move();
        if(partyMemberNum > 0) SetTrail();
    }
    
    //キー入力を取得
    private void GetInput()
    {
        inputAxis.x = Input.GetAxis("Horizontal");
        inputAxis.y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack(damageObject);
        }
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
            int i = partyMemberNum-1;
            while (i > 0)
            {
                playerTrail[i].x = playerTrail[i - 1].x;
                playerTrail[i].y = playerTrail[i - 1].y;
                i--;
            }
            Debug.Log("set");
            playerTrail[0].x = transform.position.x;
            playerTrail[0].y = transform.position.y;
        }
    }

    //攻撃はこの中に書く
    protected override void Attack(GameObject damageObject)
    {
        if (canAttack)
        {
            GameObject copied = Object.Instantiate(damageObject) as GameObject;
            copied.transform.position = this.gameObject.transform.position;

            Vector3 pd = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position; //エビ→カーソル方向のベクトル
            Vector3 pn = pd.normalized; //正規化

            float angle = Vector3.Angle(new Vector3(0, 1, 0), pn);
            if (pn.x > 0) angle = -angle;
            copied.transform.rotation = Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, angle);
        }
    }

    public override void Attacked(int damageValue)
    {
        int damageCut = 0;//ステータスから取得
        int damage = damageValue - damageCut;
        if (damage > 0)
        {
            HpviewObject.SetActive(true);
            state = State.Damaged;
            Hp -= damage;
            hpview.value = Hp;

            if (Hp <= 0)
            {
                state = State.Die;
            }
        }
    }
    

    //フロア移動
    public void GotoFirstPosition(int[] position)
    {
        transform.position = new Vector2(position[0], position[1]);
        prePos = transform.position;
    }

    public void StopMove()
    {
        state = State.Stop;
    }
    public void StartMove()
    {
        state = State.Normal;
    }
}
