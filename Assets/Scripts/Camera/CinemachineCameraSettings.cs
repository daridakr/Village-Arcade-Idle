using Cinemachine;
using UnityEngine;

[RequireComponent (typeof(CinemachineVirtualCamera))]
public class CinemachineCameraSettings : MonoBehaviour
{
    [SerializeField] private CameraConfig _config;
    [SerializeField] private Camera _uiCamera;

    private CinemachineVirtualCamera _camera;

    private void Awake() => _camera = GetComponent<CinemachineVirtualCamera>();

    private void Start()
    {
        if (_config == null)
            return;

        ApplySettings();
    }

    private void ApplySettings()
    {
        _camera.m_Lens.FieldOfView = _config.FieldOfView;
        _camera.m_Lens.NearClipPlane = _config.NearClipPlane;
        _camera.m_Lens.FarClipPlane = _config.FarClipPlane;

        _uiCamera.fieldOfView = _config.FieldOfView;

        //CinemachineTrackedDolly body = _camera.AddCinemachineComponent<CinemachineTrackedDolly>();
        //body.
    }
}
