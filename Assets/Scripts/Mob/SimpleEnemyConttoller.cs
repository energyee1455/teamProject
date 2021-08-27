using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
//敵キャラの制御クラス
public class SimpleEnemyConttoller : MobState
{
    [SerializeField] private float moveSpeed = 5f; //移動速度
    private Rigidbody2D rb;

    public GameObject enemyHpviewObject;
    Slider hpview;
    public int firstHP = 100;

    enum MoveDirection
    {
        Right = 1,
        Left = -1
    }

    MoveDirection direction;

    private void Start()
    {
        //HP表示
        hpview = enemyHpviewObject.GetComponent<Slider>();
        Hp = firstHP;
        hpview.maxValue = firstHP;

        rb = GetComponent<Rigidbody2D>();
        direction = MoveDirection.Right;
        Move();
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
                state = State.Die;
            }
        }
    }

    protected override void Attack(GameObject damageObfect)
    {
        throw new System.NotImplementedException();
    }

    //呼ばれたら向きを反転し，速度一定で移動
    protected override void Move()
    {
        if (direction == MoveDirection.Right) direction = MoveDirection.Left;
        else direction = MoveDirection.Right;
        rb.velocity = new Vector2(moveSpeed * (int)direction, 0);
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
