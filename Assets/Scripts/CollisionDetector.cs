using UnityEngine;

//�X�e�[�W��̃}�b�v�ړ��p�����蔻��
//�v�N���X���ύX
public class CollisionDetector : MonoBehaviour
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
