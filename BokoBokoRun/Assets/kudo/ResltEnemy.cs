using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResultEnemyState
{
    Enter,
    Active
}


public class ResltEnemy : Character
{
    //Enemyの状態
    public ResultEnemyState m_State = ResultEnemyState.Active;
    int m_count = 0;
    bool isRotate = false;
    Vector3 m_Respawn = new Vector3(0.0f, 0.0f, 0.0f);
    int m_Timer = 0;
    //敵を戻したかどうか
    bool m_isRespawn = false;

   
  
    



    // Start is called before the first frame update
    void Start()//初期化
    {

        m_count = 0;
        isRotate = false;

        m_Velocity = new Vector3(0.0f, 0.0f, 0.05f);

        //最初のポジションを取得する
        m_Respawn = transform.position;
        m_Timer = 0;
        //m_isRespawn = false;

    }

    // Update is called once per frame
    protected override void Update()//毎フレーム更新
    {

      
       
    }

    private void FixedUpdate()
    {
        //タイマの更新
        m_Timer++;
        Tick();
        this.transform.position += m_Velocity;
        Debug.Log("動いている");

        
        //指定したフレーム後に最初に取得したポジションに戻る
        if (m_Timer >= 210f)
        {
            Debug.Log("gfege");
            transform.position = m_Respawn;
            m_Timer = 0;
        }
    }


    protected override void Tick()//enemyの動き
    {
        if (isRotate)
        {
            Quaternion targetRotation = Quaternion.Euler(0, 90, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }


        
          
    }




}
