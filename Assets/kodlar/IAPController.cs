using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using System;
using UnityEngine.UI;

public class IAPController : MonoBehaviour,IStoreListener
{
    IStoreController controller;

    public string[] product;

    public Text coinText;

    public bool delete = true;

    public Text IAP1Text, IAP2Text,IAP3Text;

    private void Start()
    {
        if (delete)
            PlayerPrefs.DeleteAll();
        if (PlayerPrefs.GetInt("RemoveAds") == 1)
        {
            GameObject.Find("Remove").SetActive(false);
            GameObject.Find("RemoveButton").GetComponent<Button>().interactable = false;
        }
        IAPStart();
    }

    private void IAPStart()
    {
        var module = StandardPurchasingModule.Instance();
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(module);

        foreach (string item in product)
        {
            builder.AddProduct(item,ProductType.Consumable);
        }
        UnityPurchasing.Initialize(this,builder);
        IAP1Text = GameObject.Find("IAP1Text").GetComponent<Text>();
        IAP1Text.text = controller.products.WithID("coin_100").metadata.localizedPrice.ToString();
        IAP2Text = GameObject.Find("IAP2Text").GetComponent<Text>();
        IAP2Text.text = controller.products.WithID("coin_200").metadata.localizedPrice.ToString();
        IAP3Text = GameObject.Find("IAP3Text").GetComponent<Text>();
        IAP3Text.text = controller.products.WithID("removeAds_1").metadata.localizedPrice.ToString();
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        this.controller = controller;
    }
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("Error" + error.ToString());
    }
    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
        Debug.Log("Error while buying" + p.ToString());
    }
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        if (string.Equals(e.purchasedProduct.definition.id, product[0], StringComparison.Ordinal))
        {
            AddCoin(100);
            return PurchaseProcessingResult.Complete;
        }
        else if (string.Equals(e.purchasedProduct.definition.id, product[1], StringComparison.Ordinal))
        {
            AddCoin(200);
            return PurchaseProcessingResult.Complete;
        }
        else if (string.Equals(e.purchasedProduct.definition.id, product[2], StringComparison.Ordinal))
        {
            RemoveAds();
            return PurchaseProcessingResult.Complete;
        }
        else
        {
            return PurchaseProcessingResult.Pending;
        }

    }
    public void AddCoin(int coin)
    {
        coinText.text = coin.ToString() + " satın aldı";
    }
    private void RemoveAds()
    {
        PlayerPrefs.SetInt("RemoveAds", 1);
        GameObject.Find("Remove").SetActive(false);
        GameObject.Find("RemoveButton").GetComponent<Button>().interactable = false;
    }
    public void IAPButton(string id)
    {
        Product proc = controller.products.WithID(id);
        if(proc != null && proc.availableToPurchase)
        {
            print("Buying");
            controller.InitiatePurchase(proc);
        }
        else
        {
            print("not buying");
        }
    }

}
