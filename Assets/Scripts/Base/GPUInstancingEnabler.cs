using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class GPUInstancingEnabler : MonoBehaviour
{
    private void Awake()
    {
        MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
        Renderer renderer = GetComponent<Renderer>();
        renderer.SetPropertyBlock(materialPropertyBlock);
    }
}
