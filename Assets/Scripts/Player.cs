using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float jumpSpeed;
    public float rotateSpeed;
    public new Rigidbody2D rigidbody;

    private bool isDead = false;
    public bool IsDead
    {
        get
        {
            return isDead;
        }
    }

    void Awake()
    {
        jumpSpeed = 30f;
        rotateSpeed = 0f;
        rigidbody = GetComponent<Rigidbody2D>();
        GameManager.Inst.MyPlayer = this;
    }

    void Update()
    {
    }


    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            rigidbody.AddForce(Vector2.up * jumpSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(isDead == false)
        {
            GameObject.Find("Scroller").GetComponent<Scroller>().scrollStop();
            GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().spawnerStop();
        }
        OnDead();
        rigidbody.AddForce(Vector2.left, ForceMode2D.Impulse);
        rigidbody.AddTorque(5.0f);
        jumpSpeed = 0f;
        Destroy(this.gameObject, 3f);

        /*if (other.tag == "Enemy" || other.tag == "Wall" && isDead == false)
        {
        }*/
    }

    private void OnDead()
    {
        isDead = true;     
        GameManager.Inst.OnGameFinish();
    }

}

/*using System.Collections;           //네임스페이스 설정(C# 컨테이너용)
using System.Collections.Generic;   //네임스페이스 설정(C# 컨테이너용, 제네릭)
using UnityEngine;                  //네임스페이스 설정(Unity 용)
using UnityEngine.InputSystem;      //Unity 새 인풋 시스템을 쓰기 위한 네임스페이스

/// 접근제한자 : public(공용, 공공의), protected(보호된), private(개인적인)
/// public : 누구나 쓸 수 있다.
/// private : 가지고 있는 자만 쓸 수 있다. C# 디폴트
/// protected : 나와 나를 상속받은 자만 쓸 수 있다.
/// 키워드 : C#에서 사용을 예약해 놓은 단어들(파란색으로 보이는 부분들)
/// class : 일종의 틀. 어떤 기능을 하고 어떤 데이터를 가질 수 있는지 설정해 놓은 일종의 설계도
/// 객체 : 클래스를 인스턴스 한 것.클래스를 실체화 한 것
/// 변수 : 데이터를 저장하는 곳. 데이터 타입

//class MyTest
//{
    
//}

//Player라는 이름의 클래스를 만드는데 public이라 프로그램 내의 모두가 접근할 수 있고
//MonoBehaviour라는 클래스를 상속받았다.
public class Player : MonoBehaviour     
{
    // jumpPower라는 이름의 float 타입의 변수를 만드는데. public이고 저장되는 초기값은 10.0이다.
    public float jumpPower = 10.0f; 
    // rigid라는 이름의 Rigidbody2D 타입의 변수를 만드는데, private이고 저장되는 초기값은 null이다.
    private Rigidbody2D rigid = null;
    
    // 게임 오브젝트가 만들어진(Instance) 후 실행되는 함수
    private void Awake() 
    {
        //cashing을 통해 무거운 작업을 최소한으로 하기 위해 Awake에서 찾음
         rigid = GetComponent<Rigidbody2D>();   //Rigidbody2D 컴포넌트를 찾아서 rigid에 저장해라.
    }

    // 매 프레임 마다 호출되는 함수
    //private void Update()
    //{
    //    //// Input Manager를 통해 입력처리를 하는 방법
    //    //if( Input.GetButton("Jump") ) 
    //    //{            
    //    //    rigid.AddForce(Vector2.up * jumpPower);
    //    //}
    //    //if(Input.GetKeyDown(KeyCode.Space))
    //    //{

    //    //}
    //}

    // 스페이스 버튼을 눌었을 때 실행될 함수
    // 기능은 리지드바디를 통해 위쪽으로 힘을 더한다.
    public void JumpUp(InputAction.CallbackContext context)
    {
        //context.started;      //키를 눌렀을 때
        //context.performed;    //키를 길게 눌렀었을 때(차징류)
        //context.canceled;     //키를 땠을 때
        if (context.started)    //키를 눌렀을 때만 아래의 코드를 실행하라
        {
            //rigidbody에 힘을 가해라. 위쪽 방향으로
            rigid.AddForce(Vector2.up * jumpPower);

            Debug.Log("Jump!");
        }
    }
}
*/

