using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform target;

    void Start()
    {
        // NavMeshAgent�R���|�[�l���g���擾
        agent = GetComponent<NavMeshAgent>();

        // NavMeshAgent��2D���[�h�œ��삷��悤�ɐݒ�
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        // �ڕW�n�_��ݒ�
        if (target != null)
        {
            SetDestination(target.position);
        }
    }

    void Update()
    {
        // �^�b�v�����ꏊ�Ɉړ�
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SetDestination(worldPosition);
        }
    }

    // �ڕW�n�_��ݒ肷�郁�\�b�h
    public void SetDestination(Vector2 destination)
    {
        if (agent != null && agent.isOnNavMesh)
        {
            agent.SetDestination(destination);
        }
    }
}
