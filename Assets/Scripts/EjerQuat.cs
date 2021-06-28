using UnityEngine;
using MathDebbuger;

public class EjerQuat : MonoBehaviour
{
    public Vector3 White;
    public Vector3 Black;
    public Vector3 Red;
    public Vector3 Green;
    public GameObject go1;
    public GameObject go2;
    public float speed;
    public float angle;
    Vector3 origRot = new Vector3(0, 90, 0);
    Vector3 origVec = new Vector3(10, 0, 0);

    public enum Ejercicio
    {
        uno,
        dos,
        tres
    }
    public Ejercicio ejercicios = Ejercicio.uno;
    Ejercicio lastEjercicios = Ejercicio.uno;
    void Start()
    {

        White = origVec;
        Black = new Vector3(25.0f, 20.0f, 20.0f);

        Vector3Debugger.AddVector(White, Color.white, "Blanco");
        Vector3Debugger.AddVector(Black, Color.black, "Negro");
        Vector3Debugger.AddVector(Red, Color.red, "Rojo");
        Vector3Debugger.AddVector(Red, Color.green, "Verde");
        Vector3Debugger.TurnOffVector("Blanco");
        Vector3Debugger.TurnOffVector("Negro");
        Vector3Debugger.TurnOffVector("Rojo");
        Vector3Debugger.TurnOffVector("Verde");
        Vector3Debugger.EnableEditorView();
    }

    void Update()
    {
        if (ejercicios != lastEjercicios)
        {
            go1.transform.rotation = Quaternion.Euler(origRot);
            go2.transform.rotation = Quaternion.Euler(origRot);
            lastEjercicios = ejercicios;
        }

        Vector3 a;
        Vector3 b;
        switch (ejercicios)
        {
            case Ejercicio.uno:

                go1.transform.rotation = go1.transform.rotation * Quaternion.Euler(new Vector3(0, angle * Time.deltaTime * speed, 0));
                a = go1.transform.forward;
                White = a * 10.0f;
                Black = Vector3.zero;
                Red = Vector3.zero;
                Green = Vector3.zero;

                break;
            case Ejercicio.dos:
                go1.transform.rotation = go1.transform.rotation * Quaternion.Euler(new Vector3(0, angle * Time.deltaTime * speed, 0));
                a = go1.transform.forward;
                White = a * 10;
                Black = White + Vector3.up * 10;
                Red = Black + a * 10.0f;
                Green = Red;
                break;
            case Ejercicio.tres:
                go1.transform.rotation = go1.transform.rotation * Quaternion.Euler(0, angle * Time.deltaTime * speed, angle * Time.deltaTime * speed);
                a = go1.transform.forward;
                go2.transform.rotation = go2.transform.rotation * Quaternion.Euler(0, -angle * Time.deltaTime * speed, -angle * Time.deltaTime * speed);
                b = go2.transform.forward;
                White = a * 10.0f;
                Black = White + (go1.transform.up) * 10;
                Red = Black + b * 10.0f;
                Green = Red + (go2.transform.up) * 10;

                break;
            default:
                break;
        }

        Vector3Debugger.UpdatePosition("Blanco", White);
        Vector3Debugger.UpdatePosition("Negro", White, Black);
        Vector3Debugger.UpdatePosition("Rojo", Black, Red);
        Vector3Debugger.UpdatePosition("Verde", Red, Green);
    }
}
