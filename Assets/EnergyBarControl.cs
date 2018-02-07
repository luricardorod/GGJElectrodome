using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBarControl : MonoBehaviour
{
    public Text[] PowerNames;
    public Image[] PowerImages;
    public Text MoveSet;

    public void SetEnergy(Color color, float Energy)
    {
        int PowersActive = Mathf.FloorToInt(Energy * 4.0f + .05f);

        int i = 0;

        //Poderes Activados
        for (; i < PowersActive; ++i)
        {
            PowerImages[i].enabled = true;
            PowerNames[i].color = color;

            if (i != 3)
                PowerImages[i].GetComponent<RectTransform>().localScale.Set(.2f, .2f, 1.0f);
            else
                PowerImages[i].GetComponent<RectTransform>().localScale.Set(.15f, .15f, 1.0f);
        }

        //Poderes no activados
        for (; i < 4; ++i)
        {
            PowerImages[i].enabled = false;

            PowerImages[i].color = Color.white;

            if (i != 3)
                PowerImages[i].GetComponent<RectTransform>().localScale.Set(.15f, .15f, 1.0f);
            else
                PowerImages[i].GetComponent<RectTransform>().localScale.Set(.1f, .1f, 1.0f);

            PowerNames[i].color = Color.white;
        }
    }

    public void SetMoveSet(PowersAdmin.MovSet moveSet)
    {
        switch (moveSet)
        {
            case PowersAdmin.MovSet.Defensive:
                PowerNames[0].text = "Dash";
                PowerNames[1].text = "/Slide";
                PowerNames[2].text = "/Barrier";
                PowerNames[3].text = "/Bomb";
                MoveSet.text = "Defensive";
                break;

            case PowersAdmin.MovSet.Offensive:
                PowerNames[0].text = "Parry";
                PowerNames[1].text = "/Stun";
                PowerNames[2].text = "/Chain";
                PowerNames[3].text = "/Ultra";
                MoveSet.text = "Offensive";
                break;
        }
    }

}
