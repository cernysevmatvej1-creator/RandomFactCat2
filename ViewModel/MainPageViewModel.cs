
using ApiDeepSeekl.InterfaceService_;
using ApiDeepSeekl.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace ApiDeepSeekl.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Fact> _listFact;
        [ObservableProperty]
        private string _textFact;
        [ObservableProperty]
        private Fact _fact;
        [ObservableProperty]
        private string _nik;
        private ICatService _catService;
        private IUserService _userService;
        public MainPageViewModel(ICatService catService, IUserService userService   )
        {
            _catService = catService;
            _listFact = new ObservableCollection<Fact>();
            _userService = userService;
        }
        [RelayCommand]
        private async Task GetFact()
        {
            var check = await _catService.GetFact();
            if (check.Text != null)
            {
                TextFact = check.Text;
                Fact = check;
                Debug.WriteLine("Успех");
            }
        }
        [RelayCommand]
        private async Task SaveFact()
        {
            if (Fact != null && Nik != null)
            {

                await _catService.SaveFact(Nik, Fact);
                var check = ListFact.FirstOrDefault(x => x == Fact);
                if (Fact != check && check == null)
                {
                    ListFact.Add(Fact);
                }
              
                Debug.WriteLine("Успех");
              
            }
        }
        public async Task ListFacts()
        {
            ListFact.Clear();
            var check = await _catService.ListGetFacts();

       
            if (check.Message == "401")
            {
                await _userService.RefreshToken();
                check = await _catService.ListGetFacts();
            }

            // Обрабатываем результат
            if (check.Data != null)
            {
                foreach (var fact in check.Data)
                {
                    ListFact.Add(fact);
                    await DialogHelper.ShowAlert("",fact.Id.ToString());
                }
            }
            else if (!check.Success)
            {
                await DialogHelper.ShowAlert("Ошибка", $"{check.Message}");
            }
      
            Nik = _userService.GetNik();
        }
        [RelayCommand]
        private async Task Login()
        {
            if (Nik == null)
                return;
           await _userService.SignAnonimal(Nik);
        }
        [RelayCommand]
        private async Task DeleteFact(Fact fact)
        {
          await  _catService.DeleteFact(fact.Id);
            await DialogHelper.ShowAlert("D", "D");
        }
    }
}