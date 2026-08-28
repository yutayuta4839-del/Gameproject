using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using UnityEngine;
using UnityEngine.AI;

public class WayPointMover : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform waypointParent;
    public float movespeed;
    public float waitTime;
    private bool isloopWayPoint = true;
    public bool islooping { get; private set; } = true;
    Rigidbody2D rb;
    float moveThreshold = 0.1f;    // 左右に動いているとみなす最低限の速度
    float maxVerticalDrift = 0.5f; // 左右移動中に許容する上下のブレの最大値
    private SpriteRenderer enemyrenderer;
    public Sprite NormalSprite;
    public Sprite backPlayerSprite;
    public Sprite leftPlayerSprite;
    public Sprite rightPlayerSprite;

    private Transform[] waypoints;
    private int currentwaypointindex;
    private bool iswaiting;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody2D>();
        enemyrenderer = GetComponent<SpriteRenderer>();
        waypoints = new Transform[waypointParent.childCount];//waypointParentの子供の数の配列の空きができた。

        for (int i = 0; i < waypointParent.childCount; i++)
        {
            waypoints[i] = waypointParent.GetChild(i);
        }
    }
    private void Update()
    {
        if (PauseController.IsGamePosed || iswaiting)
        {
            return;
        }
        if (islooping)
        {
            MoveToWayPoint();
            ChangeAnimation();
        }

    }

    private void MoveToWayPoint()
    {
        Transform target = waypoints[currentwaypointindex];

        agent.SetDestination(target.position);


        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            waitonwaypoint(destroyCancellationToken).Forget();
        }
    }

    private async UniTaskVoid waitonwaypoint(CancellationToken cancellationToken)
    {
        iswaiting = true;
        try
        {
            // UniTaskのDelayで待機。cancellationTokenを渡すことで、オブジェクト破棄時などに安全にキャンセルされる
            await UniTask.Delay(TimeSpan.FromSeconds(waitTime), cancellationToken: cancellationToken);
            //非同期wait();

            if (isloopWayPoint)
            {
                currentwaypointindex = (currentwaypointindex + 1) % waypoints.Length;
            }
            else
            {
                // ループしない場合：インデックスが最大値を超えないように制限する
                currentwaypointindex = Mathf.Min(currentwaypointindex + 1, waypoints.Length - 1);
            }
        }
        catch (OperationCanceledException)
        {

        }
        finally
        {
            iswaiting = false;
        }

    }

    public void Setloop(bool isloop)
    {
        islooping = isloop;
    }

    private void ChangeAnimation()
    {
        Vector2 velocity = agent.velocity;
        if (velocity.x > moveThreshold && Mathf.Abs(velocity.y) < maxVerticalDrift)
        {
            enemyrenderer.sprite = rightPlayerSprite;
        }
        // --- 左移動の判定 ---
        // Xがマイナス方向に大きく、かつ上下のブレ（絶対値）が許容範囲内
        else if (velocity.x < -moveThreshold && Mathf.Abs(velocity.y) < maxVerticalDrift)
        {
            enemyrenderer.sprite = leftPlayerSprite;
        }
        // --- 上移動の判定 ---
        else if (velocity.y > moveThreshold)
        {
            enemyrenderer.sprite = backPlayerSprite;
        }
        // --- 下移動の判定 ---
        else if (velocity.y < -moveThreshold)
        {
            enemyrenderer.sprite = NormalSprite;
        }
    }
}
