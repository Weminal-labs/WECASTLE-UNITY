mergeInto(LibraryManager.library, {
  RequestWeather: function (){
    window.dispatchReactUnityEvent("RequestWeather");
  },
  RequestLogin : function (){
    window.dispatchReactUnityEvent("RequestLogin");
  },
  RequestLogOut : function (){
    window.dispatchReactUnityEvent("RequestLogOut");
  },
  RequestLobby : function(){
    window.dispatchReactUnityEvent("RequestLobby");
  },
  RequestCoin : function(){
    window.dispatchReactUnityEvent("RequestCoin");
  },
  RequestUpdateCoin : function(coin){
    window.dispatchReactUnityEvent("RequestUpdateCoin", coin);
  },
});