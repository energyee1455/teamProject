using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendController : MobState
{
    private Rigidbody2D rigidBody;

    public int friendNum; //パーティー内での順番

    //HP変数と表示用
    private int firstHp = 220;   //初期HP
    public GameObject HpviewObject;
    Slider hpview;

    //移動用のプレイヤーコントローラ
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

        //HP表示
        hpview = HpviewObject.GetComponent<Slider>();
        Hp = firstHp;
        hpview.maxValue = firstHp;
        hpview.value = firstHp;

        //ステージ跨ぎする場合の処理を，PartyManagerで実装
        canAttack = true;
        //攻撃処理のループ
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

}
