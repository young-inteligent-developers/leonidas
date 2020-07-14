using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
    {
        public bool Unlocked;
        public bool CanUnlocked;
        //public bool Defence;
        //public bool Attack;
        public string Name;
        public int Cost;
        public GameObject afterSkill;

        Color[] c = { Color.red, Color.gray, Color.green };

        void Start()
        {
            if (Unlocked == false) 
            {
                this.GetComponent<Image>().color = c[0];
            }
        }

        void Update()
        {
            if (CanUnlocked == true)
            {
                this.GetComponent<Image>().color = c[1];
            }

            if (Unlocked == true)
            {
                this.GetComponent<Image>().color = c[2];
            }

            
        }
    }

