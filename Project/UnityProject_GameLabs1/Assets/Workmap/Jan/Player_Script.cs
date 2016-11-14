using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_Script : MonoBehaviour {

    [HideInInspector]
    public bool inCombo;
    private bool cantCombo;
    //private List<KeyCode> curCombo = new List<KeyCode>(); //de combo die je nu probeert uit te voeren //later vervangen door strings, zoals Fire1
    private List<string> curCombo = new List<string>();
    //public KeyCode[] comboKeys; //de keys die je wilt gebruiken om combo's mee uit te voeren
    public string[] comboKeys;
    [Range(0.1f, 1f)]
    public float comboSpeedInput; //de snelheid waarmee je de combo's uitvoert
    private float timer;
    private bool ableToDealDamage;

    public Combat_Script.Combo[] combos;

    private void Start () {
        timer = comboSpeedInput;
	}
	
	private void Update () {
        if (cantCombo == false) {
            CheckCombo();
        }
	}

    private void CheckTimer()
    {
        if (inCombo)
        {
            timer -= Time.deltaTime;
            if (timer <= 0) //als je te laat bent met de combo
            {
                EndCombo();
            }
        }
    }

    private void CheckCombo()
    {
        CheckTimer();
        for (int x = 0; x < comboKeys.Length; x++)
        {
            if (Input.GetButtonDown(comboKeys[x]))
            {
                if (curCombo.Count < 1 || timer > 0)
                {
                    //kijk of curCombo gelijk is aan een bestaande combo
                    for (int y = 0; y < combos.Length; y++)
                    {
                        if (curCombo.Count + 1 <= combos[y].comboString.Length) {
                            if (comboKeys[x] == combos[y].comboString[curCombo.Count] && combos[y].unlocked)
                            {
                                int tempCombo = 0;
                                for (int z = 0; z < curCombo.Count; z++)
                                {
                                    if (curCombo[z] == combos[y].comboString[z])
                                    {
                                        tempCombo++;
                                    }
                                }
                                
                                if (tempCombo == curCombo.Count) {
                                    timer = comboSpeedInput;
                                    curCombo.Add(comboKeys[x]);
                                    inCombo = true;

                                    //hier kijken of de lengte gelijk is van de curcombo en de combo waarmee je vergelijkt, en dan uitvoeren
                                    if (curCombo.Count == combos[y].comboString.Length)
                                    {
                                        cantCombo = true;
                                        UseCombo(y);
                                        curCombo.Clear();
                                    }
                                    return;
                                }
                            }
                        }
                    }
                    EndCombo();
                }
            }
        }
    }

    private void UseCombo(int usingComboNumber) //hier pakt die de zoveelste uit de array, degene die je net uitgevoerd hebt
    {
        int temp = GetComponent<Multiplier_Damage>().CurrentMultiplier;
        Debug.Log(combos[usingComboNumber].name + " " + (combos[usingComboNumber].damage + temp));
        //voer hier de combo ook daadwerkelijk uit

        cantCombo = false;
    }

    public void SwitchDamage() //bepaal de damage momenten in de attacks, roep dit aan via unity animaties
    {
        ableToDealDamage = !ableToDealDamage;
    }

    private void EndCombo()
    {
        inCombo = false;
        curCombo.Clear();
        timer = comboSpeedInput;
    }
}
