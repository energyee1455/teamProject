using UnityEngine;

//?????????????????????
public class DamageObject : MonoBehaviour
{
    //??????
    public enum MobType
    {
        Friends = 0,
        Enemy = 1
    }
    public MobType mobType;
    //???????
    public int damageValue = 10;
    //????
    public float existenceTimeSeconds = 5.0f;

    private void Start()
    {
        Invoke("DeleteObject", existenceTimeSeconds);
    }
    //?????????
    private void DeleteObject()
    {
        Destroy(this);
    }

    //???????????????????????
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(mobType == MobType.Friends)
            {
                collision.gameObject.GetComponent<MobState>().Attacked(damageValue);
            }  
        }
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Friend"))
        {
            if (mobType == MobType.Enemy)
            {
                collision.gameObject.GetComponent<MobState>().Attacked(damageValue);
            }
        }
        DeleteObject();
    }
}
