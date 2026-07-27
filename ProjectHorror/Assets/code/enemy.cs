using UnityEngine;
using UnityEngine.AI; // AI機能を使用するために必要

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; // プレイヤーのTransform
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // 2Dで勝手に回転・傾いてしまうのを防ぐ重要な設定
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // プレイヤーの現在位置を目的地に設定
            agent.SetDestination(playerTransform.position);
        }
    }
}
