var connection;
connection = new signalR.HubConnectionBuilder().withUrl("/ManageLobbyHub").build();
connection.on("SendOwnerToLobby", function (e) {
    console.log("dfkdhf")
    location.href = `Lobby/${e}`
})
connection.on("CheckLobby", function (e) {
    var data = JSON.parse(e)
    console.log(data)
    var elem = `                    <li id="${data.LobbyGuid}">
                        <div class="listLobbyDetail">
                            <p class="listLobbyName">${data.LobbyName}</p>
                            <p class="listLobbyUser">${data.LimitUserCount}/${data.CountUserInLobby} <i class="fa fa-eye"></i></p>
                        </div>
                        <a href="/Lobby/${data.LobbyGuid}" class="listLobbyOpen"><i class="fa fa-door-open"></i></a>
                    </li>`
    var ulLobbies = document.getElementById("ListOfLobbiesInLobbiesPage")
    console.log("ulLobbies")
    ulLobbies.innerHTML += elem
})
connection.on("ConnectedUserGetLobbies", function (e) {
    console.log(e)
    var data = JSON.parse(e)
    console.log(data)
    var ulLobbies = document.getElementById("ListOfLobbiesInLobbiesPage")
    ulLobbies.innerHTML = ""
    for (var i of data) {
        var elem = `                    <li id="${i.LobbyGuid}">
                        <div class="listLobbyDetail">
                            <p class="listLobbyName">${i.LobbyName}</p>
                            <p class="listLobbyUser">${i.LimitUserCount}/${i.CountUserInLobby} <i class="fa fa-eye"></i></p>
                        </div>
                        <a href="/Lobby/${i.LobbyGuid}" class="listLobbyOpen"><i class="fa fa-door-open"></i></a>
                    </li>`
        ulLobbies.innerHTML += elem
    }
})
connection.start();