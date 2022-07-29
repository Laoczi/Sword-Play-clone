using UnityEngine;

namespace DynamicMeshCutter
{
    public class PlaneBehaviour : CutterBehaviour
    {
        public MeshTarget target;
        public float DebugPlaneLength = 2;
        public void Cut(Transform planePosition)
        {
            Cut(target, planePosition.position, planePosition.up, null, OnCreated);
        }

        void OnCreated(Info info, MeshCreationData cData)
        {
            MeshCreation.TranslateCreatedObjects(info, cData.CreatedObjects, cData.CreatedTargets, Separation);
        }

    }
}