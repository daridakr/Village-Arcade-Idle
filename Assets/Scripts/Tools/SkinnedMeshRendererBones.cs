using UnityEngine;

public class SkinnedMeshRendererBones : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _prefab;
    [SerializeField] private SkinnedMeshRenderer _mesh;
    [SerializeField] private Transform _rootBone;

    private void Start()
    {
        SkinnedMeshRenderer skinnedMeshRenderer = Instantiate(_prefab, transform);
        skinnedMeshRenderer.bones = _mesh.bones;
        skinnedMeshRenderer.rootBone = _rootBone;
    }
}
