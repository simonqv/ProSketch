using System;
using UnityEngine;

public class RoomClass : MonoBehaviour
{
    private static int _length = 20;
    private static int _width = 20;
    private double _angle;
    private const int Height = 9;

    private GameObject _roomPrefab;
    private Camera _camera; // Fetching camera prefab from Resources
    public Camera cam; // Instantiate camera that is fetched from _camera
    private float[] _pos;
    private float[] _angles;

    /*
     * Set length and width of room
     */
    public static void Setter(int len, int wid)
    {
        _length = len;
        _width = wid;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && cam.transform.position.x > 0)
        {
            InvertCamera();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && cam.transform.position.x < 0)
        {
            InvertCamera();
        }
    }

    /*
     * Finds room prefab, alters the width and length according to the setter, and spawns camera.
     */
    public void CreateRoom()
    {
        try
        {
            _roomPrefab = Resources.Load<GameObject>("Room/Room");
            var room = Instantiate(_roomPrefab);
            room.transform.localScale = new Vector3(_length, Height, _width);
            CreateCamera();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);        
        }
    }

    private void CreateCamera()
    {
        // Instantiate camera, places in a corner and angles it.
        _camera = Resources.Load<Camera>("Room/Camera");
        
        var lsq = Mathf.Pow(_length, 2);
        var wsq = Mathf.Pow(_width, 2);
        var lw = Mathf.Sqrt(lsq + wsq);
        var alpha = Mathf.Atan(Height*2/lw) * (180/Mathf.PI); // Rotation around cameras x-axis
        var beta = Mathf.Atan((float) _length / _width) * (180/Mathf.PI);    // Rotation around cameras y-axis
        _pos = new float[] {(-_length / 2f) * 100 * 3, Height * 100*3, (-_width / 2f) * 100 *3};
        _angles = new float[] {alpha, beta};
        cam = Instantiate(_camera);
        cam.transform.position = new Vector3(_pos[0] ,_pos[1], _pos[2] );
        cam.transform.Rotate(_angles[0], _angles[1], 0f, Space.Self);
    }
    
    public void InvertCamera()
    {
        var transform1 = cam.transform;
        var position = transform1.position;
        var x = position.x;
        var y = position.y;
        var z = position.z;
        transform1.position = new Vector3(-x, y, -z);
        transform1.Rotate(0, 180, 0, Space.World);
    }
}