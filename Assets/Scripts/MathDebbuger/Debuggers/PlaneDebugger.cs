using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathDebbuger.Internals;
using System;

public class PlaneDebugger : MonoBehaviour
{
    private Plane up;
    private Plane down;
    private Plane left;
    private Plane right;
    private Plane front;
    private Plane back;

    [SerializeField] private Transform cube;

    Vector3 point1 = new Vector3(-5, -5, -5);
    Vector3 point2 = new Vector3(5, -5, -5);
    Vector3 point3 = new Vector3(5, 5, -5);
    Vector3 point4 = new Vector3(-5, 5, -5);
    Vector3 point5 = new Vector3(5, -5, 5);
    Vector3 point6 = new Vector3(-5, -5, 5);
    Vector3 point7 = new Vector3(-5, 5, 5);
    Vector3 point8 = new Vector3(5, 5, 5);

    private bool checkUp;
    private bool checkDown;
    private bool checkLeft;
    private bool checkRight;
    private bool checkFront;
    private bool checkBack;
    void Start()
    {
        up = new Plane(point3, point7);
        down = new Plane(point1, point5);
        left = new Plane(point1, point7);
        right = new Plane(point2, point8);
        front = new Plane(point5, point7);
        back = new Plane(point1, point3);
    }


    void Update()
    {
        checkUp = up.GetSide(cube.transform.position);
        checkDown = down.GetSide(cube.transform.position);
        checkLeft = left.GetSide(cube.transform.position);
        checkRight = right.GetSide(cube.transform.position);
        checkFront = front.GetSide(cube.transform.position);
        checkBack = back.GetSide(cube.transform.position);

        if(checkUp && checkDown && checkLeft && checkRight && checkFront && checkDown)
        {
            Debug.Log("Esta adentro!");
        }
        else
        {
            Debug.Log("Esta afuera!");
        }

        //Debug.Log("Plano UP" + up.GetSide(cube.transform.position).ToString());
        //Debug.Log("Plano DOWN" + down.GetSide(cube.transform.position).ToString());
        //Debug.Log("Plano LEFT" + left.GetSide(cube.transform.position).ToString());
        //Debug.Log("Plano RIGHT" + right.GetSide(cube.transform.position).ToString());
        //Debug.Log("Plano FRONT" + front.GetSide(cube.transform.position).ToString());
        //Debug.Log("Plano BACK" + back.GetSide(cube.transform.position).ToString());
    }
}
