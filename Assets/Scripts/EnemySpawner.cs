/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int maxRate = 5;
    public int minRate = 2;
    public float[] ranPosition;
    public int[] ranNum;

    public int rate;
    private float timeAfterSpawn;
    // Start is called before the first frame update
    void Start()
    {
        ranPosition = new float[5];
        ranNum = new int[3];
        rate = Random.Range(minRate, maxRate);
        timeAfterSpawn = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 3; i++)
        {
                ranNum[i] = Random.Range(0,3);
                ranPosition[2*i] = ranNum[i] - 0.75f;
                if(i != 2)
                {
                ranPosition[(2*i)+1] = ranNum[i] - 1.25f;
                }
        }
        timeAfterSpawn += Time.deltaTime;
        if(timeAfterSpawn > rate)
        {
            timeAfterSpawn = 0f;
            rate = Random.Range(minRate, maxRate);

            GameObject enemy = Instantiate(enemyPrefab, new Vector3(5, ranPosition[0], 0), Quaternion.identity);
            GameObject enemy1 = Instantiate(enemyPrefab, new Vector3(5, ranPosition[1], 0), Quaternion.identity);
            GameObject enemy2 = Instantiate(enemyPrefab, new Vector3(5, ranPosition[2], 0), Quaternion.identity);
            GameObject enemy3 = Instantiate(enemyPrefab, new Vector3(5, ranPosition[3], 0), Quaternion.identity);
            GameObject enemy4 = Instantiate(enemyPrefab, new Vector3(5, ranPosition[4], 0), Quaternion.identity);
        }
        
    }
}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs = null;    // 생성할 적 새의 프리팹을 저장할 배열
    public float spawnStartDelay = 2.0f;        // 시작 딜레이 시간
    public float spawnInterval = 1.0f;          // 적들을 생성할 시간 간격

    private const int MAX_SPACE_COUNT = 6;    
    private const float SPACE_HEIGHT = 0.4f;
    private const float LIFETIME = 7.0f;

    // 최초의 Update 실행 직전에 한번만 호출
    private void Start()
    {        
        //Spawn함수를 코루틴으로 실행
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        // 실행했을 때 spawnStartDelay만큼 우선 대기
        yield return new WaitForSeconds(spawnStartDelay);
        
        // 그 후 반복해서 생성 시작
        while (true)    // 무한 루프
        {
            // bool[] flags = GetFlagsBoolType();
            int flags = GetFlags();     // flags 각 비트를 확인해서 1일 때만 새를 생성하면 된다.
                                        // 0000 0000 0000 0000 0000 0000 0010 1011 (가정)
            int singleFlag = 1;         // 시작값 0000 0000 0000 0000 0000 0000 0000 0001 

            // flags에 설정된 값에 따라 새 생성
            for (int i = 0; i < MAX_SPACE_COUNT; i++)
            {
                //if (flags[i] == true) // GetFlagsBoolType용 조건문

                // flags와 singleFlag를 &해서 0이 아니면 singleFlag에 설정된 비트 위치에 1이 되어있다는 것
                if ((flags & singleFlag) != 0)
                {                    
                    EnemyGenerate(i);
                }
                singleFlag <<= 1;   // singleFlag의 비트를 한번 검사할 때마다 왼쪽으로 한칸씩 옮김
            }
            /*int flags = GetFlags();

            // flags에 설정된 값에 따라 새 생성
            for (int i = 0; i < MAX_SPACE_COUNT; i++)
            {
                
                int check = (int) Mathf.Pow(2,i);
                //if (flags[i] == true) // GetFlagsBoolType용 조건문
                if((flags & check) != 0)
                {
                    EnemyGenerate(i);
                }
            }*/

            yield return new WaitForSeconds(spawnInterval);
        }
    }
    private int GetFlags()
    {
        int flags = 0;

        while (flags == 0)
        {
            int random = (int)(Random.value * 64.0f);  //화이트보드 1번
            //   0101 0101 0101 0101 0101 0101 0101 0101    (랜덤으로 나온값으로 가정)
            // & 0000 0000 0000 0000 0000 0000 0011 1111    (랜덤으로 나온값을 6bit남기는 방법)
            // 0000 0001 (MAX_SPACE_COUNT만큼 왼쪽으로 쉬프트)=> 0100 0000  (-1)=> 0011 1111
            random &= ((1 << MAX_SPACE_COUNT) - 1); //random = random & ((1 << MAX_SPACE_COUNT) - 1);

            int mask = 0b_0011;
            mask = mask << Random.Range(0, MAX_SPACE_COUNT - 1);
            //11 0000
            //01 1000
            //00 1100
            //00 0110
            //00 0011
            mask = ~mask;   //0000 1100 --> 1111 1111 1111 1111 1111 1111 1111 0011  

            flags = random & mask;
        } 

        return flags;
    }

    private bool[] GetFlagsBoolType()
    {
        bool result = false;    // 최소 하나 이상의 새를 생성할 수 있는지 확인하는 플래그
        bool[] flags = new bool[MAX_SPACE_COUNT];   // 새가 생성될 칸이 true로 표시된 배열
        while (result == false) // 만약 하나 이상의 새를 생성할 수 없는 경우 다시 계산
        {
            // flags에 랜덤으로 값 입력. true면 해당칸의 새를 생성하고 false면 생성하지 않는다.
            for (int i = 0; i < MAX_SPACE_COUNT; i++)
            {
                if (Random.Range(0, 2) == 1)
                    flags[i] = true;
            }

            // 무조건 비워질 칸을 결정
            // flags에 설정된 값과 상관 없이 비워질 공간을 지정해 덮어 쓴다.
            int index = Random.Range(0, MAX_SPACE_COUNT - 1);
            flags[index] = false;       //랜덤으로 한칸을 무조건 비운다.
            flags[index + 1] = false;   //그 다음 칸도 비워 2개 연속된 빈 공간이 생기게 만든다.

            // 새가 하나 이상 생성되는지 확인
            for (int i = 0; i < MAX_SPACE_COUNT; i++)
            {
                // 한줄에 새가 전부 안나오는 버그 수정을 위해 flags모두 검사
                if (flags[i] == true)
                {
                    // 하나라도 true가 나왔다는 것은 빈칸이 아닌 곳이 최소 1개는 있다는 의미                        
                    result = true;      // 51번 라인의 while을 벗어나기 위해 변수 변경
                    break;              // 67번 라인의 for를 벗어나기 위해 break;
                }
            }
        }

        return flags;
    }

    // 적을 생생하는 함수
    private void EnemyGenerate(int index)
    {
        // 어떤 종류의 적을 생성할지 결정
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        //GameObject enemy = GameObject.Instantiate(enemyPrefabs[enemyIndex], this.transform);
        GameObject enemy = EnemyPool.Inst.GetEnemy();
        enemy.transform.parent = this.transform;
        enemy.transform.position = this.transform.position;

        //int spaceIndex = Random.Range(0, MAX_SPACE_COUNT);
        //enemy.transform.Translate(Vector2.down * Random.Range(0.0f,2.0f));
        enemy.transform.Translate(Vector2.down * index * SPACE_HEIGHT);
        //Destroy(enemy, LIFETIME);
    }

    public void spawnerStop()
    {
        Destroy(this.gameObject);
    }


}

    // 적을 생생하는 것
    /*private void EnemyGenerate()
    {
        // 어떤 종류의 적을 생성할지 결정
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        for(int i = 0; i < 3; i++){
            ranNum[i] = Random.Range(0,3);
            if(i==2 && ranNum[0]!=ranNum[1] && ranNum[1]!=ranNum[2] && ranNum[0]!=ranNum[2])
            {
                ranNum[2] = ranNum[1];
            }
            ranPosition[2*i] = ranNum[i] - 0.75f;
            ranPosition[(2*i)+1] = ranNum[i] - 1.25f;
            GameObject enemy1 = Instantiate(enemyPrefabs[enemyIndex], new Vector3(5, ranPosition[2*i], 0), Quaternion.identity);
            GameObject enemy2 = Instantiate(enemyPrefabs[enemyIndex], new Vector3(5, ranPosition[(2*i)+1], 0), Quaternion.identity);
        }
        //enemy.transform.Translate(Vector2.down * Random.Range(0.0f,2.0f));
    }*/