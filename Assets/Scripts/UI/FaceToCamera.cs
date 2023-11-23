using UnityEngine;

public class FaceToCamera : MonoBehaviour
{
    [SerializeField] private bool _xRotate = true;

    private Quaternion _rotation;

    private void Update()
    {
        transform.forward = Camera.main.transform.forward;

        if (_xRotate == false)
        {
            _rotation = transform.localRotation;
            transform.localRotation = Quaternion.Euler(0, _rotation.eulerAngles.y, _rotation.eulerAngles.z);
        }
    }
}
