using UnityEngine;

public class Fall : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider other)
    {
        // �L�����N�^�[�����Ƃ����ɐG�ꂽ�ꍇ
        if (other.CompareTag("Player"))
        {

            EventHandler.CallGetGameOverEvent();
        }
    }
}
