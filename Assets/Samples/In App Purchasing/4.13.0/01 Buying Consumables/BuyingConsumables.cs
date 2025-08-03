using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.UI;

namespace Samples.Purchasing.Core.BuyingConsumables
{
    public class BuyingConsumables : MonoBehaviour, IDetailedStoreListener
    {
        IStoreController m_StoreController; // The Unity Purchasing system.

        //Your products IDs. They should match the ids of your products in your store.
        public string goldProductId = "com.mycompany.mygame.gold1";
        public string diamondProductId = "com.mycompany.mygame.diamond1";

        public Text GoldCountText;
        public Text DiamondCountText;

        int m_GoldCount;
        int m_DiamondCount;

        void Start()
        {
            InitializePurchasing();// инициализируем Unity Purchasing
            UpdateUI();
        }

        void InitializePurchasing()
        {
            // Создаёт конфигурацию для Unity Purchasing , инициируем его 
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            //Добовляем товары 
            builder.AddProduct(goldProductId, ProductType.Consumable);
            builder.AddProduct(diamondProductId, ProductType.Consumable);
            // устонавливаем в UnityPurchasing нашу созданую конфигурацию
            UnityPurchasing.Initialize(this, builder);
        }

        //Инициализация прошла успешно 
        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            Debug.Log("In-App Purchasing successfully initialized");
            m_StoreController = controller;
        }

        //Инициализация провалилась 
        public void OnInitializeFailed(InitializationFailureReason error)
        {
            OnInitializeFailed(error, null);
        }

        // Вывод кода ошибки в консоль 
        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            var errorMessage = $"Purchasing failed to initialize. Reason: {error}.";

            if (message != null)
            {
                errorMessage += $" More details: {message}";
            }

            Debug.Log(errorMessage);
        }

        #region ButtonMetod

        public void BuyGold()
        {
            m_StoreController.InitiatePurchase(goldProductId);
        }

        public void BuyDiamond()
        {
            m_StoreController.InitiatePurchase(diamondProductId);
        }

        #endregion

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            //Товар куплен
            var product = args.purchasedProduct;

            //Определяем что за товар 
            if (product.definition.id == goldProductId)
            {
                AddGold();
            }
            else if (product.definition.id == diamondProductId)
            {
                AddDiamond();
            }

            Debug.Log($"Purchase Complete - Product: {product.definition.id}");

            //We return Complete, informing IAP that the processing on our side is done and the transaction can be closed.
            return PurchaseProcessingResult.Complete;
        }

        // Покупка провалилась
        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.Log($"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
        {
            Debug.Log($"Purchase failed - Product: '{product.definition.id}'," +
                $" Purchase failure reason: {failureDescription.reason}," +
                $" Purchase failure details: {failureDescription.message}");
        }

        void AddGold()
        {
            m_GoldCount++;
            UpdateUI();
        }

        void AddDiamond()
        {
            m_DiamondCount++;
            UpdateUI();
        }

        void UpdateUI()
        {
            GoldCountText.text = $"Your Gold: {m_GoldCount}";
            DiamondCountText.text = $"Your Diamonds: {m_DiamondCount}";
        }
    }
}
