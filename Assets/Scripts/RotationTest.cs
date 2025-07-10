//マウスカーソルで図形を動かすプログラム

using UnityEngine;

public class RotationTest : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float _speed = 5f;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float rotX = Input.GetAxis("Mouse Y") * _speed * -1; //反転しているため×-1
            float rotY = -Input.GetAxis("Mouse X") * _speed;
            Vector3 rotation = new Vector3(rotX, rotY, 0);
            _target.Rotate(rotation, Space.World);
        }
    }
}
