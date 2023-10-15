const ShowTargetLetterPgae = document.getElementById("ShowWordForCategoryPage")
const ShowTargetLetterDiv = document.getElementById("ShowWordForCategory")
const WaitToCheckScorePage = document.getElementById("WaitToCheckScorePage")
const GamePageDiv = document.getElementById("GamePage")
var connection;
var timeOver = 180;
var targetLetter = decodeURIComponent(location.pathname).split('/')[3]
connection = new signalR.HubConnectionBuilder().withUrl(`/GameHub?LobbyUID=${location.pathname.split('/')[2]}`).build();
connection.on("BtnStopClickedStart", function () {
    timeOver = 4
})
connection.on("CallGetScore", function () {
    $("#CheckScoreText").html("در حال امتیاز دادن به کلمات شما")
    setTimeout(function () {
        connection.invoke("GetScoreReady")
    },3000)
})
connection.on("SendUserToLobby", function (e) {
    window.location.href = e;
})
connection.start();
$("#" + GamePageDiv.id).hide()
$("#" + WaitToCheckScorePage.id).hide()

var ShowLetterTime = 0
var isshowletter = false
var showletterinterval = setInterval(function () {
    ShowLetterTime++
    if (ShowLetterTime > 4 && ShowLetterTime <= 8) {
        if (isshowletter == false) {
            $("#" + ShowTargetLetterDiv.id).hide();
            ShowTargetLetterDiv.innerHTML = targetLetter
            $("#" + ShowTargetLetterDiv.id).show(250);
            isshowletter = true
        }
    } else if (ShowLetterTime > 8) {
        $("#" + ShowTargetLetterPgae.id).hide(250);
        $("#" + GamePageDiv.id).show(250);
        GamePageFunction();
        clearInterval(showletterinterval)

    } else {
        ShowTargetLetterDiv.innerHTML = ShowLetterTime
    }

}, 1000)

function GamePageFunction() {
    document.getElementById("TargetWordText").innerHTML = `بازی با کلمه ${targetLetter}`
    var timertag = document.getElementById("TimerGame")
    timertag.innerHTML = ConvertSecondToMinuts(timeOver)
    function ConvertSecondToMinuts(sec) {
        var min = Math.floor(sec / 60);
        var second = sec % 60;
        if (second < 10) {
            return `${min}:0${second}`
        } else {
            return `${min}:${second}`
        }

    }
    var gameTimerInterval = setInterval(function () {
        timeOver -= 1;
        if (timeOver < 4) {
            timertag.style.color = "red"
        }
        if (timeOver == 0) {
            clearInterval(gameTimerInterval)
            $("#" + GamePageDiv.id).hide(200)
            $("#" + WaitToCheckScorePage.id).show(200)
            var data = []
            var inputs = document.getElementsByClassName("WordInputOfCategory")
            for (var i of inputs) {
                var obj = { Word: i.value, Category: i.id, Userid: UserId, TargetLetter: targetLetter }
                data.push(obj)
            }
            connection.invoke("GetWordData", JSON.stringify(data))
        }
        timertag.innerHTML = ConvertSecondToMinuts(timeOver)
    }, 1000)
    $(".WordInputOfCategory").keydown(function (e) {
        var key = e.key
        var targetid = e.target.id
        var dataindex = e.target.getAttribute("data-index")

        console.log(dataindex)
        if (key == 1) {
            e.preventDefault()
            dataindex++;
            if (dataindex > $("#ListCategroyInGame").children().length) {
                dataindex = 1
            }
            var nextelem = document.getElementById("ListCategroyInGame").querySelector(`input[data-index="${dataindex}"]`)
            nextelem.focus()
        }
        if (key == 2) {
            e.preventDefault()
            dataindex--;
            if (dataindex == 0) {
                dataindex = $("#ListCategroyInGame").children().length
            }
            var nextelem = document.getElementById("ListCategroyInGame").querySelector(`input[data-index="${dataindex}"]`)
            nextelem.focus()
        }
        if (targetLetter == "ا") {
            if (e.target.value[0] != targetLetter || e.target.value[0] != "آ") {
                e.target.value = ""
            }
        } else {
            if (e.target.value[0] != targetLetter) {
                e.target.value = ""
            }
        }
    });
    $("#StopGameBtn").click(function (e) {
        connection.invoke("BtnStopClicked")
    })
}