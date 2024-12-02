mergeInto(LibraryManager.library, {
  GameCanPlay: function(){
    try {
      const event = new CustomEvent("GameCanPlay");
      window.dispatchEvent(event);
    } catch (e) {
      console.warn("Failed to dispatch event");
    }
  },
  PushRewardForPlayer: function (points) {
    const data = { Score: points };
    const event = new CustomEvent("PushRewardForPlayerEvent", { detail: data });
    window.dispatchEvent(event);
  },
});