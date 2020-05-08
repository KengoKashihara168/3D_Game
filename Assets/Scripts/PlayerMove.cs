using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed = 0.0f;
    [SerializeField] private float turnSpeed = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 回転
    /// </summary>
    /// <param name="angle"></param>
    private void Turnaround(float speed)
    {
        Vector3 rotate = Vector3.zero;
        rotate.y = speed;
        transform.Rotate(rotate);
    }

    /// <summary>
    /// Z方向へ移動
    /// </summary>
    public void MoveForward()
    {
        float angle = transform.eulerAngles.y;
        if(angle > 1.0f && angle <180.0f)
        {
            // -Y方向へ回転
            Turnaround(-turnSpeed);
        }
        if(angle >= 180.0f && angle < 359.0f)
        {
            // Y方向へ回転
            Turnaround(turnSpeed);
        }
        if(angle >= 359.0f || angle <= 1.0f)
        {
            // 前進
            transform.Translate(Vector3.forward * speed);
        }

        if(angle < 0)
        {
            transform.eulerAngles = Vector3.zero;
        }
    }

    /// <summary>
    /// X方向へ移動
    /// </summary>
    public void MoveRight()
    {
        float angle = transform.eulerAngles.y;
        if (angle > 91.0f && angle <= 270.0f)
        {
            // -Y方向へ回転
            Turnaround(-turnSpeed);
        }
        if (angle >= 0.0f && angle < 89.0f || angle > 270.0f && angle <= 360.0f)
        {
            // Y方向へ回転
            Turnaround(turnSpeed);
        }
        if (angle >= 89.0f && angle <= 91.0f)
        {
            // 前進
            transform.Translate(Vector3.forward * speed);
        }
    }

    public void MoveBack()
    {
        float angle = transform.eulerAngles.y;
        if (angle >= 181.0f && angle <= 360.0f)
        {
            // -Y方向へ回転angle >= 0.0f && angle <= 179.0f
            Turnaround(-turnSpeed);
        }
        if (angle >= 0.0f && angle <= 179.0f)
        {
            // Y方向へ回転
            Turnaround(turnSpeed);
        }
        if (angle > 179.0f && angle < 181.0f)
        {
            // 前進
            transform.Translate(Vector3.forward * speed);
        }
    }

    public void MoveLeft()
    {
        float angle = transform.eulerAngles.y;
        if (angle >= 0.0f && angle < 90.0f || angle >= 271.0f && angle <= 360.0f)
        {
            // -Y方向へ回転
            Turnaround(-turnSpeed);
        }
        if (angle >= 90.0f && angle <= 269.0f)
        {
            // Y方向へ回転
            Turnaround(turnSpeed);
        }
        if (angle > 269.0f && angle < 271.0f)
        {
            // 前進
            transform.Translate(Vector3.forward * speed);
        }
    }
}
