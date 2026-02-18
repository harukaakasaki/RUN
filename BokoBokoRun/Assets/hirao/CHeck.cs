using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHeck : MonoBehaviour
{
    // Start is called before the first frame update
    private float lastZ;

    void Start()
    {
        lastZ = transform.position.z;
    }

    void Update()
    {
        if (Mathf.Abs(transform.position.z - lastZ) > 0.001f)
        {
           // Debug.Log($"Z軸が変更されました: {lastZ} → {transform.position.z}");
            //Debug.Log($"変更したスクリプト: {UnityEngine.StackTraceUtility.ExtractStackTrace()}");
        }
        lastZ = transform.position.z;
    }
}
