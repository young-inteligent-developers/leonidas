using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
    {
        public bool unlocked;
        public bool canUnlock;
        //public bool Defence;
        //public bool Attack;
        //public string name;
        public int cost;
        public GameObject afterSkill;

        Color[] c = { Color.red, Color.gray, Color.green };

        void Start()
        {
            if (unlocked == false) 
            {
                this.GetComponent<Image>().color = c[0];
            }
        }

        void Update()
        {
            if (canUnlock == true)
            {
                this.GetComponent<Image>().color = c[1];
            }

            if (unlocked == true)
            {
                this.GetComponent<Image>().color = c[2];
            }

            
        }
    }

