using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoopStage : MonoBehaviour
{
    float speed = 10.0f;
    float stageWidth = 25f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ¶‚ÉˆÚ“®‚·‚é
        if (this.CompareTag("AkasakiWall"))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
            
        // ˆê’èˆêˆÊ’u‚ğ’´‚¦‚é‚ÆA‰E‚ÉˆÚ“®‚·‚é
        if (transform.position.x <= -stageWidth)
        {
            transform.position += new Vector3(stageWidth* 3, 0,0);
        }
    }
}
