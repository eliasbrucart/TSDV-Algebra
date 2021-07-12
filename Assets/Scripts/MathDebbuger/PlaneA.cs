using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CustomMath
{
    [Serializable]
    public struct PlaneA
    {
        public Vec3 normal { get; set; }
        public float distance { get; set; }
        public PlaneA flipped
        {
            get
            {
                return new PlaneA(-normal, -normal * distance);
            }
        }
        //constructor de PlaneA
        //creamos un plano a partir de dos vectores, uno que seria la normal y otro que seria un punto en el plano
        //seteamos la distancia a partir del producto punto entre la normal y punto que le demos
        public PlaneA(Vec3 inNormal, Vec3 inPoint)
        {
            normal = inNormal.normalized;
            distance = -Vec3.Dot(normal, inPoint);
        }
    
        public PlaneA(Vec3 a, Vec3 b, Vec3 c)
        {
            Vec3 sideA = b - a;
            Vec3 sideB = c - a;

            normal = Vec3.Cross(sideA, sideB).normalized;
            distance = -Vec3.Dot(normal, a);
        }
        
        //Convierto los Vector3 a vec3, asi luego seteo la normal y el distance del plano
        public PlaneA(Vector3 a, Vector3 b, Vector3 c)
        {
            Vec3 myVec3a;
            Vec3 myVec3b;
            Vec3 myVec3c;
            myVec3a.x = a.x;
            myVec3a.y = a.y;
            myVec3a.z = a.z;
            myVec3b.x = b.x;
            myVec3b.y = b.y;
            myVec3b.z = b.z;
            myVec3c.x = c.x;
            myVec3c.y = c.y;
            myVec3c.z = c.z;
            Vec3 sideA = myVec3b - myVec3a;
            Vec3 sideB = myVec3c - myVec3a;

            normal = Vec3.Cross(sideA, sideB).normalized;
            distance = -Vec3.Dot(normal, myVec3a);
        }

        //crea una copia del plano con una nueva posicion, producto de la traslacion
        //creamos un vector auxiliar que tendra el valor invertido de la normal del plano 
        //multiplicado por la distancia del mismo sumado el vector traslacion
        //El vector auxiliar es el punto que le pasamos al constructor para crear el plano
        public static PlaneA Translate(PlaneA planea, Vec3 translation)
        {
            Vec3 aux = - (planea.normal * planea.distance + translation);
            return new PlaneA(planea.normal, aux);
        }

        public Vec3 ClosetsPointOnPlane(Vec3 point)
        {
            return (point - normal * GetDistanceToPoint(point));
        }

        public void Flip()
        {
            normal = -normal;
            distance = -distance;
        }

        //retorna la distancia desde la normal al punto
        public float GetDistanceToPoint(Vec3 point)
        {
            return Vec3.Dot(normal, point) + distance;
        }
        //retorna verdadero si el punto que le pasamos esta del lado positivo del plano
        //si el producto punto entre la normal del plano y el punto sumado a la distancia del plano da mayor a cero
        //significa que ese punto esta del lado positivo, en caso contrario, esta del lado negativo
        public bool GetSide(Vec3 point)
        {
            if ((Vec3.Dot(normal, point) + distance) > 0)
                return true;
            else
                return false;
        }
        //retorna verdadero si el punto que le pasamos esta del lado positivo del plano
        public bool GetSide(Vector3 point)
        {
            Vec3 pointA;
            pointA.x = point.x;
            pointA.y = point.y;
            pointA.z = point.z;
            if ((Vec3.Dot(normal, pointA) + distance) > 0)
                return true;
            else
                return false;
        }

        //retorna verdadero si dos puntos estan del lado positivo del plano
        //comparamos si ambos puntos retornan true utilizando GetSide
        //si es asi retornamos true
        public bool SameSide(Vec3 in0, Vec3 in1)
        {
            if (GetSide(in0) == GetSide(in1))
                return true;
            else
                return false;
        }
        //establecemos un plano utilizando tres puntos que se encuentren dentro de el
        //para ello necesitamos setear la normal y distance del plano
        //como son tres puntos creamos dos vectores que representen los dos lados del plano
        //luego calculamos el producto cruz de ambos vectores y normalizamos el resultado
        //calculamos la distance invirtiendo el resultado de dot entre la normal y el primer punto recibido
        public void Set3Points(Vec3 a, Vec3 b, Vec3 c)
        {
            Vec3 side1 = b - a;
            Vec3 side2 = c - a;

            normal = Vec3.Cross(side1, side2).normalized;
            distance = -Vec3.Dot(normal, a);
        }

        public void Set3Points(Vector3 a, Vector3 b, Vector3 c)
        {
            Vec3 myVec3a;
            Vec3 myVec3b;
            Vec3 myVec3c;
            myVec3a.x = a.x;
            myVec3a.y = a.y;
            myVec3a.z = a.z;
            myVec3b.x = b.x;
            myVec3b.y = b.y;
            myVec3b.z = b.z;
            myVec3c.x = c.x;
            myVec3c.y = c.y;
            myVec3c.z = c.z;
            Vec3 side1 = myVec3b - myVec3a;
            Vec3 side2 = myVec3c - myVec3a;

            normal = Vec3.Cross(side1, side2).normalized;
            distance = -Vec3.Dot(normal, myVec3a);
        }
        //establecemos un plano utilizando un punto que se encuentra dentro del plano y una normal para orientarlo
        //la normal siempre es un vector normalizado
        //luego se calcula la distancia del plano con un producto punto invertido entre la normal y el punto dentro de el.
        public void SetNormalAndPosition(Vec3 inNormal, Vec3 inPoint)
        {
            normal = inNormal.normalized;
            distance = -Vec3.Dot(inNormal, inPoint);
        }
        //crea una copia del plano con una nueva posicion, producto de la traslacion
        //creamos un vector auxiliar a partir de la multiplicacion de la normal del plano y su distance
        //sumado al vector traslacion que le pasamos al metodo
        //calculamos la distancia del plano con el dot entre la normal y el vector auxiliar
        public void Translate(Vec3 translation)
        {
            Vec3 aux = (normal * distance + translation);
            distance = Vec3.Dot(normal, aux);
        }

        public override string ToString()
        {
            return "(normal:("+normal.x+", "+normal.y+","+ normal.z+"), distance:"+distance+")";
        }
    }
}

