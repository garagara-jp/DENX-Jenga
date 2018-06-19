using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 引っこ抜いたジェンガを消す
/// ジェンガにアタッチ
/// </summary>
public class DestroyJenga : MonoBehaviour
{
    JengaManager jengaManager;

    void Start()
    {
        jengaManager = transform.root.gameObject.GetComponent<JengaManager>();
    }

    void OnCollisionEnter(Collision col)
    {
        jengaManager.Destroy(col, gameObject);
    }
}
