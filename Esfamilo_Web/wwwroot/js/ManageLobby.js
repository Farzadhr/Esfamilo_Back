var connection;
connection = new signalR.HubConnectionBuilder().withUrl("/ManageLobbyHub").build();
connection.on("CheckLobby", function (e) {
    console.log(e)
})
connection.start();