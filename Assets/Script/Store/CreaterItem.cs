using UnityEngine;

public class CreaterItem : MonoBehaviour
{
    [SerializeField] private PlayerCharecter playerCharecter;
    [SerializeField] private GameObject _potionHealth;
    [SerializeField] private GameObject _potionExp;

    private ICharacterData _characterData;

    private void Start()
    {
        if (playerCharecter == null)
        {
            playerCharecter = FindFirstObjectByType<PlayerCharecter>();
        }

        if (playerCharecter == null)
        {
            Debug.LogError("Not Found playerCharecter in CreaterItem");
            return;
        }
        _characterData = playerCharecter.CharacterData;
    }

    public void Create(Item item)
    {
        if (item == Item.PotionExp)
        {
            _characterData.Inventory.AddItem(_potionExp);

        }
        else if (item == Item.PotionHealth)
        {
            _characterData.Inventory.AddItem(_potionHealth);
        }
    }
}
