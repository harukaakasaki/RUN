using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTransRoop : MonoBehaviour
{
    [SerializeField] private Transform targetTransStart;//始点
    [SerializeField] private Transform targetTransEnd;//終点

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position += new Vector3(0.1f, 0, 0);

        if(this.transform.position.x > targetTransEnd.position.x)
        {
            this.transform.position = new Vector3(targetTransStart.position.x, this.transform.position.y, this.transform.position.z);
        }

    }
}
