using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AttackData), typeof(Stats_Player), typeof(Animator))]
public class Combat : MonoBehaviour {

    public enum CharacterStatus {Available, Moving, Comboing, Unavailable }
    public CharacterStatus currentStatus;
    private Animator anim;

    #region ComboData

    #region InspectorSettings

    public string[] comboKeys;
    [Range(0.1f, 1f)]
    public float comboSpeedInput;
    public Combo.combos[] combos;

    #endregion

    private List<string> curCombo = new List<string>();
    private float timer;

    #endregion

    private void Awake()
    {
        anim = GetComponent<Animator>();
        timer = comboSpeedInput;
    }

    private void Update()
    {
        if(currentStatus == CharacterStatus.Available)
            CheckCombo();
    }

    #region ComboFunctions

    private void CheckCombo()
    {
        CheckTimer();
        for (int x = 0; x < comboKeys.Length; x++)
            if (Input.GetButtonDown(comboKeys[x]))
                if (curCombo.Count < 1 || timer > 0)
                {
                    for (int y = 0; y < combos.Length; y++)
                        if (curCombo.Count + 1 <= combos[y].comboString.Length)
                            if (comboKeys[x] == combos[y].comboString[curCombo.Count] && combos[y].unlocked)
                            {
                                int tempCombo = 0;
                                for (int z = 0; z < curCombo.Count; z++)
                                    if (curCombo[z] == combos[y].comboString[z])
                                        tempCombo++;
                                if (tempCombo == curCombo.Count)
                                {
                                    timer = comboSpeedInput;
                                    curCombo.Add(comboKeys[x]);
                                    currentStatus = CharacterStatus.Available;
                                    if (curCombo.Count == combos[y].comboString.Length)
                                    {
                                        currentStatus = CharacterStatus.Comboing;
                                        UseCombo(y);
                                        curCombo.Clear();
                                    }
                                    return;
                                }
                            }
                    EndCombo();
                }
    }

    private void CheckTimer()
    {
        if (currentStatus == CharacterStatus.Comboing)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
                EndCombo();
        }
    }

    #region InCombo

    public int currentCombo = 0;

    private void UseCombo(int usingComboNumber)
    {
        Debug.Log(combos[usingComboNumber].name + " " + (combos[usingComboNumber].damage));
        currentStatus = CharacterStatus.Comboing;
        currentCombo = usingComboNumber;
        anim.SetTrigger(combos[currentCombo].name);
    }

    private void EndCombo()
    {
        curCombo.Clear();
        timer = comboSpeedInput;
        currentStatus = CharacterStatus.Available;
    }

    #endregion

    #endregion
}
