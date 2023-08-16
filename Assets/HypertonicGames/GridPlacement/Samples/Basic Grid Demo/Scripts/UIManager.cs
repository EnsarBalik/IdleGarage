using Hypertonic.GridPlacement.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Hypertonic.GridPlacement.Example.BasicDemo
{
    /// <summary>
    /// This UIManager for the sample scene is responsible for displaying and hiding the different UI in the scene.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private Button _addAnObjectButton;

        [SerializeField]
        private GameObject _objectSelectionUI;

        [SerializeField]
        private GameObject _gridObjectAlignmentControls;

        [SerializeField]
        private GameObject _gridControls;

        private void Start()
        {
            _addAnObjectButton.onClick.AddListener(HandleAddAnObjectButtonPressed);

            Button_GridObjectSelectionOption.OnOptionSelected += HandleGridObjectOptionSelected;
            Button_CloseObjectSelection.OnCloseButtonPressed += HandleCloseObjectSelectionPressed;
            Button_OpenChangeAlignmentOptions.OnOpenChangeAlignmentOptionEvent += HandleOpenChangeAlignmentPressed;
            Button_ChangeAlignment.OnChangeAlignmentPressed += HandleChangeAlignmentPressed;
            Button_CancelPlacement.OnCancelPlacementPressed += HandleCancelPlacementPressed;
            GridControlManager.OnObjectPlacedOnGrid += HandleObjectPlacedOnGrid;
            ExampleGridObject.OnObjectSelected += HandleExampleGridObjectSelected;
        }

        private void OnDestroy()
        {
            _addAnObjectButton.onClick.RemoveListener(HandleAddAnObjectButtonPressed);

            Button_GridObjectSelectionOption.OnOptionSelected -= HandleGridObjectOptionSelected;
            Button_CloseObjectSelection.OnCloseButtonPressed -= HandleCloseObjectSelectionPressed;
            Button_OpenChangeAlignmentOptions.OnOpenChangeAlignmentOptionEvent -= HandleOpenChangeAlignmentPressed;
            Button_ChangeAlignment.OnChangeAlignmentPressed -= HandleChangeAlignmentPressed;
            Button_CancelPlacement.OnCancelPlacementPressed -= HandleCancelPlacementPressed;
            GridControlManager.OnObjectPlacedOnGrid -= HandleObjectPlacedOnGrid;
            ExampleGridObject.OnObjectSelected -= HandleExampleGridObjectSelected;           
        }

        private void HandleGridObjectOptionSelected(GameObject gridObject)
        {
            _addAnObjectButton.gameObject.SetActive(false);
            _objectSelectionUI.SetActive(false);
            _gridControls.SetActive(true);
        }

        private void HandleAddAnObjectButtonPressed()
        {
            _addAnObjectButton.gameObject.SetActive(false);
            _objectSelectionUI.SetActive(true);
        }

        private void HandleCloseObjectSelectionPressed()
        {
            _objectSelectionUI.SetActive(false);
        }

        private void HandleOpenChangeAlignmentPressed()
        {
            _gridObjectAlignmentControls.SetActive(true);
        }

        private void HandleCancelPlacementPressed()
        {
            _gridControls.SetActive(false);
        }

        private void HandleChangeAlignmentPressed(ObjectAlignment objectAlignment)
        {
            _gridObjectAlignmentControls.SetActive(false);
        }

        private void HandleObjectPlacedOnGrid()
        {
            GridManagerAccessor.GridManager.OnGridObjectDeleted -= HandleGridObjectDeleted;
            _addAnObjectButton.gameObject.SetActive(true);
            _gridControls.SetActive(false);
        }

        private void HandleExampleGridObjectSelected(GameObject gridObject)
        {
            GridManagerAccessor.GridManager.OnGridObjectDeleted += HandleGridObjectDeleted;
            _addAnObjectButton.gameObject.SetActive(false);
            _gridControls.SetActive(true);
        }

        private void HandleGridObjectDeleted()
        {
            GridManagerAccessor.GridManager.OnGridObjectDeleted -= HandleGridObjectDeleted;
            _addAnObjectButton.gameObject.SetActive(true);
            _gridControls.SetActive(false);
        }
    }
}