using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //1
    public static UIManager Instance;
    //2
    public GameObject addTowerWindow;
    //3
    public Text txtGold;
    public Text txtWave;
    public Text txtEscapedEnemies;

    public Transform enemyHealthBars;
    public GameObject enemyHealthBarPrefab;

    public GameObject towerInfoWindow;

    public GameObject winGameWindow;
    public GameObject loseGameWindow;
    public GameObject blackBackground;
    public GameObject centerWindow;
    public GameObject damageCanvas;

    private Scene scene;

        public static float vrUiScaleDivider = 12f;
    //1
    void Awake()
    {
        Instance = this;
        scene = SceneManager.GetActiveScene();
    }
    //2
    private void UpdateTopBar()
    {
        txtGold.text = GameManager.Instance.gold.ToString();
        txtWave.text = "Wave " + GameManager.Instance.waveNumber + " / " + WaveManager.Instance.enemyWaves.Count;
        txtEscapedEnemies.text = "Escaped Enemies " + GameManager.Instance.escapedEnemies + " / " + GameManager.Instance.maxAllowedEscapedEnemies;
    }
    //3
    public void ShowAddTowerWindow(GameObject towerSlot)
    {
        if (GameManager.Instance.gameOver)
        {
            return;
        }

        addTowerWindow.SetActive(true);
        addTowerWindow.GetComponent<AddTowerWindow>().
        towerSlotToAddTowerTo = towerSlot;

       // if (scene.name == "GameVR")
            UtilityMethods.MoveUiElementToWorldPosition(addTowerWindow.GetComponent<RectTransform>(), towerSlot.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTopBar();
    }

    public void ShowTowerInfoWindow(Tower tower)
    {
        if (GameManager.Instance.gameOver)
        {
            return;
        }

        towerInfoWindow.GetComponent<TowerInfoWindow>().tower = tower;
        towerInfoWindow.SetActive(true);
        UtilityMethods.MoveUiElementToWorldPosition(towerInfoWindow.
        GetComponent<RectTransform>(), tower.transform.position);

        //if (scene.name == "GameVR")
            UtilityMethods.MoveUiElementToWorldPosition(towerInfoWindow.GetComponent<RectTransform>(), tower.transform.position);
    }

    public void ShowWinScreen()
    {
        //blackBackground.SetActive(true);
        winGameWindow.SetActive(true);
    }

    public void ShowLoseScreen()
    {
        //blackBackground.SetActive(true);
        loseGameWindow.SetActive(true);
    }

    public void CreateHealthBarForEnemy(Enemy enemy)
    {
        GameObject healthBar = Instantiate(enemyHealthBarPrefab);
        healthBar.transform.SetParent(enemyHealthBars, false);
        healthBar.GetComponent<EnemyHealthBar>().enemy = enemy;
    }

    public void ShowCenterWindow(string text)
    {
        centerWindow.transform.Find("TxtWave").GetComponent<Text>().text = text;
        StartCoroutine(EnableAndDisableCenterWindow());
    }

    private IEnumerator EnableAndDisableCenterWindow()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(.4f); centerWindow.SetActive(true);
            yield return new WaitForSeconds(.4f); centerWindow.SetActive(false);
        }
    }

    //1
    public void ShowDamage()
    {
        StartCoroutine(DoDamageAnimation());
    }
    //2
    private IEnumerator DoDamageAnimation()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(.1f);
            damageCanvas.SetActive(true);
            yield return new WaitForSeconds(.1f);
            damageCanvas.SetActive(false);
        }
    }

}
