using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMove : MonoBehaviour
{
    // UIを拡大縮小させる


    Vector3 startScale;

    public float speed = 2f;// 動くスピード
    public float size = 0.2f;// 大きくなる量

    // Start is called before the first frame update
    void Start()
    {
        startScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        float scale = Mathf.Sin(Time.time * speed) * size;

        transform.localScale = startScale+new Vector3(scale, scale, scale);
    }
}
