using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Cylinder
{
    public Vector3 position;
    public float radius;
}

public enum CollisionState
{
    Hit,     // 当たった
    Hitting, // 当たっている
    GetAway, // 離れた
    Away,    // 離れている
}

public class CylinderCollider : MonoBehaviour
{
    private readonly int circleVertex = 32;

    [SerializeField] private float height = 0.0f;
    [SerializeField] private float radius = 0.0f;
    [SerializeField] private Vector3 offset = Vector3.zero;

    private ColliderManager manager;
    private GameObject hitObject;
    private bool isOnCollision;
    private CollisionState state;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("ColliderManager").GetComponent<ColliderManager>();
        manager.AddColliderList(this);
        isOnCollision = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// サークルコライダーのワイヤーフレームを表示
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        // 右の線
        Vector3 startPos = GetGizumoStartPointX(radius);
        Vector3 endPos = GetGizumoEndPoint(startPos);
        Gizmos.DrawLine(startPos, endPos);
        // 左の線
        startPos = GetGizumoStartPointX(-radius);
        endPos = GetGizumoEndPoint(startPos);
        Gizmos.DrawLine(startPos, endPos);
        // 奥の線
        startPos = GetGizumoStartPointZ(radius);
        endPos = GetGizumoEndPoint(startPos);
        Gizmos.DrawLine(startPos, endPos);
        // 手前の線
        startPos = GetGizumoStartPointZ(-radius);
        endPos = GetGizumoEndPoint(startPos);
        Gizmos.DrawLine(startPos, endPos);

        // 上円の記述
        Vector3 previousPosition = Vector3.zero;
        Vector3 nextPosition = GetGizumoCenter(height) + GetGizumoRotation() * GetGizmoCircleAngle(0);
        for (int i = 0; i <= circleVertex; i++)
        {
            previousPosition = nextPosition;
            nextPosition = GetGizumoCenter(height) + GetGizumoRotation() * GetGizmoCircleAngle(i);
            Gizmos.DrawLine(previousPosition, nextPosition);
        }

        // 中央円の記述
        previousPosition = Vector3.zero;
        nextPosition = GetGizumoCenter(0) + GetGizumoRotation() * GetGizmoCircleAngle(0);
        for (int i = 0; i <= circleVertex; i++)
        {
            previousPosition = nextPosition;
            nextPosition = GetGizumoCenter(0) + GetGizumoRotation() * GetGizmoCircleAngle(i);
            Gizmos.DrawLine(previousPosition, nextPosition);
        }

        // 下円の記述
        previousPosition = Vector3.zero;
        nextPosition = GetGizumoCenter(-height) + GetGizumoRotation() * GetGizmoCircleAngle(0);
        for (int i = 0; i <= circleVertex; i++)
        {
            previousPosition = nextPosition;
            nextPosition = GetGizumoCenter(-height) + GetGizumoRotation() * GetGizmoCircleAngle(i);
            Gizmos.DrawLine(previousPosition, nextPosition);
        }
    }

    /// <summary>
    /// 左右の線の開始位置を取得
    /// </summary>
    /// <param name="r">半径</param>
    /// <returns>線の開始位置</returns>
    private Vector3 GetGizumoStartPointX(float r)
    {
        Vector3 startPoint = transform.position + offset;
        startPoint.x += r;
        startPoint.y -= height;
        return startPoint;
    }

    /// <summary>
    /// 前後の線の開始位置を取得
    /// </summary>
    /// <param name="r">半径</param>
    /// <returns>線の開始位置</returns>
    private Vector3 GetGizumoStartPointZ(float r)
    {
        Vector3 startPoint = transform.position + offset;
        startPoint.z += r;
        startPoint.y -= height;
        return startPoint;
    }

    /// <summary>
    /// 線の終了位置を取得
    /// </summary>
    /// <param name="start">線の開始位置</param>
    /// <returns>線の終了位置</returns>
    private Vector3 GetGizumoEndPoint(Vector3 start)
    {
        Vector3 endPoint = start;
        endPoint.y += height * 2;
        return endPoint;
    }

    /// <summary>
    /// 円の中心点を取得
    /// </summary>
    /// <param name="h">高さ</param>
    /// <returns>円の中心点</returns>
    private Vector3 GetGizumoCenter(float h)
    {
        Vector3 center = transform.position;
        center.y += h;
        return center;
    }

    /// <summary>
    /// 円の回転角を取得
    /// </summary>
    /// <returns>円の回転角</returns>
    private Quaternion GetGizumoRotation()
    {
        Quaternion rotation = transform.rotation;
        rotation = Quaternion.AngleAxis(90.0f, Vector3.right);
        return rotation;
    }

    /// <summary>
    /// 円周の座標を取得
    /// </summary>
    /// <param name="i">座標点</param>
    /// <returns>円周の座標</returns>
    private Vector3 GetGizmoCircleAngle(int i)
    {
        Vector3 circleAngle = Vector3.zero;
        // 線を引く1ステップの角度
        float step = 2f * Mathf.PI / circleVertex;
        // 線を引く開始角度(偶数なら半ステップずらす)
        float offset = Mathf.PI * 0.5f + ((circleVertex % 2 == 0) ? step * 0.5f : 0f);

        float theta = step * i + offset;

        float x = radius * Mathf.Cos(theta);
        float y = radius * Mathf.Sin(theta);

        circleAngle.x = x;
        circleAngle.y = y;

        return circleAngle;
    }

    /// <summary>
    /// シリンダーの取得
    /// </summary>
    /// <returns>シリンダー</returns>
    public Cylinder GetCylinder()
    {
        Cylinder cylinder;
        cylinder.position = transform.position;
        cylinder.radius = radius;
        return cylinder;
    }

    public void OnCollision(GameObject obj)
    {
        if(hitObject == null)
        {
            isOnCollision = true;
        }
        else
        {
            isOnCollision = false;
        }
        hitObject = obj;
    }

    public string GetNameHitObject()
    {
        if (hitObject == null) return null;
        return hitObject.name;
    }

    public bool IsOnCollision()
    {
        return isOnCollision;
    }
}
