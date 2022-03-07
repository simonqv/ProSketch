using System;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class RoomClass : MonoBehaviour
{
    private static int _length;
    private static int _width;
    private double _angle = 45.0;
    private int _height;

    private GameObject _roomPrefab;

    private GameObject _cameraOne;
    private void Start()
    {
        
    }

    public static void Setter(int len, int wid)
    {
        _length = len;
        _width = wid;
    }    

    public void CreateRoom()
    {
        try
        {
            _roomPrefab = Resources.Load<GameObject>("Room/Room");
            var lsq = Mathf.Pow(_length, 2);
            var wsq = Mathf.Pow(_width, 2);
            var lw = Mathf.Sqrt(lsq + wsq); 
            _height = (int)Math.Ceiling(lw / Math.Tan(_angle));
            var room = Instantiate(_roomPrefab);
            room.transform.localScale = new Vector3(_length, _height, _width); 
            //var position = room.transform.position;
            //var xpos = position.x;
            //var ypos = position.y;
            //var zpos = position.z;
            //Debug.Log(xpos + " " + ypos + " " + zpos);
        }
        catch (Exception e)
        {
            Debug.Log("CLASS");
            Console.WriteLine(e.Message);        
        }
    }
    
    
}