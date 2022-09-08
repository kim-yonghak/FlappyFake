using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    /// 3월 2일 과제
    /// 플레이어는 다른것과 부딪치면 죽어서 바닥에 구르며 떨어진다. 그리고 화면밖으로 나간다.
    /// 조건1. 테스트플레이어는 바닥에 닿으면 죽는다.
    /// 조건2. 테스트플레이어는 적 새와 부딪치면 죽는다.    
    /// 조건3. 테스트플레이어는 죽으면 반대쪽 방향으로 구른다.(z축+방향으로 회전)
    /// 조건4. 테스트플레이어는 죽으면 조종이 안된다.
    /// 조건5. 테스트플레이어가 바닥에 닿으면 잠시 뒤 배경 스크롤이 멈춘다.
    public new Rigidbody2D rigidbody;
    private GameObject player = null;
    public float speed;
    void Awake()
    {
        player = new GameObject();
        player = transform.gameObject;
        rigidbody = GetComponent<Rigidbody2D>();
        speed = 0f;
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        speed = 200f;
        
        if (other.tag == "Enemy")
        {
            Player player = other.GetComponent<Player>();
        }

        /*if (other.tag == "Wall")
        {
            gameObject.SetActive(false);
        }*/
    }
}
