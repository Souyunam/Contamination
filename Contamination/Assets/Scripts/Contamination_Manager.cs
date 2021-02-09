using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Contamination
{
    public class Contamination_Manager : MonoBehaviour
    {
        #region Variables
        public static Contamination_Manager instance;

        [SerializeField] private GameObject prefabPanda;

        private List<Panda> forest;
        public List<Panda> contagiousPandas;
        #endregion

        #region ProtectionVariable
        public void AddContagiousPanda(Panda newContagousPanda)
        {
            contagiousPandas.Add(newContagousPanda);
        }

        public void RemoveContagiousPanda(Panda curedPanda)
        {
            contagiousPandas.Remove(curedPanda);
        }
        #endregion

        #region Instance
        private void Awake() 
        {
            // We create an instance of this script
            if (instance == null)
            {
                instance = this;
            }
        }
        #endregion

        private void Start()
        {
            forest = new List<Panda>();
            contagiousPandas = new List<Panda>();

            CreateForest(3);
            //Set patientZero
            forest[0].IsContaminated();
            //Spread the disease
            //Create curves
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("appuie espace");
                spreadingDisease(contagiousPandas);
            }
        }

        private void CreateForest(int forestDimension)
            ///<summary>
            ///This will create a forest as a cube forestDimension x forestDimension x forestDimension
            ///</summary>
            ///<param name="forestDimension"> number of pandas into the forest (dimension of the cube)</param>
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



        //Selection 


        //Contamination
        private void spreadingDisease(List<Panda> listPandas)
            ///<summary>
            ///For panda who are still contagious continue to spread the disease in the forest
            ///</summary>
            ///<param name="listStillContagiousPanda"> this will contains the list of pandas who can still contaminate other panda</param>
        {
            Panda[] currentContagiousPandas = new Panda[listPandas.Count];
            listPandas.CopyTo(currentContagiousPandas);

            foreach (Panda panda in currentContagiousPandas)
            {
                print(listPandas.Count);
                panda.Contaminate();
            }
        }
    }
}
