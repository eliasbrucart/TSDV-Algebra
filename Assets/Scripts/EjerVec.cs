using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathDebbuger;
using CustomMath;

public class EjerVec : MonoBehaviour
{
    public Vector3 FirstVector;
    public Vector3 SecondVector;
    public Vector3 ThirdVector;
    Vec3 first;
    Vec3 second;
    Vec3 third;
    float t = 0f;
    public enum Options
    {
        uno,
        dos,
        tres,
        cuatro,
        cinco,
        seis,
        siete,
        ocho,
        nueve,
        diez
    }
    public Options opciones = Options.uno;
    void Start()
    {

        FirstVector = new Vector3(10.0f, 10.0f, 10.0f);
        SecondVector = new Vector3(25.0f, 20.0f, 20.0f);

        first = new Vec3(FirstVector);
        second = new Vec3(SecondVector);

        Vector3Debugger.AddVector(FirstVector, Color.white, "First");
        Vector3Debugger.AddVector(SecondVector, Color.black, "Second");
        Vector3Debugger.AddVector(ThirdVector, Color.red, "Third");

        Vector3Debugger.EnableEditorView();
    }

    // Update is called once per frame
    void Update()
    {
        first = new Vec3(FirstVector);
        second = new Vec3(SecondVector);

        switch (opciones)
        {
            case Options.uno:
                third = first + second;
                break;
            case Options.dos:
                third = second - first;
                break;
            case Options.tres:
                third = Vec3.Scale(first, second);
                break;
            case Options.cuatro:
                third = Vec3.Cross(second, first);
                break;
            case Options.cinco:
                third = Vec3.Lerp(first, second, t);
                t += Time.deltaTime;
                if (t >= 1f)
                    t = 0f;
                break;
            case Options.seis:
                third = Vec3.Max(first, second);
                break;
            case Options.siete:
                third = Vec3.Project(first, second);
                break;
            case Options.ocho:
                float val = Vec3.Distance(first, second);
                Vec3 sum = first + second;
                third = val * sum.normalized;
                break;
            case Options.nueve:
                third = Vec3.Reflect(first, second);
                break;
            case Options.diez:
                third = Vec3.LerpUnclamped(second, first, t);
                t += Time.deltaTime;
                if (t >= 10f)
                    t = 0f;
                break;
            default:
                break;
        }

        //first = new Vec3(FirstVector);
        //second = new Vec3(SecondVector);
        //third = new Vec3(ThirdVector);

        Vector3Debugger.UpdatePosition("First", first);
        Vector3Debugger.UpdatePosition("Second", second);
        Vector3Debugger.UpdatePosition("Third", third);

        Plane a = new Plane();
    }
}
