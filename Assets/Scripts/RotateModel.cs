//図形を回転させるプログラム

using UnityEngine;
using UnityEngine.EventSystems;

public class RotateModel : MonoBehaviour, IDragHandler
{
    [Header("回転設定")]
    [Tooltip("回転させる3Dオブジェクトを設定")]
    public Transform targetObject;

    [Tooltip("ドラッグ時の回転速度")]
    public float rotationSpeed = 2.0f;

    [Header("ドラッグ可能エリア")]
    [Tooltip("マウスドラッグが有効なUIエリア（RectTransform）")]
    public RectTransform dragArea;

    public void OnDrag(PointerEventData eventData)
    {
        if (targetObject == null || dragArea == null)
            return;

        // マウスがドラッグエリア内にあるかをチェック
        if (!RectTransformUtility.RectangleContainsScreenPoint(dragArea, eventData.position, eventData.pressEventCamera))
            return;

        // ドラッグ操作による回転
        targetObject.Rotate(Vector3.up, -eventData.delta.x * rotationSpeed, Space.World);
        targetObject.Rotate(Vector3.right, eventData.delta.y * rotationSpeed, Space.World);
    }
}
