using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingToCenter : MonoBehaviour
{
    Rigidbody rb;
    JengaManager jengaManager;
    Transform anchorPosition;

    Vector3 targetPosition = new Vector3();
    [SerializeField]
    float power = 3;
    [SerializeField]
    bool isDebuging = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // 親オブジェクトからアンカーポジションのTransformを取得
        jengaManager = transform.root.gameObject.GetComponent<JengaManager>();
        anchorPosition = jengaManager._playPosition;

        SetTrargetPosition();
    }

    void FixedUpdate()
    {
        MoveToCenter();
    }

    void SetTrargetPosition()
    {
        targetPosition = anchorPosition.position;
        targetPosition.x += transform.localScale.x;
    }

    void MoveToCenter()
    {
        targetPosition.y = transform.position.y;
        var forceVec = targetPosition - transform.position;
        if(forceVec.magnitude >= 3.5)
            rb.AddRelativeForce(forceVec * power);

        if (isDebuging)
        {
            Debug.Log((forceVec * power).magnitude.ToString().Substring(0, 4));
            Debug.DrawLine(transform.position, targetPosition);
            
        }
    }
}
