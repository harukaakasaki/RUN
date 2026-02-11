using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Move,
    Attack,
}


public class enemyMove : C
{
    //Enemyの状態
    EnemyState m_State;
    //関数
    private void Move()
    {
        m_Velocity = new Vector3(0.0f,0.0f,0.01f);
    }



    // Start is called before the first frame update
    void Start()//初期化
    {
        m_State = EnemyState.Move;
    }

    // Update is called once per frame
    protected override void Update()//毎フレーム更新
    {
        Tick();
        ApplyGravity();

        this.transform.position += m_Velocity;

    }
    protected override void Tick()//enemyの動き
    {
        switch(m_State)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                break;
            default:
                break;
        }
    }

    


}
