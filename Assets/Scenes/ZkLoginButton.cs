/*using AllArt.SUI.Wallets;
using AllArt.SUI.RPC;
using UnityEngine;
using AllArt.SUI.RPC;
using AllArt.SUI.RPC.Filter.Types;
using AllArt.SUI.RPC.Response;

public class ZkLoginButton : MonoBehaviour
{
    private Wallet _wallet;

    void Start()
    {
        // Get the Sui wallet instance
        _wallet = Wallet.;
    }

    public void OnClick()
    {
        // Get the object reference of the zkLogin smart contract
        SuiObjectRef objectRef = ...;

        // Create a Move call transaction to login a user
        SuiMoveCall transaction = new SuiMoveCall
        {
            Module = "zk_login",
            Function = "login",
            Arguments = new object[] { "username", "password" }
        };

        // Execute the Move call transaction
        _wallet.ExecuteMoveCall(objectRef, transaction, (result) =>
        {
            // Handle the result of the Move call transaction
            if (result.IsSuccess)
            {
                // The user was successfully logged in
                Debug.Log("User successfully logged in");
            }
            else
            {
                // The user was not successfully logged in
                Debug.Log("User not successfully logged in");
            }
        });
    }
}*/