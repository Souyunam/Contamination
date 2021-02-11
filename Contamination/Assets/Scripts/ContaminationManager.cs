using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Contamination
{
    public class ContaminationManager : MonoBehaviour
    {
        #region Variables
        public static ContaminationManager instance;

        [SerializeField] private GameObject prefabPanda;

        private List<Panda> forest = new List<Panda>();
        private List<Panda> contagiousPandas = new List<Panda>();

        private int numberOfContagiousPanda = 0;
        private int numberOfContaminatedPanda = 0;
        private int numberOfDay = 0;

        public int forestDimension;

        private Panda patientZero;
        private bool confirmPatientZero = false;
        #endregion

        #region ProtectionVariable
        public void AddContagiousPanda(Panda newContagousPanda)
        {
            ///<summary> this add a panda in paramater to the contagious list</summary>
            contagiousPandas.Add(newContagousPanda);
            numberOfContagiousPanda += 1;
            numberOfContaminatedPanda += 1;
        }

        public void RemoveContagiousPanda(Panda curedPanda)
        {
            ///<summary> this remove panda in paramater to the contagious list</summary>
            contagiousPandas.Remove(curedPanda);
            numberOfContagiousPanda -= 1;
        }

        public bool ConfirmPatientZero()
            ///<summary> Check if patient zero is set (true) or not (false)</summary>
        {
            if(patientZero == null)
            { 
                return false;
            }

            else
            {
                confirmPatientZero = true;
                patientZero.IsContaminated();
                float contaminationRate = numberOfContaminatedPanda * 100f / (forestDimension * forestDimension * forestDimension);
                AppManager.instance.UpdateTextContamination(contaminationRate, numberOfDay, contaminationRate == 100f);
                return true;
            }
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

        private void OnEnable()
        {
            ResetToZero();
        }
        public void ResetToZero()
        {
            ///<summary> restart all the contamination when you reset the app</summary>
            foreach (Panda panda in forest)
            {
                panda.KillIt();
            }
            
            forest = new List<Panda>();
            contagiousPandas = new List<Panda>();
            numberOfContagiousPanda = 0;
            numberOfContaminatedPanda = 0;
            numberOfDay = 0;
            //Set patientZero to null
            patientZero = null;
            confirmPatientZero = false;
        }

        public void CreateForest()
            ///<summary>
            ///This will create a forest as a cube forestDimension x forestDimension x forestDimension
            ///</summary>

        {
            for (int i = 0; i < forestDimension; i++)
            {
                for (int j = 0; j < forestDimension; j++)
                {
                    for (int k = 0; k < forestDimension; k++)
                    {
                        //Create Panda in good position and stock them into the forest list 
                        GameObject temp = Instantiate(prefabPanda, new Vector3(i,j,k), Quaternion.identity,this.transform);
                        forest.Add(temp.GetComponent<Panda>());
                    }
                }    
            }

            //Find all the neighboors
            foreach  (Panda panda in forest)
            {
                panda.FindNeighboors(forest);
            }
        }



        private void spreadingDisease(List<Panda> listPandas)
            ///<summary>
            ///For panda who are still contagious continue to spread the disease in the forest
            ///</summary>
            ///<param name="listStillContagiousPanda"> this will contains the list of pandas who can still contaminate other panda</param>
        {
            numberOfDay += 1;
            Panda[] currentContagiousPandas = new Panda[listPandas.Count];
            listPandas.CopyTo(currentContagiousPandas);

            foreach (Panda panda in currentContagiousPandas)
            {
                panda.Contaminate();
            }
            
        }

        public void SpreadVirus()
            ///This function call spreadingDisease for using it outside this class and get back the contamination rate and hte number of day until the beginning
            ///<summary> spread the disease into the forest</summary>
        {
            spreadingDisease(contagiousPandas);
            float contaminationRate = numberOfContaminatedPanda * 100f / (forestDimension * forestDimension * forestDimension);
            AppManager.instance.UpdateTextContamination(contaminationRate, numberOfDay, contaminationRate == 100f);
        }

        #region Selection

        private void Update()
        {
            ///This will raycast a ray and if it touch a panda this one will be selected
            if(!confirmPatientZero && Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Panda")
                    {
                        if(patientZero != null)
                        {
                            patientZero.Unselect();
                        }
                        patientZero = hit.collider.gameObject.GetComponent<Panda>();
                        patientZero.IsSelected();
                    }
                }
            }
        }
        #endregion
    }
}
