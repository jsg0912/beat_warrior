using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpUI : MonoBehaviour
{
    public static PlayerHpUI Instance;

    [SerializeField] private GameObject HP;
    public GameObject HPPrefab;
    private List<Image> HPList = new();

    private void Awake()
    {
        Instance = this;
        //HPPrefab = Resources.Load("Prefab/03_UI/03_PlayerUI/PlayerHP") as GameObject;
    }

    private void Start()
    {
        Initialize();
    }

    public void CreateAndUpdateHPUI(int hp)
    {
        CreateHPUI(hp);
        UpdateHPUI();
    }

    public void CreateHPUI(int hp)
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
        int maxHP = Player.Instance.GetFinalStat(StatKind.HP);
        int currentHP = Player.Instance.GetCurrentStat(StatKind.HP);

        if (HPList.Count != maxHP)
        {
            Initialize();
            return;
        }

        for (int i = 0; i < maxHP; i++)
        {
            Util.SetActive(HPList[i].gameObject, i >= currentHP);
        }
    }

    public void Initialize()
    {
        CreateAndUpdateHPUI(Player.Instance.GetFinalStat(StatKind.HP));
    }
}