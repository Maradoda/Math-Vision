//点Pを動かすプログラム（固定座標制）

using UnityEngine;

public class StepMovePointP : MonoBehaviour
{
    public Vector3 localStep = new Vector3(1, 0, 1); // ローカル空間での移動ステップ
    public float interval = 1f;
    public int maxSteps = 7;

    private float timer = 0f;
    private int stepCount = 0;
    private Vector3 startLocalPosition = new Vector3(3, 2, 3); // 初期位置

    void Start()
    {
        this.enabled = false; // 自分自身を最初に無効化
        transform.localPosition = startLocalPosition;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            timer = 0f;

            // ローカル空間に沿って移動
            transform.Translate(localStep, Space.Self);

            stepCount++;
            if (stepCount >= maxSteps)
            {
                // 初期位置に戻す
                stepCount = 0;
                transform.localPosition = startLocalPosition;
            }

            Debug.Log("点Pの位置: " + transform.position.ToString("F2"));
        }
    }
}
