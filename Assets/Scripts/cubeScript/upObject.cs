//オブジェクトを上から見る
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpObject : MonoBehaviour
{
    public void OnClick()
    {
        GameObject obj = GameObject.Find("move2");

        // 上から見えるようにオブジェクトを回転
        obj.transform.rotation = Quaternion.Euler(60f, 0f, 0f);
    }
}
