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
  RequestAddress : function (){
    window.dispatchReactUnityEvent("RequestAddress");
  }
});