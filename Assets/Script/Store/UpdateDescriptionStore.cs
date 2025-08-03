using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class UpdateDescriptionStore : MonoBehaviour
{
    [SerializeField] private List<ViewItem> viewItem;
    [SerializeField] private CreaterItem _creater;

    private void Start()
    {
        foreach (ViewItem item in viewItem)
        {
            if (!CheckView(item))
            {
                viewItem.Remove(item);
            }
        }

        if (_creater == null)
        {
            _creater = GetComponent<CreaterItem>();
        }

        if (_creater == null)
        {
            Debug.LogError("Not Found playerCharecter in CreaterItem");
            return;
        }
    }

    public void OnPurchaseComplete(Product product)
    {
        foreach (ViewItem item in viewItem)
        {
            if (!CheckView(item)) continue;

            if (item.ID == product.definition.id)
            {
                AddItem(item.item);
            }
        }
    }

    public void OnPurchaseFalled(Product product, PurchaseFailureDescription description)
    {
        Debug.Log($"Purchase failed - Product: '{product.definition.id}'," +
               $" Purchase failure reason: {description.reason}," +
               $" Purchase failure details: {description.message}");
    }

    public void OnProductFetched(Product product)
    {
        foreach (ViewItem item in viewItem)
        {
            if (!CheckView(item)) continue;

            if (item.ID == product.definition.id)
            {
                Show(product, item);
            }
        }
    }

    private void AddItem(Item item)
    {
        switch (item)
        {
            case Item.None:

                break;

            case Item.PotionHealth:

                _creater.Create(item);
                break;

            case Item.PotionExp:

                _creater.Create(item);
                break;

            default:
                break;
        }
    }

    private bool CheckView(ViewItem item)
    {
        if (item._nameItem == null)
        {
            Debug.LogError("Not Found Text NameItem");
            return false;
        }
        if (item._priceItem == null)
        {
            Debug.LogError("Not Found Text PriceItem");
            return false;
        }
        if (item._description == null)
        {
            Debug.LogError("Not Found Text Description");
            return false;
        }
        if (item.ID == null)
        {
            Debug.LogError("Not Found Text IDItem");
            return false;
        }

        return true;
    }

    private void Show(Product product, ViewItem item)
    {
        ProductMetadata metadata = product.metadata;
        item._priceItem.SetText(metadata.localizedPrice.ToString());
        item._nameItem.SetText(metadata.localizedTitle);
        item._description.SetText(metadata.localizedDescription);
    }
}

[Serializable]
public struct ViewItem
{
    public TextMeshProUGUI _nameItem;
    public TextMeshProUGUI _priceItem;
    public TextMeshProUGUI _description;
    public Item item;
    public string ID;

}

public enum Item
{
    None,
    PotionHealth,
    PotionExp
}
