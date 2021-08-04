using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FriendController : MobState
{
    //動き用の変数
    [SerializeField]
    readonly float moveSpeed = 3.0f;
    private Rigidbody2D rigidBody;

    private int friendNum; //パーティー内での順番
    private int firstHp = 220;   //初期HP

    private PlayerController playerCon;


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
        Hp = firstHp;
        friendNum = GameManager.instance.AddToFriendStateList(this, true);
        ShowHp();

        //プレイヤークラスを取得
        playerCon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerCon.playerTrail[0] = this.transform.position;
    }

    void Update()
    {
        Move();
    }

    void FixedUpdate()
    {
        
    }

    private float warpTh = 10;
    private float moveTh = 1f;

    protected override void Move()
    {
        // プレイヤーに追従する
        Vector2 pos = transform.position;
        Vector2 def = playerCon.playerTrail[0] - pos;

        if (def.magnitude > warpTh)
        {
            Debug.Log("ワープ");
            this.transform.position = playerCon.playerTrail[0];
        }
        else if (def.magnitude > moveTh)
        {
            rigidBody.velocity = def;
        }
        else
        {
            rigidBody.velocity = new Vector2(0, 0);
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
        if (damage > 0)
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

}
