using UnityEngine;

namespace DynamicMeshCutter
{
    public class PlaneBehaviour : CutterBehaviour
    {
        public MeshTarget target;
        public float _pushForce;
        public float DebugPlaneLength = 2;
        public void Cut(Transform planePosition)
        {
            Cut(target, planePosition.position, planePosition.up, null, OnCreated);
        }

        void OnCreated(Info info, MeshCreationData cData)
        {
            MeshCreation.TranslateCreatedObjects(info, cData.CreatedObjects, cData.CreatedTargets, Separation);

            GameObject[] slices = cData.CreatedObjects;

            for (int i = 0; i < slices.Length; i++)
            {
                slices[i].GetComponent<Rigidbody>().AddForce(Vector3.forward * _pushForce, ForceMode.Impulse);
            }
        }

    }
}