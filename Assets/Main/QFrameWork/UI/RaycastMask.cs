using UnityEngine.UI;

namespace QFrameWork
{
    class RaycastMask: MaskableGraphic
    {
        protected override void Awake()
        {
            useLegacyMeshGeneration = false;
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
        }
    }
}
