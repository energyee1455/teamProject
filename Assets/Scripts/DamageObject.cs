using UnityEngine;

//?????????????????????
public class DamageObject : MonoBehaviour
{
    public float speed = 0.3f;
    //??????
    public enum MobType
    {
        Friends = 0,
        Enemy = 1
    }
    public MobType mobType;
    //ダメージ数
    public int damageValue = 10;
    //????
    public float existenceTimeSeconds = 5.0f;

    private Rigidbody2D rb;

    private void Start()
    {
        Invoke("DeleteObject", existenceTimeSeconds);
        //rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //弾の動き
        this.gameObject.transform.position += this.gameObject.transform.up * speed;
        //rb.velocity = this.gameObject.transform.forward * speed;
    }

    //?????????
    private void DeleteObject()
    {
        Destroy(this.gameObject);
    }

    //弾の当たり判定

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (mobType == MobType.Friends)
            {
                collision.gameObject.GetComponent<MobState>().Attacked(damageValue);
                DeleteObject();
            }
        }

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Friend"))
        {
            if (mobType == MobType.Enemy)
            {
                collision.gameObject.GetComponent<MobState>().Attacked(damageValue);
                DeleteObject();
            }
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            DeleteObject();
        }
    }

    private void OnCollisionEnter2D()
    {
        DeleteObject();
    }


}