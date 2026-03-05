using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class SpawnID : MonoBehaviour
{
    // Start is called before the first frame update

    int coolTimer = 0;
    [SerializeField] int coolTime = 1000;

    public int spawnID;//当たったら敵を場所によって、出現させるので、そのID
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coolTimer--;
    }

    void OnTriggerEnter(Collider other)//子オブジェクトが当たり判定と当たったら、これが呼び出される
    {
        if (coolTimer > 0) return;
        if(other.CompareTag("Player"))
        {
        // 親のEnemyFactoryを取得して通知
        EnemyFactory factory = GetComponentInParent<EnemyFactory>();
        if (factory != null)
        {
            //生成のためのidを渡す
            factory.OnChildernTrigger(spawnID);
                //クールタイムをセットする
                coolTimer = coolTime;
        }
        }
    }
}
