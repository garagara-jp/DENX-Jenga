using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ジェンガをドラッグして動かす
/// ジェンガにアタッチ
/// </summary>
public class DragMoving : MonoBehaviour
{
    JengaManager jengaManager;
    DragAddForce dragAddForce;

    Vector3 screenPoint;
    Vector3 startMousePosition;
    Vector3 forceVec;
    bool isenable;  // ドラッグを有効にするかどうか

    [SerializeField]
    float dragPower = 35;   // ドラッグする力

    public Vector3 _forceVec { get { return forceVec; } }
    public float _dragPower { get { return dragPower; } }

    void Start()
    {
        jengaManager = transform.root.gameObject.GetComponent<JengaManager>();
        isenable = false;
    }

    void OnMouseDown()
    {
        ClickJenga();
        gameObject.AddComponent<DragAddForce>();
        dragAddForce = GetComponent<DragAddForce>();
    }

    void OnMouseDrag()
    {
        DragJenga();
    }

    void OnMouseUp()
    {
        ReleaseJenga();
        Destroy(dragAddForce);
    }

    /// <summary>
    /// ジェンガをクリックした瞬間の挙動
    /// </summary>
    void ClickJenga()
    {
        // 左Altを押している間は別のモードを起動するためにスクリプトを無効化
        if (Input.GetKey(KeyCode.LeftAlt))
            return;

        // JengaManagerに自分のオブジェクト情報を渡す
        jengaManager._clickedJenga = transform.gameObject;

        //カメラから見たオブジェクトの現在位置を画面位置座標に変換
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);

        //取得したscreenPointの値を変数に格納
        float x = Input.mousePosition.x;
        float y = Input.mousePosition.y;

        // クリック時のオブジェクトの位置
        startMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(x, y, screenPoint.z));

        // マウス押下のフラグを建てる
        isenable = true;
    }

    /// <summary>
    /// ジェンガをドラッグしている最中の挙動
    /// </summary>
    void DragJenga()
    {
        // マウス押下のフラグを確認
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

        var localForceVec = transform.InverseTransformVector(forceVec);
        //forceVec = localForceVec;
        forceVec.y = 0;
    }

    /// <summary>
    /// ジェンガを手放したときの挙動
    /// </summary>
    void ReleaseJenga()
    {
        // 変数を初期化
        forceVec = Vector3.zero;
        isenable = false;
    }
}
