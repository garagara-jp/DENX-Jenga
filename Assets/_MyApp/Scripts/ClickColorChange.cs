using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickColorChange : MonoBehaviour
{
    Color startColor;
    new Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void OnMouseDown()
    {
        // 変化する前の色を取得
        startColor = renderer.material.color;
        
        // 左Altを押している間は別のモードを起動するためにスクリプトを無効化
        if (Input.GetKey(KeyCode.LeftAlt))
            return;

        // 色の変化
        renderer.material.color = Color.cyan;
    }

    void OnMouseUp()
    {
        // 色を元に戻す
        renderer.material.color = startColor;
    }
}
