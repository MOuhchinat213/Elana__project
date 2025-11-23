using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform follow;


    // Update is called once per frame
    void Update()
    {
        Vector3 pos = follow.position;
        pos.x = follow.position.x;
        pos.y=0;
        pos.z=-4f;
        transform.position=pos;
    
    }
}
