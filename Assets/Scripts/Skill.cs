using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
    {
        public bool Unlocked;
        //public bool Defence;
        //public bool Attack;
        public string Name;
        public int Cost;

        Color[] c = { Color.red, Color.gray, Color.green };

        void Start()
        {
            if (Unlocked == true)
            {
                this.GetComponent<Image>().color = c[2];
            }
        }
    }

