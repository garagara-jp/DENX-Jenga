using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラにアタッチ
/// </summary>
public class CameraOffsetCalculation : MonoBehaviour
{
    [SerializeField]
    SceneViewCamera sceneViewCamera;
    [SerializeField]
    CreatJengaTower creatJengaTower;

    [SerializeField]
    Transform anchorPosition;
    [SerializeField]
    Transform jenga;

    Vector3 targetPositon;
    float offset;

    void Start()
    {
        targetPositon = anchorPosition.position;
        targetPositon.x += jenga.localScale.x;
    }

    void Update()
    {
        CalcOffset();
        sceneViewCamera._offset = offset;
        sceneViewCamera._wheelSpeed = offset;
    }

    void CalcOffset()
    {
        targetPositon.y = anchorPosition.position.y + jenga.localScale.y * creatJengaTower._layerCount / 2;
        offset = (targetPositon - transform.position).magnitude;
    }
}
