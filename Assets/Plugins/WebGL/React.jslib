mergeInto(LibraryManager.library, {
  PushRewardForPlayer: function (points) {
    const data = { Score: points };
    const event = new CustomEvent("PushRewardForPlayerEvent", { detail: data });
    window.dispatchEvent(event);
  },
});