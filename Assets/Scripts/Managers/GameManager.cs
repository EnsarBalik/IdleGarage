using System;
using System.Collections;
using Cinemachine;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Hypertonic.GridPlacement;
using Hypertonic.GridPlacement.Example.MoveGridDemo;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int coin;
    [SerializeField] private TextMeshProUGUI coinText;
    public GameObject joystick;
    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera buildingCam;
    public GridSettings _gridSettings;
    public GridSaveManager GridSave;

    private void Start()
    {
        instance = this;
    }

    private void LateUpdate()
    {
        coin = PlayerPrefs.GetInt("myCoin", coin);
        coinText.text = "" + coin;

        if (Input.GetKeyDown(KeyCode.N))
        {
            CollectCoin(100);
            coinText.gameObject.transform.DOShakeScale(0.5f, 0.3f)
                .OnComplete(() => coinText.gameObject.transform.localScale = new Vector3(1, 1, 1));
            PlayerPrefs.SetInt("myCoin", coin);
        }
    }

    public void SpendMoney(int spendCost)
    {
        coin -= spendCost;
        StartCoroutine(_coinText());
        PlayerPrefs.SetInt("myCoin", coin);
    }

    public void CollectCoin(int coinCollect)
    {
        coin += coinCollect;
        StartCoroutine(_coinText());
        coinText.gameObject.transform.DOShakeScale(0.5f, 0.3f)
            .OnComplete(() => coinText.gameObject.transform.localScale = new Vector3(1, 1, 1));
        coinText.DOColor(Color.green, 0.2f).OnComplete(() => coinText.DOColor(Color.white, 0.2f));
        PlayerPrefs.SetInt("myCoin", coin);
    }

    private IEnumerator _coinText()
    {
        float totalTime = 2f;
        float elapsedTime = 0;
        int coinAmount = PlayerPrefs.GetInt("myCoin");
        float step = (coin - coinAmount) / totalTime;

        while (elapsedTime < totalTime)
        {
            elapsedTime += Time.deltaTime;
            int amount = Mathf.Clamp(coinAmount + Mathf.RoundToInt(step * elapsedTime), 0, coin);
            coinText.text = amount.ToString();
            yield return null;
        }
    }

    public void StartStopPlacement()
    {
        joystick.SetActive(false);
        buildingCam.gameObject.SetActive(true);
        mainCam.gameObject.SetActive(false);
    }

    public void StopPlacement()
    {
        joystick.SetActive(true);
        buildingCam.gameObject.SetActive(false);
        mainCam.gameObject.SetActive(true);
    }

    public void FindProductForCam()
    {
        StartCoroutine(FindObject());
    }

    private IEnumerator FindObject()
    {
        yield return new WaitForSeconds(0.1f);
        buildingCam.Follow = GameObject.Find("Object Placement Container").transform;
    }
}