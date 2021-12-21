$(document).ready(function () {
    var ParentMenu = document.getElementById(localStorage.getItem("ParentMenu"));
    if (ParentMenu != null) {
        ParentMenu.className = "w3-bar-block w3-hide w3-padding-large w3-medium w3-show";
        var MainMenu = document.getElementById(localStorage.getItem("MainMenu"));
        if (MainMenu != null) {
            MainMenu.className = "w3-bar-block w3-hide w3-padding-large w3-medium w3-show";
        }
    }

    $('#lblProjectTitle').text(localStorage.getItem("File"));
    window.localStorage.removeItem("ParentMenu");
    window.localStorage.removeItem("MainMenu");
});

