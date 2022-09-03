using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

//Ezyslice is a library which helps slicing the objects easy using predefine methods and functions
//https://github.com/DavidArayan/ezy-slice this is has been used for this project
public class ObjectSlicer : MonoBehaviour
{
    public float slicedObjectInitialVelocity = 100;
    public Material slicedMaterial;
    public Transform startSlicingPoint;
    public Transform endSlicingPoint;
    public LayerMask sliceableLayer; //used to call our new layer making sure we do not affect anything else in the scene
    public VelocityEstimator velocityEstimator; // used to determine how long how the fruits will be thrown

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit; // hit is used to detect collision between the blade and the object
        Vector3 slicingDirection = endSlicingPoint.position - startSlicingPoint.position;
        bool hasHit = Physics.Raycast(startSlicingPoint.position, slicingDirection, out hit, slicingDirection.magnitude, sliceableLayer);

        if(hasHit)
        {
            if (hit.transform.gameObject.layer == 9)
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            else
                Slice(hit.transform.gameObject,hit.point, velocityEstimator.GetVelocityEstimate());
        }
    }

    void Slice(GameObject target, Vector3 planePosition, Vector3 slicerVelocity)
    {
        Debug.Log("WE SLICE THE OBJECT"); //using debug for checking interaction of object with the blades
        Vector3 slicingDirection = endSlicingPoint.position - startSlicingPoint.position;
        Vector3 planeNormal = Vector3.Cross(slicerVelocity, slicingDirection);

        SlicedHull hull = target.Slice(planePosition, planeNormal, slicedMaterial);

        if(hull != null)
        {
            DisplayScore.score++;

            //upper and lowerhull are used to detect slicing is done in lower or upper portion of the object
            GameObject upperHull = hull.CreateUpperHull(target, slicedMaterial);
            GameObject lowerHull = hull.CreateLowerHull(target, slicedMaterial);

            CreateSlicedComponent(upperHull);
            CreateSlicedComponent(lowerHull);

            Destroy(target);
        }
    }
    //CreateSliced Component function is used to show post slice mesh and destorying them once process is completed
    void CreateSlicedComponent(GameObject slicedHull)
    {
        Rigidbody rb = slicedHull.AddComponent<Rigidbody>();
        MeshCollider collider = slicedHull.AddComponent<MeshCollider>();
        collider.convex = true;

        rb.AddExplosionForce(slicedObjectInitialVelocity, slicedHull.transform.position, 1); // slicing force applied to the object

        Destroy(slicedHull, 4);
    }
}
