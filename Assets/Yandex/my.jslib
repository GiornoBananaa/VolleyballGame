mergeInto(LibraryManager.library, {

  StartAd : function(){
    ysdk.adv.showFullscreenAdv()
  },

  RewardedAd : function(){
    ysdk.adv.showRewardedVideo({
      callbacks: {
        onOpen: () => {
          myGameInstance.SendMessage("AudioManager", "Mute");
        },
        onClose: () => {
          myGameInstance.SendMessage("AudioManager", "UnMute");
        },
        onRewarded: () => {
          myGameInstance.SendMessage("SkinSwitcher", "AdReward");
        },
        nError: (e) => {
          console.log('Error while open video ad:', e);
        }
      } 
    });
  }
});