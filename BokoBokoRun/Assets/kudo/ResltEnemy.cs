using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResultEnemyState
{
    Enter,
    Active
}


public class ResltEnemy : C
{
    //Enemyの状態
    public ResultEnemyState m_State = ResultEnemyState.Enter;
    int m_count = 0;
    bool isRotate = false;
    Vector3 m_Respawn = new Vector3(0.0f, 0.0f, 0.0f);
    int m_Timer = 0;
    //敵を戻したかどうか
    bool m_isRespawn = false;
    //関数
    private void Move()//移動中
    {

        m_Velocity = new Vector3(0.01f, 0.0f, 0.0f);
    }
    private void Entering()//出場中
    {
        m_count++;
        //角度によって、進む向きを変える
        Quaternion rot = transform.rotation;//現在の角度を入手
        if (rot == Quaternion.identity)//どっちか2分の1
        {
            m_Velocity = new Vector3(0.0f, 0.0f, 0.005f);
        }
        else
        {
            m_Velocity = new Vector3(0.0f, 0.0f, -0.005f);
        }
        if (m_count > 1000)
        {
            m_State = ResultEnemyState.Active;
            

        }
    }



    // Start is called before the first frame update
    void Start()//初期化
    {

        m_count = 0;
        isRotate = false;

        //最初のポジションを取得する
        m_Respawn = transform.position;
        m_Timer = 0;
        //m_isRespawn = false;

    }

    // Update is called once per frame
    protected override void Update()//毎フレーム更新
    {

        Tick();
        this.transform.position += m_Velocity;

        //最初のポジションを取得する
        //m_CharacterPos = transform.position;

        //指定したフレーム後に最初に取得したポジションに戻る
        if(m_Timer >= 140f)
        {
            Debug.Log("gfege");
            transform.position = m_Respawn;
            m_Timer = 0;
        }
       
    }

    private void FixedUpdate()
    {
        //タイマの更新
        m_Timer++;
    }


    protected override void Tick()//enemyの動き
    {
        if (isRotate)
        {
            Quaternion targetRotation = Quaternion.Euler(0, 90, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }


        switch (m_State)
        {
            case ResultEnemyState.Enter:
                Entering();
                break;
            case ResultEnemyState.Active:
                Move();
                break;
            default:
                break;
        }
    }




}
