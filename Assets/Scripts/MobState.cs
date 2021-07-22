using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�L�����N�^�[��Ԃ̒��ۃN���X
//�v���C���[�C�����C�G�̂��ꂼ��Ŏ���
public abstract class MobState : MonoBehaviour
{
    //�L�����N�^�[���
    public enum State{
        Normal,
        Damaged,
        Die
    }

    //���ʃp�����[�^
    protected int Hp { set; get; }
    protected bool canAttack;
    protected float coolTime { set; get; }


    protected abstract void Attack();
    protected abstract void Move();

    //�_���[�W���󂯂�
    public abstract void Damaged(int damageValue);
}
