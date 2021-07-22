using UnityEngine;

//�_���[�W��^����@�\
//�U������R���C�_�[�����I�u�W�F�N�g�ɃA�^�b�`
public class DamageObject : MonoBehaviour
{
    //�N�̍U����
    public enum MobType
    {
        Friends = 0,
        Enemy = 1
    }
    public MobType mobType;
    //�^����_���[�W
    public int damageValue = 10;

    //�����蔻�肩��^�O���肵�ă_���[�W��^����i���ǂ������j
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
