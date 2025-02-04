using UnityEngine;

public class AnimatedCollider : MonoBehaviour
{
    public MeshCollider meshCollider;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    private void FixedUpdate() {
        Mesh bakedMesh = new Mesh();
        skinnedMeshRenderer.BakeMesh(bakedMesh);
        meshCollider.sharedMesh = bakedMesh;
    }
}
