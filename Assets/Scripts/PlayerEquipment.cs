using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    [SerializeField]Armor[] currentLoadOut = new Armor[6];
    private int[] flatStatBoost = new int[3];// HP, ATK, DEF
    private float[] percentStatBoost = new float[7];//HP, ATK, DEF,POT, RES, CRate,CDMG,SPGain
    // Start is called before the first frame update
    void Start()
    {
        // int j = 0;
        // for (int i = 0; i < 6; i++)
        // {
        //     if (armor[i] != null)
        //     {
        //         if (i < 2)
        //         {
        //             if (i == 0)
        //                 flatStatBoost[0] = armor[i].statval[0];
        //             else
        //                 flatStatBoost[2] = armor[i].statval[0];
        //         }
        //         else
        //         {
        //             percentStatBoost[armor[i].stat] = armor[i].statval[0];
        //         }

        //         for (j = 1; j < armor[i].statval; j++)
        //         {
        //             percentStatBoost[armor[i].stat] = armor[i].statval[j];
        //         }
        //     }
        // }
    }
}
