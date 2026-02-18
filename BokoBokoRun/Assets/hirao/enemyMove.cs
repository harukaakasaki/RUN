using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
  Enter,
  Active
}


public class enemyMove : C
{
    //Enemyの状態
    public EnemyState m_State = EnemyState.Enter;
    int m_count = 0;
    bool isRotate = false;
    //関数
    private void Move()//移動中
    {
        
      m_Velocity = new Vector3(0.01f, 0.0f, 0.0f);
    }
    private void Entering()//出場中
    {
        m_count++;
        m_Velocity = new Vector3(0.0f, 0.0f, 0.01f);
        if(m_count > 1000)
        {
            m_State = EnemyState.Active;
            isRotate = true;
          
        }
    }



    // Start is called before the first frame update
    void Start()//初期化
    {
    
        m_count = 0;
        isRotate = false;
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
        if(isRotate)
        {
            Quaternion targetRotation = Quaternion.Euler(0, 90, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }


        switch(m_State)
        {
            case EnemyState.Enter:
                Entering();
                break;
            case EnemyState.Active:
                Move();
                break;
            default:
                break;
        }
    }

    


}
