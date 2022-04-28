using System;
using UnityEngine;

public class RoomClass : MonoBehaviour
{
    private static int _length = 20;
    private static int _width = 20;
    private double _angle;
    private int _height;
    private const int Height = 9;

    private GameObject _roomPrefab;
    private Camera _camera;
    public Camera cam;
    private float[] _pos;
    private float[] _angles;

    private void Start()
    {
        
    }

    public int GetLength() { return _length; }

    public int GetWidth() { return _width; }

    public int GetHeight() { return Height; }
    public static void Setter(int len, int wid)
    {
        _length = len;
        _width = wid;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && cam.transform.position.x > 0)
        {
            MoveCamera(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && cam.transform.position.x < 0)
        {
            MoveCamera(-1);
        }
    }

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
        // Instansiera kamera, placear i ett hörn, vinkla
        _camera = Resources.Load<Camera>("Room/Camera");
        
        var lsq = Mathf.Pow(_length, 2);
        var wsq = Mathf.Pow(_width, 2);
        var lw = Mathf.Sqrt(lsq + wsq);
        var alpha = Mathf.Atan(Height*2/lw) * (180/Mathf.PI); // Rotation around cameras x-axis
        var beta = Mathf.Atan((float) _length / _width) * (180/Mathf.PI);    // Rotation around cameras y-axis
        _pos = new float[] {(-_length / 2f) * 100 * 3, Height * 100*3, (-_width / 2f) * 100 *3};
        _angles = new float[] {alpha, beta};
        _height = (int)((float)_width / 2 / Mathf.Tan(30 * Mathf.PI / 180)+Height) * 100;
        cam = Instantiate(_camera);
        cam.transform.position = new Vector3(_pos[0] ,_pos[1], _pos[2] );
        cam.transform.Rotate(_angles[0], _angles[1], 0f, Space.Self);
        

    }
    

    
    // Input: P = 1 for camera position 1, P = -1 for camera position 2
    private void MoveCamera(int p)
    {
        cam.transform.position = new Vector3(_pos[0] * p,_pos[1], _pos[2] * p);
        cam.transform.Rotate(0, 180, 0, Space.World);
        
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