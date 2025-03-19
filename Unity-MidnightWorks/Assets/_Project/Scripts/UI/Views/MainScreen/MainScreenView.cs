using CityBuilder.City;
using UnityEngine;
using UnityEngine.UI;

namespace CityBuilder.MainScreen
{
    public class MainScreenView : UIView
    {
        [SerializeField] private Button closeButton;

        [SerializeField] private Button loadGameButton;
        [SerializeField] private Button startNewGameButton;

        private CityPresenter _cityPresenter => Dependencies.CityPresenter;
        
        private void Start()
        {
            loadGameButton.interactable = _cityPresenter.HasSavedCity();
        }

        private void OnEnable()
        {
            closeButton.onClick.AddListener(OnExit);
            
            startNewGameButton.onClick.AddListener(StartNewGame);
            loadGameButton.onClick.AddListener(LoadGame);
        }

        private void OnDisable()
        {
            closeButton.onClick.RemoveListener(OnExit);
            
            startNewGameButton.onClick.RemoveListener(StartNewGame);
            loadGameButton.onClick.RemoveListener(LoadGame);
        }

        private void StartNewGame()
            => _cityPresenter.StartNewGame();

        private void LoadGame()
            => _cityPresenter.LoadGame();

        private void OnExit()
            => Application.Quit();
    }
}