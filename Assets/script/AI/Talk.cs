using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    private NavMeshAgent agent;
    public float moveRadius = 10f; // NPC �̃����_���ړ��͈�
    public float detectionRange = 5f; // ����NPC�����m����͈�
    public float conversationProbability = 0.5f; // ��b������m�� (50%)
    public float conversationDuration = 3f; // ��b�̎�������

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        MoveToRandomPosition();
    }

    void MoveToRandomPosition()
    {
        // �����_���Ȉʒu��ݒ�
        Vector3 randomDirection = Random.insideUnitSphere * moveRadius;
        randomDirection += transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, moveRadius, -1);
        agent.SetDestination(navHit.position);
    }

    void Update()
    {
        // NPC �������_���Ɉړ�
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            MoveToRandomPosition();
        }

        // Raycast �őO����NPC�����邩�����m
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, detectionRange))
        {
            // NPC �ɂԂ��������b�������������
            if (hit.collider.CompareTag("NPC"))
            {
                // �m���Ɋ�Â��ĉ�b�������I��
                if (Random.value < conversationProbability)
                {
                    // ��b���J�n
                    StartCoroutine(ConversationCoroutine());
                }
                else
                {
                    // NPC �������
                    AvoidNPC(hit.collider.transform);
                }
            }
        }
    }

    // ��b�R���[�`��
    IEnumerator ConversationCoroutine()
    {
        // ��b���͈ړ����~
        agent.isStopped = true;

        
        
        yield return new WaitForSeconds(conversationDuration);

       
        // �Ăу����_���Ɉړ�
        agent.isStopped = false;
        MoveToRandomPosition();
    }

    // NPC ���������֐�
    void AvoidNPC(Transform otherNPC)
    {
        // ����̂��߂ɉ��Ɉړ�����
        Vector3 avoidanceDirection = (transform.position - otherNPC.position).normalized;
        Vector3 newDestination = transform.position + avoidanceDirection * 2f; // 2���j�b�g���
        agent.SetDestination(newDestination);

        //Debug.Log("NPC ������܂����B");
    }
}
