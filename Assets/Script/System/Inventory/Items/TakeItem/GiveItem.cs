using UnityEngine;

public class GiveItem : MonoBehaviour
{
    [SerializeField] private GameObject _uiItem;

    private void OnTriggerEnter(Collider other)
    {
        var playerData = other.gameObject.GetComponent<ICharacterData>();
        if (playerData != null)
        {
            if (_uiItem.GetComponent<IUiItem>() != null)
            {
                playerData.Inventory.AddItem(_uiItem);
                GameObject.Destroy(gameObject);
            }
        }
    }

    private void OnValidate()
    {
        if (_uiItem.GetComponent<IUiItem>() == null)
        {
            Debug.LogError($"Not found UiItem {this.name} ");
        }
    }
}
