using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI crystalsText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI SawDamageText;
    [SerializeField] private TextMeshProUGUI SpikesSpeenText;
    [SerializeField] private TextMeshProUGUI IncomeText;
    [SerializeField] private TextMeshProUGUI SawPriceText;
    [SerializeField] private TextMeshProUGUI SpikesPriceText;
    [SerializeField] private TextMeshProUGUI IncomePriceText;

    [SerializeField] private Button BuySawButton;
    [SerializeField] private Button BuySpikesButton;
    [SerializeField] private Button BuyIncomeButton;

    private CoinsInteractor coinsInteractor;
    private CrystalsInteractor crystalsInteractor;
    private LevelInteractor levelInteractor;
    private PlayerInteractor playerInteractor;
    private UpgradesInteractor upgradesInteractor;


    private void Start()
    {
        coinsInteractor = Game.GetInteractor<CoinsInteractor>();
        crystalsInteractor = Game.GetInteractor<CrystalsInteractor>();
        levelInteractor = Game.GetInteractor<LevelInteractor>();
        playerInteractor = Game.GetInteractor<PlayerInteractor>();
        upgradesInteractor = Game.GetInteractor<UpgradesInteractor>();


        coinsInteractor.AddActionToOnCoinsChangeEvent(
            x => OnLongValueChanged(coinsText, x));

        crystalsInteractor.AddListenerToOnCrystalsChangeEvent(
            x => OnLongValueChanged(crystalsText, x));

        levelInteractor.AddActionToOnLevelEndEvent(
            x => OnValueChanged(levelText, levelInteractor.CurrentLvl + 1));

        playerInteractor.AddActionToOnSawDamageChangeEvent(
            x => OnValueChanged(SawDamageText, x));

        playerInteractor.AddActionToOnSpikeFrequencyChangeEvent(
            x => OnPercentChanged(SpikesSpeenText, x - 1));

        playerInteractor.AddActionToOnIncomeFactorChangeEvent(
            x => OnPercentChanged(IncomeText, x));

        upgradesInteractor.AddActionToOnSawPricesChangedEvent(
            x => OnLongValueChanged(SawPriceText, x));

        upgradesInteractor.AddActionToOnSpikesPricesChangedEvent(
            x => OnLongValueChanged(SpikesPriceText, x));

        upgradesInteractor.AddActionToOnIncomePricesChangedEvent(
            x => OnLongValueChanged(IncomePriceText, x));

        coinsInteractor.AddActionToOnCoinsChangeEvent(
            x => BuySawButton.interactable = x >= upgradesInteractor.SawPrice);

        coinsInteractor.AddActionToOnCoinsChangeEvent(
            x => BuySpikesButton.interactable = x >= upgradesInteractor.SpikesPrice);

        coinsInteractor.AddActionToOnCoinsChangeEvent(
            x => BuyIncomeButton.interactable = x >= upgradesInteractor.IncomePrice);
    }



    public void OnLongValueChanged(TextMeshProUGUI text, int value)
    {
        text.text = value.ToShortString();
    }

    public void OnValueChanged(TextMeshProUGUI text, int value)
    {
        text.text = value.ToString();
    }

    public void OnPercentChanged(TextMeshProUGUI text, float value)
    {
        text.text = $"{(int)(value * 100)}%";
    }
}
