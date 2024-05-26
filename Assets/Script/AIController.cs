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
        // NavMeshAgentコンポーネントを取得
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").GetComponent<Transform>();

        // NavMeshAgentが2Dモードで動作するように設定
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
        //targetまで移動
        if (target != null)
        {
            SetDestination(target.position);
        }

        /* タップした場所に移動
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SetDestination(worldPosition);
        }
        */
    }

    // 目標地点を設定するメソッド
    public void SetDestination(Vector2 destination)
    {
        if (agent != null && agent.isOnNavMesh)
        {
            agent.SetDestination(destination);
        }
    }
}
