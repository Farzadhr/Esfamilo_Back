using Esfamilo_Core.Interfaces;
using Esfamilo_Core.ModelView;
using Esfamilo_Domain.Models;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace Esfamilo_Web.Hubs
{
    public class GameHub : Hub
    {
        private string LobbyUID;
        private static readonly Dictionary<string, string> UserSetWordInDbFinish = new Dictionary<string, string>();
        private IWordForCategoryService wordForCategoryService;
        private ILobbyService lobbyService;
        public GameHub(IWordForCategoryService wordForCategoryService,ILobbyService lobbyService)
        {
            this.wordForCategoryService = wordForCategoryService;
            this.lobbyService = lobbyService;
        }
        public override Task OnConnectedAsync()
        {
            LobbyUID = Context.GetHttpContext().Request.Query["LobbyUID"].ToString();
            Groups.AddToGroupAsync(Context.ConnectionId, LobbyUID);
            return base.OnConnectedAsync();
        }
        public async Task BtnStopClicked()
        {
            LobbyUID = Context.GetHttpContext().Request.Query["LobbyUID"].ToString();
            await Clients.Group(LobbyUID).SendAsync("BtnStopClickedStart");
        }
        public async Task GetWordData(string WordData)
        {
            LobbyUID = Context.GetHttpContext().Request.Query["LobbyUID"].ToString();
            var lobby = await lobbyService.GetLobbyWithUID(LobbyUID);
            var data = JsonConvert.DeserializeObject<List<GetWordFromJsModelView>>(WordData);
            foreach(var i in data)
            {
                var WordForCategoryModel = new WordForCategory
                {
                    Word = i.Word,
                    TargetLetter = i.TargetLetter,
                    CategoryId = i.GetCategoryId(),
                    UserId = i.Userid,
                    LobbyRound = lobby.CurrentRound,
                    LobbyId = lobby.Id,
                    IsCorrect = await CheckPredictedWord.CheckPredictedWordIsCorrect(i.Word, i.Category),
                };
                await wordForCategoryService.Add(WordForCategoryModel);
            }
            await Clients.Caller.SendAsync("CallGetScore");
        }
        public async Task GetScoreReady()
        {
            LobbyUID = Context.GetHttpContext().Request.Query["LobbyUID"].ToString();
            var lobby = await lobbyService.GetLobbyWithUID(LobbyUID);
            var WordsInLobby = await wordForCategoryService.GetAllWordsFromLobby(lobby.Id);
            foreach(var i in WordsInLobby)
            {
                if(i.IsCorrect == true)
                {
                    bool isSingle = false;
                    var SimilarWordCount = WordsInLobby.Where(x => x.Equals(i.Word)).Count();
                    if (SimilarWordCount > 0)
                    {
                        var UpdateWord = i;
                        UpdateWord.WordScore = 5;
                        await wordForCategoryService.Update(UpdateWord);
                    }
                    else
                    {
                        var UpdateWord = i;
                        UpdateWord.WordScore = 10;
                        await wordForCategoryService.Update(UpdateWord);
                    }
                }
                else
                {
                    var UpdateWord = i;
                    UpdateWord.WordScore = 0;
                    await wordForCategoryService.Update(UpdateWord);
                }
            }
            
            var lobbyurl = $"/Lobby/{LobbyUID}";
            await Clients.Caller.SendAsync("SendUserToLobby",lobbyurl);
        }
    }
}
