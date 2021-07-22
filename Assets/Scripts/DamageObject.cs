using UnityEngine;

//ダメージを与える機能
//攻撃判定コライダーを持つオブジェクトにアタッチ
public class DamageObject : MonoBehaviour
{
    //誰の攻撃か
    public enum MobType
    {
        Friends = 0,
        Enemy = 1
    }
    public MobType mobType;
    //与えるダメージ
    public int damageValue = 10;

    //当たり判定からタグ判定してダメージを与える（改良したい）
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(mobType == MobType.Friends)
            {
                collision.gameObject.GetComponent<MobState>().Damaged(damageValue);
            }  
        }
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Friend"))
        {
            if (mobType == MobType.Enemy)
            {
                collision.gameObject.GetComponent<MobState>().Damaged(damageValue);
            }
        }
    }

}
