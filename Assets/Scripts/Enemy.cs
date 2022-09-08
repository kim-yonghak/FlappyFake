using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyType : byte
{
    NORMAL = 0,
    BLUE,
    RED,
    INVALID
}
public class Enemy : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    private GameObject enemy = null;

    private const float SCORE_POSITION = 0f;
    private bool getScore = false;
     private EnemyType type = EnemyType.INVALID;
    public EnemyType Type
    {
        get
        {
            return type;
        }
        set
        {
            if (type == EnemyType.INVALID)
            {
                type = value;   // 한번만 쓸 수 있다.
            }
        }
    }

    private void OnEnable()
    {
        getScore = false;
    }
    
    void Awake()
    {
        enemy = new GameObject();
        enemy = transform.gameObject;
        //Destroy(this.gameObject, 7f);
    }

    // Update is called once per frame
    void Update()
    {
        enemy.transform.Translate(new Vector3(-1,0,0) * moveSpeed * Time.deltaTime);

        if (!getScore)
        {
            //if ((!getScore) && (transform.position.x < SCORE_POSITION))
            if (transform.position.x < SCORE_POSITION)
            {
                GameManager.Inst.Score += 1;
                getScore = true;
            }
            //Text scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
            //scoreText.text = $"Score : {GameManager.Inst.Score}";
        }
    }
}
