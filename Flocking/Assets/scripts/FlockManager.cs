using UnityEngine;

public class FlockManager : MonoBehaviour
{
    //Definindo variaveis//
    public GameObject fishPrefabs;
    public int numFish = 20;
    public GameObject[] allFish;
    public Vector3 swinLimits = new Vector3(5, 5, 5);
    public Vector3 goalPos;

    //configuracao para grupos//

    [Header("Configuracoes do Cardume")]
    [Range(0.0f, 5.0f)]
    public float minSpeed;
    [Range(0.0f, 5.0f)]
    public float maxSpeed;
    [Range(1.0f, 10.0f)]
    public float neighbourDistance;
    [Range(0.0f, 5.0f)]
    public float rotationSpeed;

    void Start()
    {
        //intanciando quantidade de objetos ao grupo//
        allFish = new GameObject[numFish];
        for(int i = 0; i < numFish; i++)
        {

            //spawn randomico aos obejtos //
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-swinLimits.x, swinLimits.x),
                                                                Random.Range(-swinLimits.y, swinLimits.y),
                                                                Random.Range(-swinLimits.z, swinLimits.z));
            
            //nnstanciando obejtos definidos//
            allFish[i] = Instantiate(fishPrefabs, pos, Quaternion.identity);
            allFish[i].GetComponent<Flock>().myManager = this;
        }       
        goalPos = this.transform.position;
    }
    private void Update()
    {
        //movimentacao as posicoes definidas anteriormente//
        goalPos = this.transform.position + new Vector3(Random.Range(-swinLimits.x, swinLimits.x), Random.Range(-swinLimits.y, swinLimits.y), Random.Range(-swinLimits.z, swinLimits.z));
    }
}
