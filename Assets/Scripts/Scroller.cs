/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    
    }

    public float scrollSpeed = 3.0f;
    private GameObject[] background = null;
    private GameObject frontBG = null;
    private GameObject rearBG = null;
    private const float END_POINT = -2.0f;
    private const float BACKROUND_GAP = 1.42f;

    private void Awake()
    {
        background = new GameObject[transform.childCount];

        for(int i = 0; i < transform.childCount; i++)
        {
            background[i] = transform.GetChild(i).gameObject;
        }
        frontBG = background[0];
        rearBG = background[transform.childCount - 1];
    }

    void Update()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            background[i].transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);
            if(background[i].transform.position.x < END_POINT)
            {
                background[i].transform.position = new Vector3(
                    rearBG.transform.position.x + BACKROUND_GAP,
                    frontBG.transform.position.y,
                    0.0f);
                rearBG = background[i];
                if(transform.childCount - 1 != i)
                {
                    frontBG = background[i+1];   
                }
                else
                {
                    frontBG = background[0];
                }
            }
        }
    }
}*/


//네임스페이스 설정
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Scroller클래스 선언(만드는 클래스, 함수, 변수가 어떤 구조를 가지는지 말하는 것)
public class Scroller : MonoBehaviour
{
    //각종 변수들 선언

    // 배경이 이동하는 속도. public으로 선언해서 유니티 에디터에서도 편집 가능해짐
    public float scrollSpeed = 15.0f;   

    // 배경 한줄을 여러개 담고 있는 배열(배열은 여러 데이터를 담을 수 있는 통이다)
    private GameObject[] backgrounds = null;

    // 제일 앞에 있는 배경 한줄
    private GameObject frontBG = null;

    // 제일 마지막에 있는 배경 한줄
    private GameObject rearBG = null;

    // backgrounds에서 현재 front를 가리키는 인덱스
    private int frontIndex = 0;

    // 배경 한줄이 완전히 밖으로 나간 것을 확인하기 위한 기준점.
    // const로 선언해서 절대 값이 변하지 않는다.(상수가 됨)
    private const float END_POINT = -2.0f;

    // 배경 한줄의 폭. const로 선언해서 절대 값이 변하지 않는다.(상수가 됨)
    private const float BACKGROUND_GAP = 1.43f;

    // 게임 오브젝트가 만들어 진 직후에 호출되는 함수
    private void Awake()
    {
        // backgrounds 배열에 메모리를 할당.
        // transform.childCount에 기록된 갯수만큼 GameObject를 담을 수 있는 크기로 메모리 할당.
        backgrounds = new GameObject[transform.childCount];

        // for문을 이용해 transform.childCount만큼 반복
        for (int i = 0; i< transform.childCount; i++)
        {
            // 이 스크립트의 자식들의 참조(주소)를 하나씩 backgrounds 배열에 저장.
            backgrounds[i] = transform.GetChild(i).gameObject;
        }
        // 첫번째 배경 한줄을 frontBG에 저장
        frontBG = backgrounds[frontIndex];
        // 마지막 배경 한줄을 rearBG에 저장
        rearBG = backgrounds[transform.childCount - 1];
    }

    // 매 프레임마다 호출(Call)된다.
    private void Update()
    {
        // for문을 이용해 반복 수행. 
        // transform.childCount는 반복 횟수
        // i는 0부터 (transform.childCount - 1)까지 변경됨
        for (int i = 0; i < transform.childCount; i++)
        {
            //Translate함수를 사용해서 backgrounds에 들어 있는 모든 배경을 같이 움직임
            //움직이는 방향은 왼쪽이고 1초에 scrollSpeed만큼 움직인다.
            backgrounds[i].transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);
        }

        // 조건문을 통해 제일 앞에 있는 배경한줄의 x값이 END_POINT보다 작다
        // => 앞으로 절대 화면에 나올일이 없다.
        if (frontBG.transform.position.x < END_POINT)
        {
            // 제일 앞에 있던 배경한줄을 제일 마지막에 있는 배경한줄 뒤에 붙인다.
            frontBG.transform.position = new Vector3(
                //x값을 제일 마지막에 있는 배경한줄의 x값에 배경한줄의 폭만큼 더해서 설정한다.
                rearBG.transform.position.x + BACKGROUND_GAP,   
                frontBG.transform.position.y,
                0.0f);
            //여기까지 했을 때 발생하는 문제는?
            //앞뒤가 갱신이 되지 않던 문제

            //새로운 제일 뒤쪽과 앞쪽을 설정
            rearBG = frontBG;   //이전 제일앞에 있던 것이 제일 뒤쪽으로 이동했으니 frontBG를 rearBG에 할당

            //frontBG는 frontIndex값을 갱신해서 설정
            //배열 크기가 6일 때 접근 가능한 범위는 0~5
            //이 범위를 벗어나게 되면 IndexOutOfRangeException 발생
            //예외를 방지하기 위해 %연산으로 통해 수의 범위를 조절한다.
            frontIndex++;
            frontIndex = frontIndex % transform.childCount;
            frontBG = backgrounds[frontIndex];
        }
    }

    public void scrollStop()
    {
        scrollSpeed = 0f;
    }
}
