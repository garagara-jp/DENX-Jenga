using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ジェンガをドラッグして動かす
/// ジェンガにアタッチ
/// </summary>
public class PowerCalculation : MonoBehaviour
{
    DragAddForce dragAddForce;

    Vector3 screenPoint;
    Vector3 startMousePosition;
    Vector3 forceVec;

    bool isenable;

    public Vector3 _forceVec { get { return forceVec; } }
    
    void Start()
    {
        forceVec = Vector3.zero;
        isenable = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            CalcStartMousePosition();

        if(Input.GetMouseButton(0))
            CalcPower();

        if (Input.GetMouseButtonUp(0))
            PowerOff();
    }

    void CalcStartMousePosition()
    {
        // 左Altを押している間は別のモードを起動するためにスクリプトを無効化
        if (Input.GetKey(KeyCode.LeftAlt))
            return;

        //カメラから見たオブジェクトの現在位置を画面位置座標に変換
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);

        //取得したscreenPointの値を変数に格納
        float x = Input.mousePosition.x;
        float y = Input.mousePosition.y;

        // クリック時のオブジェクトの位置
        startMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(x, y, screenPoint.z));

        isenable = true;
    }

    void PowerOff()
    {
        forceVec = Vector3.zero;
        isenable = false;
    }

    void CalcPower()
    {
        if (!isenable)
            return;

        //ドラッグ時のマウス位置を変数に格納
        float x = Input.mousePosition.x;
        float y = Input.mousePosition.y;

        //ドラッグ時のマウス位置をシーン上の3D空間の座標に変換する
        var currentScreenPoint = new Vector3(x, y, screenPoint.z);

        // ドラッグ時のオブジェクトの位置
        var currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint);

        // ドラッグした距離をベクトルとして算出
        forceVec = currentPosition - startMousePosition;
        forceVec.y = 0;
    }
}
