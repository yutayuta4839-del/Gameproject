using Cysharp.Threading.Tasks;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private NavMeshAgent agent;
    private bool isChasing = false;

    [SerializeField] private float stopChaseDistance = 7f;
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private Transform sightObject;

    public Sprite NormalSprite;
    public Sprite backPlayerSprite;
    public Sprite leftPlayerSprite;
    public Sprite rightPlayerSprite;
    private SpriteRenderer enemyrenderer;

    float moveThreshold = 0.1f;    // 左右に動いているとみなす最低限の速度
    float maxVerticalDrift = 0.5f; // 左右移動中に許容する上下のブレの最大値

    [Header("waypointinfo")]
    public Transform waypointParent;
    public float waitTime;
    private bool isloopWayPoint = true;
    private int currentwaypointindex;
    private bool iswaiting;
    GameObject playerobj;

    public bool islooping { get; private set; } = true;
    Rigidbody2D rb;
    private Transform[] waypoints;

    Vector2 playerpos;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        enemyrenderer = GetComponent<SpriteRenderer>();
        playerobj = GameObject.FindGameObjectWithTag("Player");
        waypoints = new Transform[waypointParent.childCount];//waypointParentの子供の数の配列の空きができた。

        for (int i = 0; i < waypointParent.childCount; i++)
        {
            waypoints[i] = waypointParent.GetChild(i);
        }

       

        // 2Dで勝手に回転・傾いてしまうのを防ぐ重要な設定
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = moveSpeed;
    }

    void Update()
    {
        if (playerobj != null)
        {
            playerpos = playerobj.transform.position;
        }

        if (isChasing)
        {
            // プレイヤーの現在位置を目的地に設定
            agent.SetDestination(playerTransform.position);
            ChangeAnimation();

            float distance = Vector2.Distance(transform.position, playerpos);
            Debug.Log($"敵の位置: {transform.position} / プレイヤーの位置: {playerpos} / 距離: {distance}");

            if (distance > stopChaseDistance)
            {
                Debug.Log("見失いました。追跡終了");
                Debug.Log(distance);
                Debug.Log(stopChaseDistance);
                StopChasing();
                Setloop(true);
            }
        }

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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerTransform == null) return;

        if (collision.CompareTag("Player"))
        {

            isChasing = true;
            Setloop(false);
           
        }


    }

    private void StopChasing()
    {
        isChasing = false;
        agent.ResetPath(); // 目的地のクリア
       

        // （お好みで）ここで元の巡回ルートに戻る処理などを入れられます
    }

    private void ChangeAnimation()
    {
        Vector2 velocity = agent.velocity;
        if (velocity.x > moveThreshold && Mathf.Abs(velocity.y) < maxVerticalDrift)
        {
            enemyrenderer.sprite = rightPlayerSprite;
            RotateSight(velocity);
        }
        // --- 左移動の判定 ---
        // Xがマイナス方向に大きく、かつ上下のブレ（絶対値）が許容範囲内
        else if (velocity.x < -moveThreshold && Mathf.Abs(velocity.y) < maxVerticalDrift)
        {
            enemyrenderer.sprite = leftPlayerSprite;
            RotateSight(velocity);
        }
        // --- 上移動の判定 ---
        else if (velocity.y > moveThreshold)
        {
            enemyrenderer.sprite = backPlayerSprite;
            RotateSight(velocity);
        }
        // --- 下移動の判定 ---
        else if (velocity.y < -moveThreshold)
        {
            enemyrenderer.sprite = NormalSprite;
            RotateSight(velocity);
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

    private void RotateSight(Vector2 velocity)
    {
        if (sightObject == null) return;

        // 現在の移動方向から角度を計算する (Atan2はラジアンを返すので、Degreeに変換)
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        angle += 90f;
        // 角度を適用 (Z軸を中心に回転)
        sightObject.localEulerAngles = new Vector3(0, 0, angle);
    }

}
