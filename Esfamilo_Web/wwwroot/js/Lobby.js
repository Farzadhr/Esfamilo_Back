var connection;
connection = new signalR.HubConnectionBuilder().withUrl(`/LobbyHub?LobbyUID=${location.pathname.split('/')[2]}`).build();
connection.on("GotoGameAllUserLobby", function (url) {
    location.href = url
})
connection.on("CheckUsersInLobbies", function (data) {
    console.log(JSON.parse(data))
    var uluser = document.getElementById("ListOfUserInLobby")
    uluser.innerHTML = ""
    for (var i of JSON.parse(data)) {
        var elem = `                            <li id="${i.UserId}">
                                <div class="UserinLobbyDetail">
                                    <p class="UserInLobbyName">${i.UserName}</p>
                                    <p class="UserInLobbyUserScore">${i.UserScore} <i class="fa fa-trophy"></i></p>
                                </div>
                            </li>`

        uluser.innerHTML += elem
    }

})
connection.on("OutAllLobbyUser", function () {
    location.href = "/"
})
connection.on("CheckSenderMessage", function (jdata, isSender) {
    var data = JSON.parse(jdata)
    var message = `                        <li class="userMessage">
                            <div class="message">
                                <p class="messageText">${data.Message}</p>
                                <p class="messagesProfile">farzadflr</p>
                            </div>
                        </li>`
    var ulmessagecon = document.getElementById("MessagesUlContainer")
    ulmessagecon.innerHTML += message
})
connection.on("CheckClientMessage", function (jdata) {
    var data = JSON.parse(jdata)
    var message = `                        <li class="otherMessage">
                            <div class="message">
                                <p class="messageText">${data.Message}</p>
                                <p class="messagesProfile">${data.SenderName}</p>
                            </div>
                        </li>`
    var ulmessagecon = document.getElementById("MessagesUlContainer")
    ulmessagecon.innerHTML += message
})
connection.on("LobbyStopGame", function () {
    var startbtn = document.getElementById("btnstartgameinlobby")
    startbtn.setAttribute("disabled", "true");
    startbtn.style.backgroundColor = "#3a3b61"
})
connection.start();

var ulmessages = document.getElementById("sendMessageForm")
ulmessages.addEventListener("submit", function (e) {
    e.preventDefault()
    var message = document.getElementById("SendMessageTextInMessageLobby")
    if (message.value != null) {
        connection.invoke("SendMessage", message.value)
        message.value = ""
    } else {
        message.value = ""
        message.setAttribute("placeholder", "لطفا پیام خود را وارد کنید")
    }
})
document.getElementById("btnstartgameinlobby").addEventListener("click", function (e) {
    connection.invoke("GoUsersToRoom")
})
document.getElementById("ExitLobbyBtn").addEventListener("click", function (e) {
    connection.invoke("ExitLobbyCustom")
})