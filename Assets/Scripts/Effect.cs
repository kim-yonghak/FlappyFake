/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Effect : MonoBehaviour
{
    public float speed;
    public new Rigidbody2D rigidbody;

    void Awake()
    {
        speed = 30f;
        rigidbody = GetComponent<Rigidbody2D>();
    }


    public void Left(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            rigidbody.AddForce(new Vector2(-1, 0) * speed);
        }
    }

    public void Right(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            rigidbody.AddForce(new Vector2(1, 0) * speed);
        }
    }

    public void Up(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            rigidbody.AddForce(new Vector2(0, 1) * speed);
        }
    }

    public void Down(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            rigidbody.AddForce(new Vector2(0, -1) * speed);
        }
    }
}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Effect : MonoBehaviour
{
    //private void Awake()
    //{
    //    Debug.Log("Awake");
    //}

    //private void OnEnable()
    //{
    //    Debug.Log("OnEnable");
    //}

    //private void Start()
    //{
    //    Debug.Log("Start");
    //}

    //public void TestPrint()
    //{
    //    Debug.Log("테스트 테스트");
    //}

    //int Plus(int a, int b)
    //{
    //    return a + b;
    //}

    //rigidbody의 velocity 사용
    //리지드바디의 운동 방향 변경
    //즉각적으로 움직임이 변경됨(관성X)
    //물리엔진 사용시 움직임 예측이 힘들다. 하지만 Velecity를 사용하면 어느정도 예측이 쉽다.
    //velocity로 움직이는 것은 물리엔진으로 움직이는 것이다.

    //transform의 Translate 사용
    //Translate은 게임 오브젝트에 있는 transform 컴포넌트의 position 값을 변경해주는 함수
    //기존 position에서 파라메터로 입력 받은 만큼 이동
    //순간이동이다.

    //void Test123()
    //{
    //    //this.transform.position;  //(1,2,0)
    //    this.transform.Translate(new Vector3(1, 0, 0)); //(1,2,0) + (1,0,0) = (2,2,0)
    //}

    Vector2 moveDir = new Vector2();
    Rigidbody2D rigid = null;
    public float moveSpeed = 50.0f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        moveDir = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        rigid.AddForce(moveDir * moveSpeed * Time.deltaTime);
        //rigid.MovePosition(moveDir);  //Translate과 비슷. 충둘체크를 진행함

        //this.transform.position += (Vector3)moveDir * moveSpeed * Time.deltaTime;

    }
}