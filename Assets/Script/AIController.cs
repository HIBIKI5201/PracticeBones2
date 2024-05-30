using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static PlayerController;

public class AIController : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform target;

    [SerializeField] private GameObject DestroyParticle;
    PlayerController controller;

    void Start()
    {
        // NavMeshAgentコンポーネントを取得
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").GetComponent<Transform>();

        // NavMeshAgentが2Dモードで動作するように設定
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(DestroyParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            controller = collision.GetComponent<PlayerController>();
            if (controller._playerStatus == PlayerStatus.Normal)
            {
                Instantiate(DestroyParticle, transform.position, Quaternion.identity);
            }
        }
    }

    void Update()
    {
        if(!ReStartManager.gameOver)
        {
        //targetまで移動
            if (target != null)
            {
                SetDestination(target.position);
            }

            transform.rotation = Quaternion.Euler(0, 0, 0);
        }else
        {
            SetDestination(transform.position);
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
