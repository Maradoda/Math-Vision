using UnityEngine;

public class VertexLabel : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public Transform pointC;

    void OnGUI()
    {
        DrawLabel(pointA, "A");
        DrawLabel(pointB, "B");
        DrawLabel(pointC, "C");
    }

    void DrawLabel(Transform point, string label)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(point.position);
        if (screenPos.z > 0)  // カメラの前にあるときのみ表示
        {
            GUI.Label(new Rect(screenPos.x, Screen.height - screenPos.y, 50, 20), label);
        }
    }
}
