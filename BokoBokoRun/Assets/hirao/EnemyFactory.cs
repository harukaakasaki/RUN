using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyFactory : MonoBehaviour
{
    // Inspectorでアサインする敵のPrefab
    public GameObject enemyPrefab;

    // 指定位置に敵を生成するメソッド
    public GameObject SpawnEnemy(Vector3 position)
    {
        if (enemyPrefab != null)
        {
            return Instantiate(enemyPrefab, position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Enemy Prefabがアサインされていません");
            return null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.enterKey.wasPressedThisFrame)
        {
            SpawnEnemy(this.transform.position);
        }

    }
}
