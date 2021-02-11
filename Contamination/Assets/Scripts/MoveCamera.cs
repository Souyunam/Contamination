using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private float speed;

    public Vector3 center;
    public static MoveCamera instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public void SetCameraPosition(int dimension)
    {
        ///<summary>Set the camera position dependin of dimension of the forest</summary>
        ///Camera will look at the "center" of the forest cube and the distance between the cube and camera will depend on the dimension 
        float temp = (dimension - 1) / 2f;
        center = new Vector3(temp,temp,temp);
        Vector3 newPosition = this.transform.position;
        newPosition.z = -dimension * 2;
        this.transform.position = newPosition;
    }

    private void Update()
    {
        //The camera wiil always look at the center of the cube
        this.transform.LookAt(center);
         
        //You can move the camera with the axis
        transform.RotateAround(center, Vector3.up, -Input.GetAxis("Horizontal") * speed);
        transform.RotateAround(center, transform.right, -Input.GetAxis("Vertical") * speed);
    }
}
