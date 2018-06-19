using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ジェンガをドラッグして動かす
/// ドラッグが始まったときにジェンガへアタッチ、終わったら削除
/// </summary>
public class DragAddForce : MonoBehaviour
{
    Rigidbody rb;
    DragMoving dragMoving;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        dragMoving = GetComponent<DragMoving>();
    }

    void FixedUpdate()
    {
        DragObject();
    }

    /// <summary>
    /// 物体をドラッグする
    /// </summary>
    void DragObject()
    {
        rb.AddForce(dragMoving._forceVec * dragMoving._dragPower);
    }
}
