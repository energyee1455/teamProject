using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
//敵キャラの制御クラス
public class SimpleEnemyController : MobState
{
    [SerializeField] private float moveSpeed = 5f; //移動速度
    private Rigidbody2D rb;

    public GameObject enemyHpviewObject;
    Slider hpview;
    public int firstHP = 100;

    string[] nothitDirection = { "up", "down", "right", "left" };

    public String direction;

    private void Start()
    {
        //HP表示
        hpview = enemyHpviewObject.GetComponent<Slider>();
        Hp = firstHP;
        hpview.maxValue = firstHP;

        //状態
        rb = GetComponent<Rigidbody2D>();
        direction = "Right";
        state = State.Normal;
        Move();
    }

    private void Update()
    {
        if(state == State.Die)
        {
            Destroy(this.gameObject);
        }
        else if (state == State.Normal || state == State.Damaged)
        {
            var list = new List<string>();
            list.AddRange(nothitDirection);

            Ray2D up = new Ray2D(this.transform.position, this.transform.up);
            Ray2D down = new Ray2D(this.transform.position, this.transform.up * -1);
            Ray2D right = new Ray2D(this.transform.position, this.transform.right);
            Ray2D left = new Ray2D(this.transform.position, this.transform.right * -1);

            RaycastHit2D upHit = Physics2D.Raycast(up.origin, up.direction, 1.5f, 1 << 7);
            RaycastHit2D downHit = Physics2D.Raycast(down.origin, down.direction, 1.5f, 1 << 7);
            RaycastHit2D rightHit = Physics2D.Raycast(right.origin, right.direction, 1.5f, 1 << 7);
            RaycastHit2D leftHit = Physics2D.Raycast(left.origin, left.direction, 1.5f, 1 << 7);

            if (upHit.collider)
            {
                list.Remove("up");
                if (direction == "up")
                {
                    Move();
                }
            }
            else
            {
                if (list.Contains("up") == false)
                {
                    list.Add("up");
                }
            }

            if (downHit.collider)
            {
                list.Remove("down");
                if (direction == "down")
                {
                    Move();
                }
            }
            else
            {
                if (list.Contains("down") == false)
                {
                    list.Add("down");
                }
            }

            if (rightHit.collider)
            {
                list.Remove("right");
                if (direction == "right")
                {
                    Move();
                }
            }
            else
            {
                if (list.Contains("right") == false)
                {
                    list.Add("right");
                }
            }

            if (leftHit.collider)
            {
                list.Remove("left");
                if (direction == "left")
                {
                    Move();
                }
            }
            else
            {
                if (list.Contains("left") == false)
                {
                    list.Add("left");
                }
            }
            nothitDirection = list.ToArray();
        }
    }

    public override void Attacked(int damageValue)
    {
        int damageCut = 0;//ステータスから取得
        int damage = damageValue - damageCut;
        if (damage > 0)
        {
            enemyHpviewObject.SetActive(true);
            state = State.Damaged;
            Hp -= damage;
            hpview.value = Hp;

            if (Hp <= 0)
            {
                state = State.Stop;
                Drop();
            }
        }
    }

    public GameObject dropItem1;
    public int percentage = 100;
    private void Drop()
    {
        if (UnityEngine.Random.Range(1f, 100f)<= percentage)
        {
            Debug.Log("Drop");
            Instantiate(dropItem1, transform.position, Quaternion.identity);
            state = State.Die;
        }
    }


    protected override void Attack(GameObject damageObfect)
    {
        throw new System.NotImplementedException();
    }

    //呼ばれたら向きを反転し，速度一定で移動
    protected override void Move()
    {   
        if(state == State.Die || state == State.Stop)
        {
            rb.velocity = new Vector2(0, 0);
        }
        else
        {
            direction = nothitDirection[UnityEngine.Random.Range(0, nothitDirection.Length)];
            switch (direction)
            {
                case "up":
                    rb.velocity = new Vector2(0, moveSpeed);
                    break;
                case "down":
                    rb.velocity = new Vector2(0, moveSpeed * -1);
                    break;
                case "right":
                    rb.velocity = new Vector2(moveSpeed, 0);
                    break;
                case "left":
                    rb.velocity = new Vector2(moveSpeed * -1, 0);
                    break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //壁にぶつかった
        if (collision.gameObject.CompareTag("Wall"))
        {
            Move();
        }
    }
}


