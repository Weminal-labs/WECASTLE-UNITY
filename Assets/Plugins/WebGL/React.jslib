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
  },
  RequestID : function(json, fakeID){
     try {
      window.dispatchReactUnityEvent("RequestID", UTF8ToString(json), UTF8ToString(fakeID));
    } catch (e) {
      console.warn("Failed to dispatch event");
    }
  },
  SaveMob : function(json){
     try {
      window.dispatchReactUnityEvent("RequestID", UTF8ToString(json));
    } catch (e) {
      console.warn("Failed to dispatch event");
    }
  },
  SaveListMob : function(json){
     try {
      window.dispatchReactUnityEvent("RequestID", UTF8ToString(json));
    } catch (e) {
      console.warn("Failed to dispatch event");
    }
  },
  SavePlayer : function(json, fakeID){
     try {
      window.dispatchReactUnityEvent("RequestID", UTF8ToString(json), UTF8ToString(fakeID));
    } catch (e) {
      console.warn("Failed to dispatch event");
    }
  }
});