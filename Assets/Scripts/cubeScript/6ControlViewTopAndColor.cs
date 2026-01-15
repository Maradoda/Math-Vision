using UnityEngine;

public class ControlViewTopAndColor : MonoBehaviour
{
    [Header("対象の図形オブジェクト")]
    public GameObject targetObject;

    [Header("変更後の色")]
    public Color changeColor = Color.red;

        void Start()
    {
        // ★ シーン開始時に「ボタンを押した状態」にする
        OnClick();
    }

    public void OnClick()
    {
        if (targetObject == null) return;

        // ① 色を変える
        ChangeColor(targetObject, changeColor);

        // ② 上から見えるように回転
        ViewFromTop(targetObject);
    }

    // 色変更処理
    void ChangeColor(GameObject obj, Color color)
    {
        var renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = color;
        }
    }

    // 上から見る（X軸を90度回転）
    void ViewFromTop(GameObject obj)
    {
        obj.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }
}
