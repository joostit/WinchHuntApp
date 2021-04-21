window.initMapsScript = function (apiKey) {
    console.log("Window.InitMapsScript called");



	let scriptsIncluded = false;
	let scriptTags = document.querySelectorAll('head > script');
	scriptTags.forEach(scriptTag => {
		if (scriptTag.getAttribute('src').startsWith("https://maps.googleapis.com/maps/api/js?key=")) {
			scriptsIncluded = true;
			console.log("Google maps API script already included");
			return;
		}
	});

	console.log("Injecting Google Maps API script to <head>");

	let src = "https://maps.googleapis.com/maps/api/js?key=" + apiKey + "&v=3";

	console.log("Injecting src: " + src);

	let importedMaps = document.createElement('script');
	importedMaps.src = src;
	importedMaps.defer = true;
	document.head.appendChild(importedMaps);

};
