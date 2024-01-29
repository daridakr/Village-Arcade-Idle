using UnityEngine;

[CreateAssetMenu(fileName = "NewCameraConfig", menuName = "System/Camera Config")]
public sealed class CameraConfig : ScriptableObject
{
    [SerializeField] private float _fieldOfView;
    [SerializeField] private float _nearClipPlane;
    [SerializeField] private float _farClipPlane;

    public float FieldOfView { get => _fieldOfView; }
    public float NearClipPlane { get => _nearClipPlane; }
    public float FarClipPlane { get => _farClipPlane; }
}
