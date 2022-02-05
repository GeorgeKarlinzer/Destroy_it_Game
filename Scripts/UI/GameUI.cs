using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Color normHeartColor;
    [SerializeField] private Color darkHearkColor;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Image progressBar;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI incomeText;
    [SerializeField] private Sprite nextLevelSprite;
    [SerializeField] private Sprite repeatLevelSprite;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button backToMenuButton;
    [SerializeField] private UIElements gameUI;
    [SerializeField] private UIElements endGameUI;

    private LevelInteractor levelInteractor;
    private PlayerInteractor playerInteractor;


    private void Start()
    {
        levelInteractor = Game.GetInteractor<LevelInteractor>();
        playerInteractor = Game.GetInteractor<PlayerInteractor>();

        levelInteractor.AddActionToOnCurrentDifficultyChangeEvent(OnDifficultyChanged);

        // On level start
        levelInteractor.AddActionToOnLevelStartEvent(() => { 
            gameUI.Show();
            var lvl = 1 + levelInteractor.CurrentLvl;
            levelText.text = $"{lvl}";
            nextLevelButton.GetComponent<Image>().sprite = nextLevelSprite;
            progressBar.fillAmount = 0;
        });

        // On lose
        levelInteractor.AddActionToOnLoseEvent(() => nextLevelButton.GetComponent<Image>().sprite = repeatLevelSprite);

        // On Level end
        levelInteractor.AddActionToOnLevelEndEvent(x => gameUI.Hide());

        levelInteractor.AddActionToOnLevelEndEvent(x => endGameUI.Show());
        
        levelInteractor.AddActionToOnLevelEndEvent(x => incomeText.text = $"{x}");


        playerInteractor.AddActionToOnHealthChangeEvent(OnHealthChanged);

        backToMenuButton.onClick.AddListener(() => endGameUI.Hide());

        nextLevelButton.onClick.AddListener(() => levelInteractor.StartLevel());
        nextLevelButton.onClick.AddListener(() => endGameUI.Hide());
    }

    public void OnHealthChanged(int value)
    {
        for(var i = 0; i < hearts.Length; i++)
            hearts[i].color = i < value ? normHeartColor : darkHearkColor;
    }

    public void OnDifficultyChanged(int currentV, int defaultV)
    {
        progressBar.fillAmount = 1 - (float)currentV / defaultV;
    }
}
