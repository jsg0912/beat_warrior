using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpUI : MonoBehaviour
{
    public static HpUI Instance;

    [SerializeField] private GameObject HP;
    private GameObject HPPrefab;
    private List<Image> HPList;

    private void Awake()
    {
        Instance = this;

        HPPrefab = Resources.Load("Prefab/PlayerHP") as GameObject;
        HPList = new();
    }

    public void SetAndUpdateHPUI(int hp)
    {
        SetHPUI(hp);
        UpdateHPUI();
    }

    public void SetHPUI(int hp)
    {
        HPList.Clear();
        foreach (Transform child in HP.GetComponentInChildren<Transform>())
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < hp; i++)
        {
            GameObject hpui = Instantiate(HPPrefab);
            hpui.transform.SetParent(HP.transform, false);
            HPList.Add(hpui.transform.GetChild(0).GetComponent<Image>());
        }
    }

    public void UpdateHPUI()
    {
        int hp = Player.Instance.GetCurrentStat(StatKind.HP);

        if (hp == 0)
        {
            foreach (Image image in HPList) image.gameObject.SetActive(true);
            return;
        }

        for (int i = 0; i < Player.Instance.GetFinalStat(StatKind.HP); i++)
        {
            if (i < hp) HPList[i].gameObject.SetActive(false);
            else HPList[i].gameObject.SetActive(true);
        }
    }
}