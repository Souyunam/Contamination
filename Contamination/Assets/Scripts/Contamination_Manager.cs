using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contamination_Manager : MonoBehaviour
{
    public static Contamination_Manager instance;

    [SerializeField] public GameObject prefabPanda;
    private List<Panda> forest;


    private void Awake()
    {
        // We create an instance of this script
        if (instance == null)
        {
            instance = this;
        }
    }

    private void CreateForest(int forestDimension)
        ///This will create a forest as a cube forestDimension x forestDimension x forestDimension///
    {
        for (int i = 0; i < forestDimension; i++)
        {
            for (int j = 0; j < forestDimension; j++)
            {
                for (int k = 0; k < forestDimension; k++)
                {
                    Debug.Log(new Vector3(i,j,k));
                    new Panda(new Vector3(i, j, k));
                }
            }    
        }
    }

    private void Start()
    {
        CreateForest(3);
    }

}
