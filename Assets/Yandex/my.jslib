mergeInto(LibraryManager.library, {

  StartAd : function(){
    ysdk.adv.showFullscreenAdv({
      callbacks: {
        onOpen: () => {
          myGameInstance.SendMessage("AudioManager", "Mute");
        },
        onClose: () => {
          myGameInstance.SendMessage("AudioManager", "UnMute");
        },
        onError: (e) => {
          myGameInstance.SendMessage("AudioManager", "UnMute");
          console.log('Error while open video ad:', e);
        }
      } 
    })
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
          myGameInstance.SendMessage("AudioManager", "UnMute");
        },
        onError: (e) => {
          myGameInstance.SendMessage("AudioManager", "UnMute");
          console.log('Error while open video ad:', e);
        }
      } 
    })
  }
});