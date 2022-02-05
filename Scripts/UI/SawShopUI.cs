using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SawShopUI : MonoBehaviour
{
    [SerializeField] private Sprite selectedSprite;
    [SerializeField] private Sprite unselectedSprite;

    [SerializeField] private GameObject[] sawMain;
    [SerializeField] private GameObject description;

    private AchievementInteractor achievementInteractor;
    private SawShopInteractor sawInteractor;


    private void Start()
    {
        sawInteractor = Game.GetInteractor<SawShopInteractor>();
        achievementInteractor = Game.GetInteractor<AchievementInteractor>();

        achievementInteractor.AddEventToOnAchieveDone(OnOpen);

        for (int i = 0; i < sawMain.Length; i++)
        {
            var a = i;
            sawMain[i].transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() => OnClickOnBlock(a));
            sawMain[i].transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => OnClickOnSaw(a));
        }

        OnClickOnSaw(sawInteractor.CurrentIndex);
    }

    public void OnClickOnBlock(int index)
    {
        description.gameObject.SetActive(true);
        description.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = achievementInteractor.GetDescription(index);
    }

    public void OnClickOnSaw(int index)
    {
        sawMain[sawInteractor.CurrentIndex].GetComponent<Image>().sprite = unselectedSprite;
        sawMain[index].GetComponent<Image>().sprite = selectedSprite;
        sawInteractor.SetSaw(index);
    }

    public void OnOpen(int index)
    {
        sawMain[index].transform.GetChild(0).gameObject.SetActive(true);
        sawMain[index].transform.GetChild(1).gameObject.SetActive(false);
    }
}
