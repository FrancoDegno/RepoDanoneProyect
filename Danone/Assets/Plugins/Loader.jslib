mergeInto(LibraryManager.library, {
CheckDevice: function(){
	
	if (UnityLoader.SystemInfo.mobile) {
		return true;
	}
	else
	return false;
},

});