using UnityEngine;

//�X�e�[�W�ړ��̂��߂̃R���C�_�[
public class StageChangeCollider: MonoBehaviour
{
    public StageManager.StageName nextStage;
    public StageManager.StairDirection nextDirection;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StageManager.instance.PrepareToMoveStage(nextStage, nextDirection);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StageManager.instance.Cancel();
        }
    }
}
