using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 入力に応じてマウスカーソルの画像を変更する
/// </summary>
public class MouseIconHandler : MonoBehaviour
{
    // マウスカーソル用のテクスチャ
    [SerializeField]
    Texture2D cam;
    [SerializeField]
    Texture2D rotation;
    [SerializeField]
    Texture2D scaling;
    [SerializeField]
    Texture2D move;

    [SerializeField]
    Vector2 hotSpot = Vector2.zero;

    CursorMode cursorMode = CursorMode.Auto;

    void Update()
    {
        ChangeMouseIcon();
    }

    void ChangeMouseIcon()
    {
        // 右クリック
        if (Input.GetMouseButton(1) && !Input.GetKey(KeyCode.LeftAlt))
            Cursor.SetCursor(cam, hotSpot, cursorMode);
        // 左Alt + 左クリック
        else if(Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(0))
            Cursor.SetCursor(rotation, hotSpot, cursorMode);
        // 左Alt + 右クリック
        else if(Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(1))
            Cursor.SetCursor(scaling, hotSpot, cursorMode);
        // 中クリック
        else if (Input.GetMouseButton(2))
            Cursor.SetCursor(move, hotSpot, cursorMode);
        else
            Cursor.SetCursor(null, hotSpot, cursorMode);
    }
}
