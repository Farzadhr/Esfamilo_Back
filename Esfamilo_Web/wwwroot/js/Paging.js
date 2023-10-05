$("#OpenProfileBtn").click(function (e) {
    $("#Home").removeAttr("show")
    $("#Home").slideUp(500);
    $("#ProfilePage").slideDown(500)
    $("#ProfilePage").attr("show", true);
})
$("#OpenHomeBtnInProfile").click(function (e) {
    $("#ProfilePage").removeAttr("show")
    $("#ProfilePage").slideUp(500);
    $("#Home").slideDown(500)
    $("#Home").attr("show", true);
})
$("#BtnOpenUpdataProfileFromProfile").click(function (e) {
    $("#ProfilePage").removeAttr("show")
    $("#ProfilePage").slideUp(500);
    $("#UpdateProfile").slideDown(500)
    $("#UpdateProfile").attr("show", true);
})
$("#BtnCloseProfileFromProfile").click(function (e) {
    $("#UpdateProfile").removeAttr("show")
    $("#UpdateProfile").slideUp(500);
    $("#ProfilePage").slideDown(500)
    $("#ProfilePage").attr("show", true);
})
$("#OpenCreateLobbyPage").click(function (e) {
    $("#Home").removeAttr("show")
    $("#Home").slideUp(500);
    $("#CreateLobby").slideDown(500)
    $("#CreateLobby").attr("show", true);
})
$("#CloseCreateLobbyPage").click(function (e) {
    $("#CreateLobby").removeAttr("show")
    $("#CreateLobby").slideUp(500);
    $("#Home").slideDown(500)
    $("#Home").attr("show", true);
})
$("#OpenLobbiesPage").click(function (e) {
    $("#Home").removeAttr("show")
    $("#Home").slideUp(500);
    $("#Lobbies").slideDown(500)
    $("#Lobbies").attr("show", true);
})
$("#CloseLobbiesPage").click(function (e) {
    $("#Lobbies").removeAttr("show")
    $("#Lobbies").slideUp(500);
    $("#Home").slideDown(500)
    $("#Home").attr("show", true);
})
showandoffpage();
function showandoffpage() {
    var pages = document.getElementsByClassName("Page")
    for (var i of pages) {
        i.style.display = "none"
    }
    if (window.innerWidth > 760) {
        document.getElementById("MobileDivContainer").remove();
        document.querySelector(".Page[show]").style.display = "block"
    } else {
        document.getElementById("DesktopDivContainer").remove();
        document.querySelector(".Page[show]").style.display = "flex"
    }
}