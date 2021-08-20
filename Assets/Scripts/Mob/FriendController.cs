using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendController : MobState
{
    private Rigidbody2D rigidBody;

    public int friendNum; //パーティー内での順番
    private int firstHp = 220;   //初期HP

    private PlayerController playerCon;
    private float warpTh = 6;
    private float moveTh = 1f;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        // 衝突時にobjectを回転させない設定(スクリプトに書いてなくてもいい)
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;

        //プレイヤークラスを取得
        playerCon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerCon.playerTrail[0] = this.transform.position;

        //HPをセット<後から変更>
        //ステージ跨ぎする場合の処理を，PartyManagerで実装
        Hp = firstHp;
        canAttack = true;

    }

    private void FixedUpdate()
    {
        Move();
    }

    protected override void Move()
    {
        // プレイヤーに追従する
        Vector2 current = transform.position;
        Vector2 next = playerCon.playerTrail[friendNum];
        Vector2 def = next - current;

        if (def.magnitude > warpTh)
        {
            this.transform.position = next;
        }
        else if (def.magnitude > moveTh)
        {
            rigidBody.velocity = def;
        }
        else
        {
            rigidBody.velocity = Vector2.zero;
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
