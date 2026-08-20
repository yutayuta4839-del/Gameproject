using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using UnityEngine;

public class WayPointMover : MonoBehaviour
{
    public Transform waypointParent;
    public float movespeed;
    public float waitTime;
    public bool loopWayPoint = true;

    private Transform[] waypoints;
    private int currentwaypointindex;
    private bool iswaiting;

    private void Start()
    {
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
        MoveToWayPoint();


    }

    private void MoveToWayPoint()
    {
        Transform target = waypoints[currentwaypointindex];

        transform.position = Vector2.MoveTowards(transform.position, target.position, movespeed * Time.deltaTime);

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

            if (loopWayPoint)
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
}
