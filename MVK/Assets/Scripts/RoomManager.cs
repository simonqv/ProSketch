using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var room = gameObject.AddComponent<RoomClass>();
        room.CreateRoom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
