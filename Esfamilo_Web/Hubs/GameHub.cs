using Esfamilo_Core.Interfaces;
using Esfamilo_Core.Ml;
using Esfamilo_Core.ModelView;
using Esfamilo_Domain.Models;
using Esfamilo_Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Security.Claims;
using static Esfamilo_Core.Enums.CategoryEnum;

namespace Esfamilo_Web.Hubs
{
    public class GameHub : Hub
    {
        private string LobbyUID;
        private static readonly Dictionary<string, string> UserSetWordInDbFinish = new Dictionary<string, string>();
        private IWordForCategoryService wordForCategoryService;
        private ILobbyService lobbyService;
        private IUserInLobbyService _userInLobbyService;
        private WordPredictML _wordPredictML;
        private UserManager<IdentityUser> _userManager;
        public GameHub(IWordForCategoryService wordForCategoryService, ILobbyService lobbyService, WordPredictML wordPredictML, IUserInLobbyService userInLobbyService, UserManager<IdentityUser> userManager)
        {
            this.wordForCategoryService = wordForCategoryService;
            this.lobbyService = lobbyService;
            _wordPredictML = wordPredictML;
            _userInLobbyService = userInLobbyService;
            _userManager = userManager;
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
            foreach (var i in data)
            {
                var WordForCategoryModel = new WordForCategory
                {
                    Word = i.Word,
                    TargetLetter = i.TargetLetter,
                    CategoryId = i.GetCategoryId(),
                    UserId = i.Userid,
                    LobbyRound = lobby.CurrentRound,
                    LobbyId = lobby.Id,
                    IsCorrect = await CheckPredictedWordIsCorrect(i.Word, i.Category),
                };
                await wordForCategoryService.Add(WordForCategoryModel);
            }
            await Clients.Caller.SendAsync("CallGetScore");
        }
        public async Task GetScoreReady()
        {
            var GetCurrentUserInLobby = await _userInLobbyService.GetUserInLobbyByUserId(Context.User.FindFirstValue(ClaimTypes.NameIdentifier));
            LobbyUID = Context.GetHttpContext().Request.Query["LobbyUID"].ToString();
            var lobby = await lobbyService.GetLobbyWithUID(LobbyUID);
            var WordsInLobby = await wordForCategoryService.GetAllWordsFromLobby(lobby.Id);
            var getUserInLobby = await _userInLobbyService.GetUserInLobbybyLobbyID(lobby.Id);
            if (GetCurrentUserInLobby.IsUserOwner == true)
            {
                foreach (var i in WordsInLobby)
                {
                    if (i.IsCorrect == true)
                    {
                        var SimilarWordCount = WordsInLobby.Where(x => x.Word == i.Word).Count();
                        if (SimilarWordCount > 1)
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
                foreach(var user in getUserInLobby)
                {
                    var allwordsfromuser = await wordForCategoryService.GetAllWordsFromUserId(user.UserId);
                    int SumUserScore = 0;
                    foreach (var word in allwordsfromuser)
                    {
                        SumUserScore += word.WordScore;
                    }
                    var getuserinlobby = await _userInLobbyService.GetUserInLobbyByUserId(user.UserId);
                    getuserinlobby.UserScore += SumUserScore;
                    await _userInLobbyService.Update(getuserinlobby);
                }
            }
            else
            {
                await Task.Delay(3000);
            }
            var lobbyurl = $"/Lobby/{LobbyUID}";
            await Clients.Caller.SendAsync("SendUserToLobby", lobbyurl);
        }
        public async Task<bool> CheckPredictedWordIsCorrect(string word, string category)
        {

            if (category == CategoryEnums.Names.ToString() || category == CategoryEnums.Lnames.ToString()
|| category == CategoryEnums.Cars.ToString() || category == CategoryEnums.Objects.ToString())
            {
                return true;
            }
            var persianPredict = await CategoryToPersian.GetCategoryPersianName(category);
            var predictedCate = _wordPredictML.Predict(word).PredictedLabel;
            if (predictedCate == persianPredict)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
