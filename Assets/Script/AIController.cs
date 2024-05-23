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

        // NavMeshAgentが2Dモードで動作するように設定
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        // 目標地点を設定
        if (target != null)
        {
            SetDestination(target.position);
        }
    }

    void Update()
    {
        // タップした場所に移動
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SetDestination(worldPosition);
        }
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
