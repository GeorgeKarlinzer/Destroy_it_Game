using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button backToMenuButton;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private UIElements uIElements;

    UpgradesInteractor upgradesInteractor;
    CoinsInteractor coinsInteractor;
    PlayerInteractor playerInteractor;
    LevelInteractor levelInteractor;


    private void Start()
    {
        upgradesInteractor = Game.GetInteractor<UpgradesInteractor>();
        coinsInteractor = Game.GetInteractor<CoinsInteractor>();
        playerInteractor = Game.GetInteractor<PlayerInteractor>();
        levelInteractor = Game.GetInteractor<LevelInteractor>();

        levelText.text = $"{1 + levelInteractor.CurrentLvl}";
        backToMenuButton.onClick.AddListener(() => uIElements.Show());
    }

    public void BuySawDamage()
    {
        var price = upgradesInteractor.SawPrice;
        upgradesInteractor.SawPrice = (int)(price * UpgradesRepository.PRICE_FACTOR);
        coinsInteractor.Coins -= price;
        playerInteractor.SawDamage += UpgradesRepository.SawDamagePerLvl;
    }

    public void BuySpikesSpeed()
    {
        var price = upgradesInteractor.SpikesPrice;
        upgradesInteractor.SpikesPrice = (int)(price * UpgradesRepository.PRICE_FACTOR);
        coinsInteractor.Coins -= price;
        playerInteractor.SpikeFrequency += UpgradesRepository.SpikesSpeedPerLvl;
    }

    public void BuyIncomeFactor()
    {
        var price = upgradesInteractor.IncomePrice;
        upgradesInteractor.IncomePrice = (int)(price * UpgradesRepository.PRICE_FACTOR);
        coinsInteractor.Coins -= price;
        playerInteractor.IncomeFactor += UpgradesRepository.IncomeFactorPerLvl;
    }

    public void StartLevelClick()
    {
        uIElements.Hide();
        levelInteractor.StartLevel();
    }
}
