using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sebastian.Geometry;


[RequireComponent(typeof(MeshFilter))]

public class ShapeCreator : MonoBehaviour
{
    public MeshFilter my_MeshFilter { get { return GetComponent<MeshFilter>(); } }
    
    public List<Sebastian.Geometry.Shape> shapes = new List<Sebastian.Geometry.Shape>();




    public void UpdateMeshDisplay()
    {
        CompositeShape compShape = new CompositeShape(shapes);
        my_MeshFilter.mesh = compShape.GetMesh();
    }



    public static Mesh createMesh(IEnumerable<Vector3> _verts)
    {

        ShapeCreator new_ShapeCreator = new GameObject().AddComponent<ShapeCreator>();

        new_ShapeCreator.shapes.Add(new Sebastian.Geometry.Shape());



        new_ShapeCreator.shapes[0].points = new List<Vector3>(_verts);


        new_ShapeCreator.UpdateMeshDisplay();


        return new_ShapeCreator.my_MeshFilter.mesh;

    }


#if UNITY_EDITOR

    private void OnValidate()
    {
        UpdateMeshDisplay();
    }


#endif


}
