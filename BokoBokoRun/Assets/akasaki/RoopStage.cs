using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoopStage : MonoBehaviour
{
    float speed = 10.0f;
    float stageWidth = 25f;
    Vector3 transPos = new Vector3(20,0,-578);
    Vector3 prevPos; 

    // Start is called before the first frame update
    void Start()
    {
        // èâä˙ç¿ïW
        prevPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // ç∂Ç…à⁄ìÆÇ∑ÇÈ
        if (this.CompareTag("AkasakiWall"))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        
        float distance = transform.position.z - prevPos.z;
            


        if(distance<0)
        {
            distance = -distance;
        }

        
        // ç∂Ç…à⁄ìÆÇ∑ÇÈ
        if (this.CompareTag("AkasakiWall"))
        {
            if (this.transform.position.z <= transPos.z)
            {
                this.transform.position = prevPos;
            }
        }
        else
        {
            if (this.transform.position.x <= transPos.x)
            {
                this.transform.position = prevPos;
            }
        }
    }
}
