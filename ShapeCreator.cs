using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sebastian.Geometry;
using System.Linq;


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



    public static Mesh createMesh(IEnumerable<Vector3> _verts,bool _3d = false)
    {

        ShapeCreator new_ShapeCreator = new GameObject().AddComponent<ShapeCreator>();

        new_ShapeCreator.shapes.Add(new Sebastian.Geometry.Shape());




        new_ShapeCreator.shapes[0].points = new List<Vector3>(_verts);

        new_ShapeCreator.UpdateMeshDisplay();

        if(_3d == true)
        {

            List<Vector3> _points = new List<Vector3>(new_ShapeCreator.my_MeshFilter.mesh.vertices);
            List<int> _triangles = new List<int>(new_ShapeCreator.my_MeshFilter.mesh.triangles);



            int _n_points = _points.Count;


            List<int> _bottom_face_triangles = new List<int>(new_ShapeCreator.my_MeshFilter.mesh.triangles.Select(x => x + _n_points)).ToList(); ;

            _bottom_face_triangles.Reverse();



            _points.AddRange(_points);

            //ok now we must stich together the sides...
            for(int i = 0; i < _n_points - 1; i += 1)
            {
                //triangles
                _triangles.Add(i);
                _triangles.Add(i + 1);
                _triangles.Add(i + _n_points);

                _triangles.Add(i + 1);
                _triangles.Add(i + _n_points + 1);
                _triangles.Add(i + _n_points);
            }

            _triangles.Add(_n_points - 1);
            _triangles.Add(0);
            _triangles.Add(_n_points * 2 - 1);

            _triangles.Add(0);
            _triangles.Add(_n_points);
            _triangles.Add(_n_points * 2 - 1);


            _triangles.AddRange(_bottom_face_triangles);




            new_ShapeCreator.my_MeshFilter.mesh.vertices = _points.ToArray();
            new_ShapeCreator.my_MeshFilter.mesh.triangles = _triangles.ToArray();
            new_ShapeCreator.my_MeshFilter.mesh.RecalculateNormals();

            List<Vector3> _normals = new List<Vector3>();

            for (int i = 0; i < _n_points; i += 1)
            {
                _normals.Add(new Vector3(0, 1, 0));
            }

            for (int i = 0; i < _n_points; i += 1)
            {
                _normals.Add(new Vector3(0, 0, -1));
            }

            new_ShapeCreator.my_MeshFilter.mesh.normals = _normals.ToArray();





        }

        Mesh _Mesh = new_ShapeCreator.my_MeshFilter.mesh;

        Destroy(new_ShapeCreator.gameObject);

        return _Mesh;

    }



}
