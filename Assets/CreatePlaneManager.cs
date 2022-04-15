using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlaneManager : MonoBehaviour
{
    [SerializeField] private GameObject parentObject;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(UpdatePlanes());
    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator UpdatePlanes()
    {
        while (true)
        {
            foreach (Transform childTransform in parentObject.transform)
            {
                if (childTransform.gameObject.name == "Platform")
                {
                    GameObject grandchild = childTransform.transform.Find("PlatformQuad").gameObject;
                    Mesh mesh = grandchild.transform.GetComponent<MeshFilter>().mesh;
                    Bounds bounds = mesh.bounds;
                    Vector3 originalSize = bounds.size;
                    Vector3 originalPosition = grandchild.transform.localPosition;
                    Vector3 originalRotation = grandchild.transform.localEulerAngles;
                    grandchild.SetActive(false);

                    if (childTransform.Find("PlatformPlane") ==  null)
                    {
                        var obj = new GameObject("PlatformPlane");
                        //obj.transform.localPosition = originalPosition;
                        //obj.transform.localRotation = Quaternion.Euler(originalRotation.x, originalRotation.y, originalRotation.z);
                        SquareMesh squareMesh = obj.AddComponent<SquareMesh>();
                        obj.transform.parent = childTransform.transform;
                        Vector3[] vertices = new Vector3[] {
                        new Vector3 (- originalSize.x / 2, - originalSize.y / 2, 0),
                        new Vector3 (- originalSize.x / 2,  originalSize.y / 2, 0),
                        new Vector3 (originalSize.x / 2 , - originalSize.y / 2, 0),
                        new Vector3 (originalSize.x / 2,  originalSize.y / 2, 0),
                        };
                        squareMesh.MakePlane(vertices);
                    }

                }
                //Debug.Log(childTransform.gameObject.name);
            }
            //3•b‚¨‚«‚ÉƒXƒLƒƒƒ“
            yield return new WaitForSeconds(3f);
        }
    }
}
