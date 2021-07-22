using UnityEngine;

//�X�e�[�W��̃}�b�v�ړ��p�����蔻��

public class StageChangeCollider: MonoBehaviour
{
    public StageManager.StageName nextStage;
    public StageManager.StairDirection nextDirection;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StageManager.instance.MoveStage(nextStage, nextDirection);
        }
    }
}
