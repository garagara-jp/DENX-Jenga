using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFriction : MonoBehaviour
{
    [SerializeField]
    new Collider collider;    // 自分の

    [SerializeField]
    float changedDynamicFriction = 0.04f;
    [SerializeField]
    float changeStaticFriction = 0.2f;
    [SerializeField]
    float cStaticFriction = 0.3f;
    [SerializeField]
    float cDynamicFriction = 0.3f;

    List<GameObject> list = new List<GameObject>();

    float startDynamicFriction;
    float startStaticFriction;

    void Update()
    {
        CountNumber();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Jenga")
            list.Add(col.gameObject);
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Jenga")
            list.Remove(col.gameObject);
    }

    void OnMouseDown()
    {
        ReduceFriction();
    }

    void OnMouseUp()
    {
        IncreaseFriction();
    }

    void ReduceFriction()
    {
        // 変更前の摩擦力を保存
        startDynamicFriction = collider.material.dynamicFriction;
        startStaticFriction = collider.material.staticFriction;
        if (list.Count <= 4)
        {
            collider.material.dynamicFriction = changedDynamicFriction / list.Count * cDynamicFriction;
            collider.material.staticFriction = changeStaticFriction / list.Count * cStaticFriction;
        }
        else if(list.Count == 5 || list.Count == 6)
        {
            collider.material.dynamicFriction = changedDynamicFriction / (list.Count * 1.5f) * cDynamicFriction;
            collider.material.staticFriction = changeStaticFriction / (list.Count * 1.5f) * cStaticFriction;
        }
        else
        {
            collider.material.dynamicFriction = changedDynamicFriction / (list.Count * 2) * cDynamicFriction;
            collider.material.staticFriction = changeStaticFriction / (list.Count * 2) * cStaticFriction;
        }
    }

    void IncreaseFriction()
    {
        collider.material.dynamicFriction = startDynamicFriction;
        collider.material.staticFriction = startStaticFriction;
    }

    void CountNumber()
    {
        if (list.Count >= 7)
        {
            collider.material.frictionCombine = PhysicMaterialCombine.Minimum;
        }
        else
        {
            collider.material.frictionCombine = PhysicMaterialCombine.Average;
        }
    }

    void OnMouseDrag()
    {
        //Debug.Log(list.Count);
        //Debug.Log(collider.material.frictionCombine);
        //Debug.Log("dyn : " + collider.material.dynamicFriction);
        //Debug.Log("sta : " + collider.material.staticFriction);
    }
}
