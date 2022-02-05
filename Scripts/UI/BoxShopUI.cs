using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BoxShopUI : MonoBehaviour
{
    [SerializeField] private Sprite selectedSprite;
    [SerializeField] private Sprite unselectedSprite;

    [SerializeField] private GameObject[] prices;
    [SerializeField] private GameObject[] boxBlocks;
    [SerializeField] private GameObject buyPanel;

    private BoxShopInteractor boxInteractor;


    private void Start()
    {
        boxInteractor = Game.GetInteractor<BoxShopInteractor>();

        for (int i = 0; i < boxBlocks.Length; i++)
        {
            int a = i;
            var blockButton = boxBlocks[i].GetComponent<Button>();
            var boxButton = boxBlocks[i].transform.parent.GetChild(0).GetComponent<Button>();
            boxButton.onClick.AddListener(() => OnClickOnBox(a));
            boxButton.onClick.AddListener(() => boxButton.GetComponent<Animator>().SetTrigger("start"));
            blockButton.onClick.AddListener(() => OnClickOnBlock(a));
        }

        boxInteractor.AddListenerToOnBuyEvent(OnBuy);
        OnClickOnBox(boxInteractor.CurrentBox);
    }

    public void OnClickOnBlock(int index)
    {
        buyPanel.SetActive(true);
        var boxSprite = buyPanel.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>();
        var blockBoxSprite = buyPanel.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<Image>();
        var buyButton = buyPanel.transform.GetChild(1).GetChild(2).GetComponent<Button>();

        boxSprite.sprite = boxBlocks[index].transform.parent.GetChild(0).GetComponent<Image>().sprite;
        blockBoxSprite.sprite = boxBlocks[index].GetComponent<Image>().sprite;

        buyButton.interactable = boxInteractor.GetPrice(index) <= Game.GetInteractor<CrystalsInteractor>().Crystals;

        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => boxInteractor.BuyBox(index));
        buyButton.onClick.AddListener(() => buyPanel.SetActive(false));
    }

    public void OnBuy(int index)
    {
        boxBlocks[index].SetActive(false);
        prices[index].SetActive(false);
    }

    public void OnClickOnBox(int index)
    {
        boxBlocks[boxInteractor.CurrentBox].transform.parent.GetComponent<Image>().sprite = unselectedSprite;
        boxBlocks[index].transform.parent.GetComponent<Image>().sprite = selectedSprite;
        boxInteractor.SetBox(index);
    }
}
