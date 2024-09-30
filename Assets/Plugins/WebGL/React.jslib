mergeInto(LibraryManager.library, {
  PushRewardForPlayer : function(number){
     try {
      window.dispatchReactUnityEvent("PushRewardForPlayer", number);
    } catch (e) {
      console.warn("Failed to dispatch event");
    }
  },
});