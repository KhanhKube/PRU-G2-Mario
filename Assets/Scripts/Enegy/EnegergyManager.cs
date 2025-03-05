using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnegergyManager : MonoBehaviour
{
    public int maxEnergy = 100;
    public int currentEnergy;

    public Enegy engergyBar;

    private void Start()
    {
        currentEnergy = maxEnergy;
        engergyBar.SetMaxEnergy(maxEnergy);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            TakeEnergy(20);
        }
    }

    public void TakeEnergy(int decrease)
    {
        currentEnergy -= decrease;
        engergyBar.SetCurrentEnergy(currentEnergy);
    }
}
