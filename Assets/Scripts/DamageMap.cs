using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�_���[�W�}�b�v�̎���
//�_���[�W��^��������T���v��
public class DamageMap : MonoBehaviour
{
    //�^����_���[�W
    public int damageValue = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<MobState>().Damaged(damageValue);
        }
    }
}
