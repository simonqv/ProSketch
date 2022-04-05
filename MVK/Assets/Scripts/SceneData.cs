using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneData
{
    public int room_width;
    public int room_height;
    public int room_length;
    
    // From what I gathered, it is easiest to save files as binaries? But doing so we can only store
    // int or float arrays, ints, strings or bools.  
    // Therefore I think we can store the different types of objects as numbers, this can of course be difficult to 
    // keep track of tho... and have their positions in a position array. So when we load the file, we would do something
    // like reading one object from objects and then 3 coordinates from positions, to place it.
    // This is however just a first idea, y'all might have a better idea. 
    public int[] objects;
    public float[] positions;

    public SceneData(RoomClass room)
    {
        // Just placeholders for now
        room_width = 1;
        room_height = 1;
        room_length = 1;
        
        int num_objects_in_scene = 10;
        objects = new int[num_objects_in_scene];
        positions = new float[num_objects_in_scene * 3];
        
        // create for-loop to iterate over objects and populate objects and positions
    }
    
    
}
