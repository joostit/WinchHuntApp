window.getDimensions = function () {
    return {
        width: window.innerWidth,
        height: window.innerHeight
    };
};

window.getInnerDimensionsById = function (id) {
    let element = document.getElementById(id);
    return {
        width: element.clientWidth,
        heigth: element.clientHeight
    };
};


window.browserResize = {
    getInnerHeight: function () {
        return window.innerHeight;
    },

    getInnerWidth: function () {
        return window.innerWidth;
    },

    registerResizeCallback: function () {
        window.addEventListener("resize", browserResize.resized);
    },

    resized: function () {
        DotNet.invokeMethodAsync("WinchHuntApp.Client", 'OnBrowserResize').then(data => data);
    }
}

