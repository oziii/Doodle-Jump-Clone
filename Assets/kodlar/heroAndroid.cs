using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heroAndroid : MonoBehaviour
{
    Doodler dr;
    void Start()
    {
        dr = GetComponent<Doodler>();
    }

    void Update()
    {
        
    }
    public void sol()
    {
        dr.sol = true;

    }
    public void sag()
    {
        dr.sag = true;
    }
    public void solCik()
    {
        dr.sol = false;
    }
    public void sagCik()
    {
        dr.sag = false;
    }
}
