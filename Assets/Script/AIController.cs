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
        target = GameObject.Find("Player").GetComponent<Transform>();

        // NavMeshAgent��2D���[�h�œ��삷��悤�ɐݒ�
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        //target�܂ňړ�
        if (target != null)
        {
            SetDestination(target.position);
        }

        /* �^�b�v�����ꏊ�Ɉړ�
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SetDestination(worldPosition);
        }
        */
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
