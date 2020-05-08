using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    List<CylinderCollider> colliders;

    private void Awake()
    {
        colliders = new List<CylinderCollider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0;i < colliders.Count;i++)
        {
            for(int j = i + 1;j < colliders.Count;j++)
            {
                // 当たり判定
                if(IsCollision(colliders[i].GetCylinder(),colliders[j].GetCylinder()))
                {
                    colliders[i].OnCollision(colliders[j].gameObject);
                    colliders[j].OnCollision(colliders[i].gameObject);
                }
            }
        }


    }

    /// <summary>
    /// コライダーの追加
    /// </summary>
    /// <param name="collider">コライダー</param>
    public void AddColliderList(CylinderCollider collider)
    {
        colliders.Add(collider);
    }

    /// <summary>
    /// 衝突判定
    /// </summary>
    /// <param name="a">オブジェクトA</param>
    /// <param name="b">オブジェクトB</param>
    /// <returns>衝突結果</returns>
    private bool IsCollision(Cylinder a,Cylinder b)
    {
        bool isCollision = false;
        Vector2 posA = new Vector2(a.position.x, a.position.z);
        Vector2 posB = new Vector2(b.position.x, b.position.z);

        float distance = Vector2.SqrMagnitude(posA - posB);
        float r = Mathf.Pow(a.radius + b.radius, 2.0f);

        if(distance < r) isCollision = true;

        return isCollision;
    }
}
