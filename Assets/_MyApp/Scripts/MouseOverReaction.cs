using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class MouseOverReaction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    Image image { get { return GetComponent<Image>(); } }
    [SerializeField]
    AudioSource sound;

    // オブジェクトの範囲内にマウスポインタが入った際に呼び出されます。
    // this method called by mouse-pointer enter the object.
    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = Color.cyan;
        sound.Play();
    }

    // オブジェクトの範囲内からマウスポインタが出た際に呼び出されます。
    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Color.white;
    }
}