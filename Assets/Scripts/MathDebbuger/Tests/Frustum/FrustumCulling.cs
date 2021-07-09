using UnityEngine;
using CustomMath;
using System.Collections.Generic;

public class FrustumCulling : MonoBehaviour
{
    public Plane far;
    public Plane near;
    public Plane top;
    public Plane bottom;
    public Plane right;
    public Plane left;

    private bool farOk;
    private bool nearOk;
    private bool topOk;
    private bool bottomOk;
    private bool rightOk;
    private bool leftOk;

    [SerializeField] private GameObject prefabPoint;
    [SerializeField] private GameObject[] pointsFrustrum;

    public List<Vec3> points;

    [SerializeField] public CanRender[] objectsInScene;
    void Start()
    {
        //buscamos los objetos que tengan el script CanBeRender
        objectsInScene = FindObjectsOfType<CanRender>();

        //Hacemos hijos los puntos que usaremos para delimitar el frustum, el padre es el frustum obj
        for (int i = 0; i < pointsFrustrum.Length; i++)
        {
            pointsFrustrum[i] = Instantiate(prefabPoint, points[i], transform.rotation, transform);
            pointsFrustrum[i].name = "Point" + i.ToString();
        }

        //creamos los planos tomando tres puntos, en esto utilizaremos los puntos que definimos antes
        near = new Plane(pointsFrustrum[0].transform.position, pointsFrustrum[1].transform.position,
            pointsFrustrum[2].transform.position);
        right = new Plane(pointsFrustrum[5].transform.position, pointsFrustrum[4].transform.position,
            pointsFrustrum[3].transform.position);
        left = new Plane(pointsFrustrum[6].transform.position, pointsFrustrum[7].transform.position,
            pointsFrustrum[2].transform.position);
        bottom = new Plane(pointsFrustrum[7].transform.position, pointsFrustrum[4].transform.position,
            pointsFrustrum[3].transform.position);
        top = new Plane(pointsFrustrum[5].transform.position, pointsFrustrum[6].transform.position,
            pointsFrustrum[0].transform.position);
        far = new Plane(pointsFrustrum[6].transform.position, pointsFrustrum[5].transform.position,
            pointsFrustrum[4].transform.position);

        //flipeamos el plano right y el near ya que se crean mirando en otra direccion al resto, su normal apunta hacia otro lado
        right.Flip();
        near.Flip();

        //top.Flip();
        //bottom.Flip();
        //left.Flip();
        //right.Flip();
        //far.Flip();
        //near.Flip();
    }

    void Update()
    {
        RenderObjects();
        UpdatePlanes();
    }

    public void UpdatePlanes()
    {
        //Establecemos un plano utilizando tres puntos que esten dentro del plano, estos puntos pueden girar en el sentido de las agujas del reloj
        near.Set3Points(pointsFrustrum[0].transform.position, pointsFrustrum[1].transform.position,
            pointsFrustrum[2].transform.position);

        right.Set3Points(pointsFrustrum[5].transform.position, pointsFrustrum[4].transform.position,
            pointsFrustrum[3].transform.position);

        left.Set3Points(pointsFrustrum[6].transform.position, pointsFrustrum[7].transform.position,
            pointsFrustrum[2].transform.position);

        bottom.Set3Points(pointsFrustrum[7].transform.position, pointsFrustrum[4].transform.position,
            pointsFrustrum[3].transform.position);

        top.Set3Points(pointsFrustrum[5].transform.position, pointsFrustrum[6].transform.position,
            pointsFrustrum[0].transform.position);

        far.Set3Points(pointsFrustrum[6].transform.position, pointsFrustrum[5].transform.position,
            pointsFrustrum[4].transform.position);

        right.Flip();
        near.Flip();
    }

    public void RenderObjects()
    {
        //recorremos todos los planos y chequeamos que todos los planos del frustum tengan su normal positiva con direccion a los objetos a renderizar
        //si todos dan verdadero, significa que hay un objeto dentro del scope del frustum, por lo cual lo reenderizamos
        foreach (CanRender renderObject in objectsInScene)
        {
            topOk = top.GetSide(renderObject.transform.position);
            bottomOk = bottom.GetSide(renderObject.transform.position);
            leftOk = left.GetSide(renderObject.transform.position);
            rightOk = right.GetSide(renderObject.transform.position);
            farOk = far.GetSide(renderObject.transform.position);
            nearOk = near.GetSide(renderObject.transform.position);

            Debug.Log("Plano top :" + top.GetSide(renderObject.transform.position));
            Debug.Log("Plano bot :" + bottom.GetSide(renderObject.transform.position));
            Debug.Log("Plano left :" + left.GetSide(renderObject.transform.position));
            Debug.Log("Plano right :" + right.GetSide(renderObject.transform.position));
            Debug.Log("Plano far :" + far.GetSide(renderObject.transform.position));
            Debug.Log("Plano near :" + near.GetSide(renderObject.transform.position));

            //top.Flip();
            //bottom.Flip();
            //left.Flip();
            //right.Flip();
            //far.Flip();
            //near.Flip();

            if (nearOk && topOk && bottomOk && farOk && leftOk && rightOk)
                renderObject.Render(true);
            else
                renderObject.Render(false);
        }
    }
}
