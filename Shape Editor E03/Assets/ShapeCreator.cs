using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sebastian.Geometry;


[RequireComponent(typeof(MeshFilter))]

public class ShapeCreator : MonoBehaviour
{
    public MeshFilter MeshFilter;
    
    public List<Sebastian.Geometry.Shape> shapes = new List<Sebastian.Geometry.Shape>();

    public float handleRadius = .5f;


   
    [SerializeField] public bool showShapesList;



    public void UpdateMeshDisplay()
    {
        CompositeShape compShape = new CompositeShape(shapes);
        MeshFilter.mesh = compShape.GetMesh();
    }



}
