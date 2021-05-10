using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    //Definindo variaveis//

    public FlockManager myManager;
    float speed;
    bool turnig = false;

    void Start()
    {

        //passando um valor aleatorio dentro do range pre definido//
        speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
    }

    void Update()
    {
        //passando limite de map para nadar//
        Bounds b = new Bounds(myManager.transform.position, myManager.swinLimits * 2);

        //condicoes de test//
        if (!b.Contains(transform.position))
        {
            turnig = true;
        }
        else
        {
            turnig = false;
        }

        if (turnig)
        {
            //definindo locomocao aos objetos//

            Vector3 direction = myManager.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), myManager.rotationSpeed * Time.deltaTime);
        }
        else
        {

            //definindo velocidade para retorno ao grupo //
            if (Random.Range(0,100) < 10)
            {
                speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
            }
            if(Random.Range(0,100) < 20)
            {
                ApplyRules();
            }
        }

        //direcao final dos objetos e posicao //
        transform.Translate(0, 0, Time.deltaTime * speed);
    }

    void ApplyRules()
    {
        //Definindo variaveis//

        GameObject[] gos;
        Vector3 vcentre = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        float gSpeed = 0.01f;
        float nDistance;
        int groupSize = 0;

        //pegando todos os objetos do grupo//

        gos = myManager.allFish;

        foreach (GameObject go in gos)
        {
            if (go != this.gameObject)
            {
                //passando ao codigo as distancias entre os objetos //

                nDistance = Vector3.Distance(go.transform.position, this.transform.position);
                //testando a variavel com a do FlockManager

                if (nDistance <= myManager.neighbourDistance)
                {
                    vcentre += go.transform.position;

                    //adicionando objeto ao grupo//
                    groupSize++;

                    //test para evitar colisao com os outros peixes //
                    if (nDistance < 1.0)
                    {
                        vavoid = vavoid + (this.transform.position - go.transform.position);
                    }
                    //busca componente com flock//
                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }

        //tamanho do grupo //
        if (groupSize > 0)
        {
            vcentre = vcentre / groupSize + (myManager.goalPos - this.transform.position);
            speed = gSpeed / groupSize;

            Vector3 direction = (vcentre + vavoid) - transform.position;
            if(direction != Vector3.zero)
            {
                //rotacao natural/ quartenin//
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), myManager.rotationSpeed * Time.deltaTime);
            }
        }
    }
}
