using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Contamination
{
    public class Contamination_Manager : MonoBehaviour
    {
        public static Contamination_Manager instance;

        [SerializeField] public GameObject prefabPanda;
        public List<Panda> forest;


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
                        //Create Panda in good position and stock them into the forest list 
                        GameObject temp = Instantiate(prefabPanda, new Vector3(i,j,k), Quaternion.identity);
                        forest.Add(temp.GetComponent<Panda>());
                    }
                }    
            }

            foreach  (Panda panda in forest)
            {
                panda.FindNeighboors(forest);
            }
        }

        private void Start()
        {
            CreateForest(3);
        }

    }
}
