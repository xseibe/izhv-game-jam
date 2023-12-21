using System.Collections.Generic;
using UnityEngine;

public class DetectTargetHit : MonoBehaviour
{
    [SerializeField] List<Material> MaterialCycleList;

    [HideInInspector] 

    private int colorIndex = 0;
    private MeshRenderer maskMeshRendererLEye;
    private MeshRenderer maskMeshRendererREye;

    public void Start()
    {
        maskMeshRendererREye = transform.parent.transform.Find("Eyes/REye").GetComponent<MeshRenderer>();
        maskMeshRendererLEye = transform.parent.transform.Find("Eyes/LEye").GetComponent<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Snowball")
        {
            maskMeshRendererREye.material = MaterialCycleList[colorIndex % MaterialCycleList.Count];
            maskMeshRendererLEye.material = MaterialCycleList[colorIndex % MaterialCycleList.Count];

            colorIndex++;
        }
    }
}
