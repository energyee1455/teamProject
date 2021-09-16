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

    //攻撃のオブジェクト：外から変更可能にする
    public GameObject damageObject;
    public int coolTime = 5; //攻撃のクールタイム：武器によって変更

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        // 衝突時にobjectを回転させない設定(スクリプトに書いてなくてもいい)
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;

        //プレイヤークラスを取得
        playerCon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        //playerCon.playerTrail[0] = this.transform.position;

        //HPをセット<後から変更>
        //ステージ跨ぎする場合の処理を，PartyManagerで実装
        Hp = firstHp;
        canAttack = true;

        StartCoroutine(AttackCoroutine());
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

    IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(coolTime);
    }

    protected override void Attack(GameObject damageObfect)
    {
        if (canAttack)
        {
            GameObject copied = Object.Instantiate(damageObfect) as GameObject;
            copied.transform.position = this.gameObject.transform.position;

            //狙った方向に向ける
            Vector3 pd = target.position - this.transform.position;
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
