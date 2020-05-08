using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerMove move;
    //private CylinderCollider cylinderCollider;

    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<PlayerMove>();
        //cylinderCollider = GetComponent<CylinderCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            move.MoveForward();
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            move.MoveBack();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            move.MoveRight();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            move.MoveLeft();
        }

        //if(cylinderCollider.IsOnCollision())
        //{
        //    Debug.Log(cylinderCollider.GetNameHitObject());
        //}
    }
}
